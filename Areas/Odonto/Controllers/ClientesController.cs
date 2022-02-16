using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Odonto.Areas.Odonto.Models.Clientes;
using Odonto.Areas.Odonto.Models.Receituario;
using Odonto.Models.BDODONTO.Context;
using Odonto.Models.BDODONTO.Entidades;
using Odonto.Services.Interfaces;

namespace Odonto.Areas.Odonto.Controllers
{
    [Authorize]
    [Area("Odonto")]
    public class ClientesController : Controller
    {
        private readonly IUser _user;
        private readonly DBOdontoContext _context;
        private readonly INotificacao _notify;
        private readonly IUpload _upload;

        public ClientesController(IUser user, DBOdontoContext context, INotificacao notify, IUpload upload)
        {
            _user = user;
            _context = context;
            _notify = notify;
            _upload = upload;
        }

        public IActionResult Cadastrar() => View(new ClientesVM());

        [HttpPost]
        public IActionResult Cadastrar(ClientesVM model)
        {
            var direitorio = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Upload\\Clientes");

            var url = "";
            var id = 0;
            if (model.Id != 0)
            {
                id = model.Id;
                url = "cliente";
                var cliente = _context.Clientes.First(c => c.Id == model.Id);
                cliente.Nome = model.Nome;
                cliente.NumConvenio = model.NumConvenio;
                cliente.Cidade = model.Cidade;
                cliente.Convenio = model.Convenio;
                cliente.DataNasc =model.DataNasc;
                cliente.InicioTrate = model.InicioTrate;
                cliente.Telefone = model.Telefone;
                cliente.Telefone2 = model.Telefone2;
                cliente.Obs = model.Obs;
                cliente.Profissao = model.Profissao;
                cliente.EstadoCivil = model.EstadoCivil;
                cliente.Endereco = model.Endereco;
                cliente.Email = model.Email;
                cliente.DentistaOrigem = model.DentistaOrigem;                
                
                
                if(model.Foto != null)
                {
                    if (cliente.Extensao != null)
                        _upload.Delete(direitorio, $"{id}_{cliente.Extensao}");

                    _upload.Save(direitorio, model.Foto, cliente.Id);
                    cliente.Extensao = Path.GetFileName(model.Foto.FileName);
                }
                _context.Update(cliente);
                _context.SaveChanges();
                _notify.Success("Paciente atualizado com sucesso.");       
                
            }
            else
            {
                url = "anamnese";
                var cliente = new Clientes()
                {
                    Nome = model.Nome,
                    Cidade = model.Cidade,
                    Convenio = model.Convenio,
                    DentistaOrigem = model.DentistaOrigem,
                    Email = model.Email,
                    Endereco = model.Endereco,
                    InicioTrate = model.InicioTrate,
                    Profissao = model.Profissao,
                    EstadoCivil = model.EstadoCivil,
                    DataNasc = model.DataNasc,
                    Obs = model.Obs,
                    NumConvenio = model.NumConvenio,
                    Telefone = model.Telefone,
                    Telefone2 = model.Telefone2,
                    IdResp = _user.Id,
                    Extensao = model.Foto != null ? Path.GetFileName(model.Foto.FileName) : null
                };
                _context.Add(cliente);                
                _notify.Success("Paciente cadastrado com sucesso.");
                _context.SaveChanges();
                id = cliente.Id;
                
                if (model.Foto != null)
                {
                    _upload.Save(direitorio, model.Foto, id);
                }                
            }

            return RedirectToAction(nameof(Detalhes), new { id, url });
        }

        public IActionResult DeletarPaciente(int id)
        {
            var cliente = _context.Clientes.First(c => c.Id == id);
            cliente.IsAtivo = false;
            _context.Update(cliente);
            _context.SaveChanges();
            _notify.Success("Cliente apagado com sucesso.");
            return RedirectToAction("Listar");
        }

        public IActionResult Listar(string id)
        {
             
            var clientes = _context.Clientes
                .Where(c => c.IsAtivo
                && (!string.IsNullOrEmpty(id) ? EF.Functions.Like(c.Nome, "%" + id+ "%") : true)
                )
                .Select(c => new ClientesVM
                {   
                    Id = c.Id,
                    Imagem = c.Extensao != null ? $"{c.Id}_{c.Extensao}": null,
                    Cidade =  c.Cidade,
                    DataNascimento = c.DataNasc,
                    Endereco =  c.Endereco,
                    Telefone = c.Telefone,
                    EstadoCivil = c.Extensao,
                    Nome = c.Nome

                })
                .ToList();
                


            return View(clientes);
        }
        
        public IActionResult Detalhes(int id)
        {
            var format = "dd/MM/yyyy";
            CultureInfo provider = CultureInfo.InvariantCulture;            


            ViewData["Id"] = id;
            ViewData["Nome"] = _context.Clientes.First(C => C.Id == id).Nome;
            
            var clienteAnamnese = _context.ClienteAnamneses.Where(s => s.ClienteId == id && s.Descricao != null);

            var anamneseList = _context.Anamneses
                .Where(c => c.IsAtivo)
                .Select(c => new Anamneses
                {
                    AnamneseId = c.Id,
                    Id = clienteAnamnese.Any(s => s.AnamneseId == c.Id) ? clienteAnamnese.First(s => s.AnamneseId == c.Id).Id.ToString() : null,
                    ClienteId = id,
                    Tipo = c.Tipo,
                    Descricao = c.Descricao,
                    DescricaoAnamnese = clienteAnamnese.Any(s => s.AnamneseId == c.Id) ? clienteAnamnese.First(s => s.AnamneseId == c.Id).Descricao.ToString() : null
                })
                .OrderBy(c => c.Tipo)                   
                .ToList();

            var cliente = _context.Clientes.Where(c => c.Id == id)
                .Select(c => new ClientesVM
                {
                    Nome = c.Nome,
                    Id = c.Id,
                    Cidade = c.Cidade,
                    Convenio = c.Convenio,
                    DataNasc = c.DataNasc,
                    DentistaOrigem = c.DentistaOrigem,
                    Email = c.Email,
                    Endereco = c.Endereco,
                    EstadoCivil = c.EstadoCivil,
                    InicioTrate = c.InicioTrate,
                    Telefone2 = c.Telefone2,
                    NumConvenio = c.NumConvenio,
                    Obs = c.Obs,
                    Profissao = c.Profissao,
                    Telefone = c.Telefone,
                    Imagem = c.Extensao == null ? null : $"{c.Id}_{c.Extensao}"                   
                })
                .First();

            cliente.Anamnese = new AnamneseVM() { AnamnesesList = anamneseList };

            //var mesesMin = new string[] { "Jan", "Fev", "Mar", "" };


            var procedimentos = _context.Procedimentos
                .Where(c => c.IsAtivo && c.ClienteId == id)
                .Select(c => new ProcedimentosVM
                {
                    Data = String.Format("{0:dd/MM/yyyy}", DateTime.ParseExact(c.DataRealizacao, format, provider)),
                    Titulo = c.Titulo,
                    Id = c.Id,
                    DateOrder = DateTime.ParseExact(c.DataRealizacao, format, provider),
                    Descricao = c.Descricao
                })
                .OrderByDescending(c => c.DateOrder)
                .ToList();

            cliente.Procedimentos = procedimentos;

            var exames = _context.Exames
                .Where(c => c.IsAtivo && c.ClienteId == id)
                .Select(c => new ExamesVM
                {
                    Id = c.Id,
                    Data = String.Format("{0:dd/MM/yyyy}", c.DataRealizao),
                    Descricao = c.Descricao,
                    Imagem = $"{c.Id}_{c.Extensao}",
                    Titulo = c.Titulo,
                    DateOrder = DateTime.ParseExact(c.DataRealizao, format, provider),
                })
                .OrderByDescending(c => c.DateOrder)
                .ToList();

            cliente.Exames = exames;

           var receitas =  _context.Receituarios
                .Where(c => c.ClienteId == id && c.IsAtivo)
                .Include(c => c.ReceituarioMedicamentos)
                .Select(c => new ReceituarioVM
                {
                    Data = String.Format("{0:dd/MM/yyyy}", c.DataCad),
                    MedicamentosList = c.ReceituarioMedicamentos.ToList(),
                    DateOrder = c.DataCad,
                    Id = c.Id,
                    EspecialId = c.ReceituarioMedicamentos.Any(a => a.Tipo == 2) ? c.Id : 0,
                    SimplesId = c.ReceituarioMedicamentos.Any(a => a.Tipo == 1) ? c.Id : 0
                })
                .OrderByDescending(c => c.DateOrder)
                .ToList();

            cliente.Receituarios = receitas;


            return View(cliente);
        }

        public IActionResult Anamnese(AnamneseVM model)
        {
            var clienteAnamnese = new ClienteAnamnese();

            foreach (var item in model.AnamnesesList)
            {
                if (item.DescricaoAnamnese != null || item.Id != null)
                {
                    if (item.Id != null)
                    {
                        clienteAnamnese = _context.ClienteAnamneses.First(c => c.Id == Int32.Parse(item.Id));
                        clienteAnamnese.Descricao = item.DescricaoAnamnese;
                        _context.Update(clienteAnamnese);
                    }
                    else
                    {
                        clienteAnamnese = new ClienteAnamnese()
                        {
                            AnamneseId = item.AnamneseId,
                            ClienteId = item.ClienteId,
                            Descricao = item.DescricaoAnamnese
                        };
                        _context.Add(clienteAnamnese);
                    }                  
                }
            }
            
            _notify.Success("Anamnese cadastrada com sucesso.");
            _context.SaveChanges();

            return RedirectToAction(nameof(Detalhes), new { id = model.AnamnesesList.First().ClienteId, url = "anamnese"});

        }

        [HttpGet]
        public IActionResult Procedimentos(int id, int clienteId)
        {
            if (id != 0)
                return View(new ProcedimentosVM(_context, id, clienteId));
            else
                return View(new ProcedimentosVM(clienteId));            
        }

        [HttpPost]
        public IActionResult Procedimentos(ProcedimentosVM model)
        {
            if (model.Id != 0)
            {
                model.Edit(_context.Procedimentos.First(c => c.Id == model.Id));
                _notify.Success("Procedimento editado com sucesso.");
            }
            else
            {
                _context.Add(model.Insert());
                _notify.Success("Procedimento cadastrado com sucesso.");
            }
                

            _context.SaveChanges();

            return RedirectToAction(nameof(Detalhes), new { id = model.ClienteId, url = "procedimentos"});
        }

        public IActionResult ExcluirProcedimentos(int clienteId, int id)
        {
            var procedimento = _context.Procedimentos.First(c => c.Id == id);
            procedimento.IsAtivo = false;
            _context.Update(procedimento);
            _context.SaveChanges();
            _notify.Success("Procedimento apagado com sucesso.");
            return RedirectToAction(nameof(Detalhes), new { id = clienteId, url = "procedimentos"});
        }

        public IActionResult ExcluirExame(int clienteId, int id)
        {
            var exames = _context.Exames.First(c => c.Id == id);
            exames.IsAtivo = false;
            _context.Update(exames);
            _context.SaveChanges();
            _notify.Success("Exame apagado com sucesso.");
            return RedirectToAction(nameof(Detalhes), new { id = clienteId, url = "exames" });
        }

        public IActionResult ExcluirReceituario(int clienteId, int id)
        {
            var receituarios = _context.Receituarios.First(c => c.Id == id);
            receituarios.IsAtivo = false;
            _context.Update(receituarios);
            _context.SaveChanges();
            _notify.Success("Receituário apagado com sucesso.");
            return RedirectToAction(nameof(Detalhes), new { id = clienteId, url = "receituario" });
        }


        [HttpGet]
        public IActionResult Exames(int id, int clienteId)
        {
            if (id != 0)
                return View(new ExamesVM(_context,id, clienteId));
            else
                return View(new ExamesVM(clienteId));
        }

        [HttpPost]        
        public  IActionResult Exames(ExamesVM model)
        {
            if (model.Id != 0)
            {
                model.Edit(_context.Exames.First(c => c.Id == model.Id));
                _context.SaveChanges();
                _notify.Success("Exame cadastrado com sucesso.");
            }
            else
            {
                var exame = model.Insert();
                _context.Add(exame);
                 _context.SaveChanges();               
                    var diretorio = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Upload\\Exames");
                    if (model.Foto != null)
                    {
                        if (!Directory.Exists(diretorio)) Directory.CreateDirectory(diretorio);
                        var nFiles = Directory.GetFiles(diretorio);
                        using (var stream = new FileStream(Path.Combine(diretorio, exame.Id.ToString() + "_" + Path.GetFileName(model.Foto.FileName)), FileMode.Create))
                        {
                            model.Foto.CopyTo(stream);
                        }
                    } 
                _notify.Success("Exame cadastrado com sucesso.");
            }
            return RedirectToAction(nameof(Detalhes), new { id = model.ClienteId, url = "exames"});
        }

    }
}