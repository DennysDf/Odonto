using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Odonto.Areas.Odonto.Models.Home;
using Odonto.Models.BDODONTO.Context;
using Odonto.Models.Login;
using Odonto.Services.Interfaces;

namespace Odonto.Areas.Odonto.Controllers
{
    [Authorize]
    [Area("Odonto")]
    public class HomeController : Controller
    {
        private readonly IUser _user;
        private readonly DBOdontoContext _context;
        private readonly INotificacao _notify;
        private readonly IUpload _upload;
        public IHttpContextAccessor _http { get; set; }

        public HomeController(IUser user, DBOdontoContext context, INotificacao notify, IUpload upload, IHttpContextAccessor http)
        {
            _user = user;
            _context = context;
            _notify = notify;
            _upload = upload;
            _http = http;
        }

        public IActionResult Index()
        {
            ViewData["QtdClientes"] = _context.Clientes.Include(c => c.Resp).Where(c => c.Resp.ConsultorioId == _user.ClinicaId).Count();

            ViewData["QtdReceituarios"] = _context.Receituarios.Where(c => c.IsAtivo).Include(c => c.Clientes).ThenInclude(c => c.Resp).Where(c => c.Clientes.Resp.ConsultorioId == _user.ClinicaId).Count();

            ViewData["QtdProcedimentos"] = _context.Procedimentos.Where(c => c.IsAtivo).Include(c => c.Cliente).ThenInclude(c => c.Resp).Where(c => c.Cliente.Resp.ConsultorioId == _user.ClinicaId).Count();

            ViewData["QtdExames"] = _context.Exames.Where(c => c.IsAtivo).Include(c => c.Cliente).ThenInclude(c => c.Resp).Where(c => c.Cliente.Resp.ConsultorioId == _user.ClinicaId).Count();

            return View();
        }

        public IActionResult Perfil()
        {
            var cliente = _context.Usuarios
                .Include(c => c.Consultorio)
                .Where(c => c.Id == _user.Id)
                .Select(c => new PerfilVM
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Email = c.Email,
                    Endereco = c.Consultorio.Endereco,
                    NomeConsultorio = c.Consultorio.Nome,
                    Telefone = c.Consultorio.Telefone,
                    Conselho = c.Conselho,
                    Especialidade = c.Especialidade
                })
                .First();
            
            var cli = TempData["Filtro"] != null ? JsonConvert.DeserializeObject<PerfilVM>(TempData["Filtro"].ToString()) : cliente;

            return View(cli);
        }
        
        [HttpPost]
        public async Task<IActionResult> Perfil(PerfilVM model, IFormFile Foto)
        {
            

            var direitorio = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Upload\\Usuarios");

            var user = _context.Usuarios.First(c => c.Id == _user.Id);

            if (model.SenhaN != null && model.Senha == null)
            {
                TempData["Filtro"] = JsonConvert.SerializeObject(model);
                TempData["MensagemSenhaAtual"] = "Para atualizar a senha digite a senha atual.";
                return RedirectToAction("Perfil");
            }

            var senha = model.Senha != null ? model.Senha : user.Senha;

            if(senha != user.Senha)
            {
                TempData["Filtro"] = JsonConvert.SerializeObject(model);
                TempData["MensagemSenhaAtual"] = "Senha não coincide com a senha atual.";
                return RedirectToAction("Perfil");
            }

            if(Foto != null && user.Extensao != null)
            {
                _upload.Delete(direitorio, $"{user.Id}_{user.Extensao}");
                user.Extensao = null;
            }

            if (Foto != null)
            {
                _upload.Save(direitorio, Foto, _user.Id);
                user.Extensao = Path.GetFileName(Foto.FileName);
            }
                

            user.Nome = model.Nome;
            user.Email = model.Email;
            user.Especialidade = model.Especialidade;
            user.Conselho = model.Conselho;
            if (model.SenhaN != null)
                user.Senha = model.SenhaN;
            _context.Update(user);

            var consultorio = _context.Consultorios.First(c => c.Id == user.ConsultorioId);
            consultorio.Endereco = model.Endereco;
            consultorio.Nome = model.NomeConsultorio;
            consultorio.Telefone = model.Telefone;
            _context.Update(consultorio);

            var createCookies = new CreateCookies(_http);
            await createCookies.Autenticar(user);

            _context.SaveChanges();
            _notify.Success("Perfil atualizado com sucesso.");

            return RedirectToAction("Index");
        }     

        public bool ValidaSenha(string senha)
        {
            var senhaAtual = _context.Usuarios.First(c => c.Id == _user.Id).Senha;

            return senhaAtual == senha;

        }
    }
}