using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Odonto.Areas.Odonto.Models.Receituario;
using Odonto.Models.BDODONTO.Context;
using Odonto.Models.BDODONTO.Entidades;
using Odonto.Services.Interfaces;

namespace Odonto.Areas.Odonto.Controllers
{
    [Authorize]
    [Area("Odonto")]
    public class ReceituarioController : Controller
    {
        private readonly IUser _user;
        private readonly DBOdontoContext _context;
        private readonly INotificacao _notify;
        private readonly IUpload _upload;
        public static string[] Forma = { "Comprimidos", "Cápsulas", "Drágeas", "Pós", "Granulados", "Soluções", "Gotas", "Xaropes", "Suspesões", "Elixires", "Pomadas", "Cremes", "Géis", "Pastas" };

        public static string[] AdmVia = { "Oral", "Sublingual", "Retal","Bucal", "Intramuscular", "Intravenosa", "Subcutânea", "Respiratória", "Ocular", "Intranasal", "Respiratória", "Vaginal", "Local" };

        public static string[] DuracaoEm = { "Dias", "Semanas", "Meses" };

        public ReceituarioController(IUser user, DBOdontoContext context, INotificacao notify, IUpload upload)
        {
            _user = user;
            _context = context;
            _notify = notify;
            _upload = upload;
        }

        public IActionResult Cadastrar(int simplesId = 0, int especialId = 0, int pacienteId = 0, int router = 0)
        {
            var id = simplesId != 0 ? simplesId : especialId;

            ViewData["SimplesId"] = simplesId;
            ViewData["EspecialId"] = especialId;

            ViewData["Verifica"] = ((id != 0) && (pacienteId != 0)) ? true : false;
            ViewData["Verifica2"] = ((id != 0) && (pacienteId == 0)) ? true : false;
            ViewData["Verifica3"] = ((id == 0) && (pacienteId != 0) && (router != 0)) ? true : false;
            ViewData["Id"] = id;
            ViewData["Cliente"] = pacienteId;

            return View(new ReceituarioVM(_context, id, pacienteId, router));
        }

        public string[] JsonRemedios(string Termo)
        {

            var remedios = _context.Medicamentos.Where(c =>
                 (!string.IsNullOrEmpty(Termo) ? EF.Functions.Like(c.Nome, "" + Termo + "%") : true))
                 .GroupBy(c => c.Nome)
                 .Select(c => c.Key)
                 .ToArray();

            return remedios;
        }

        [HttpPost]
        public IActionResult Cadastrar(ReceituarioVM model)
        {
            var id = 0;
            List<Receituario> especial = null;
            List<Receituario> simples = null;
            var especialId = 0;
            var simplesId = 0;


            if (model.Id != 0)
            {
                id = model.Id;
                var receita = _context.Receituarios.First(c => c.Id == model.Id);
                receita.IsAtivo = false;
                _context.Update(receita);   
                _notify.Success("Receituário editado com sucesso.");
            }
            else
            {
                _notify.Success("Receituário cadastrado com sucesso.");
            }


            especial = model.ReceituarioList.Where(c => c.Tipo == 2).ToList();
            simples = model.ReceituarioList.Where(c => c.Tipo == 1).ToList();


            var receitaEspecial = new Receituarios()
            {
                ClienteId = model.PacienteId,
            };
            var receitaSimples = new Receituarios()
            {
                ClienteId = model.PacienteId,
            };

            var receitaEspecisal = especial.Any() ? _context.Add(receitaEspecial) : null;
            var receitaSimplses = simples.Any() ? _context.Add(receitaSimples) : null;

            _context.SaveChanges();

            especialId = receitaEspecial.Id;
            simplesId = receitaSimples.Id;

            _context.SaveChanges();

            foreach (var item in especial)
            {
                var receituario = new ReceituarioMedicamentos()
                {
                    AdmVia = item.AdmVia,
                    Quantidade = item.Quantidade.Value,
                    Concentracao = item.Concentracao,
                    Dose = item.Dose,
                    Duracao = item.Duracao.Value,
                    DuracaoEm = item.DuracaoEm,
                    Forma = item.Forma,
                    Medicamento = item.Medicamento,
                    Tipo = item.Tipo,
                    RecedituarioId = especialId
                };
                _context.Add(receituario);
            }

            foreach (var item in simples)
            {
                var receituario = new ReceituarioMedicamentos()
                {
                    AdmVia = item.AdmVia,
                    Quantidade = item.Quantidade.Value,
                    Concentracao = item.Concentracao,
                    Dose = item.Dose,
                    Duracao = item.Duracao.Value,
                    DuracaoEm = item.DuracaoEm,
                    Forma = item.Forma,
                    Medicamento = item.Medicamento,
                    Tipo = item.Tipo,
                    RecedituarioId = simplesId
                };
                _context.Add(receituario);
            }

            

            _context.SaveChanges();

            return RedirectToAction("Detalhes", new { simplesId, especialId, print = true, clienteid = model.PacienteId, router = model.Router });

        }

        [HttpPost]
        public IActionResult TrReceituario(Receituario model, int qtd, bool isExist)
        {
            var receituario = new Receituario()
            {
                FormaS = Forma[model.Forma - 1],
                Quantidade = model.Quantidade,
                Medicamento = model.Medicamento,
                Concentracao = model.Concentracao,
                DuracaoEmS = DuracaoEm[model.DuracaoEm - 1],
                Dose = model.Dose,
                AdmViaS = AdmVia[model.AdmVia - 1],
                Duracao = model.Duracao,
                AdmVia = model.AdmVia,
                DuracaoEm = model.DuracaoEm,
                Forma = model.Forma,
                Tipo = model.Tipo,
                Index = isExist ? qtd : (qtd + 1)
            };

            return View(receituario);
        }

        public IActionResult Imprimir(int id, int simplesId = 0, int especialId = 0)
        {

            var simples = new ListReceituario();
            var especial = new ListReceituario();
            var listReceituario = new List<ListReceituario>();

     
            if (especialId != 0)
            {
                especial = new ListReceituario()
                {
                    IdReceita = especialId,
                    ReceituarioList = Receita(especialId),
                    IsEspecial = true
                };
                listReceituario.Add(especial);
                
            }


            if (simplesId != 0)
            {
                simples = new ListReceituario()
                {
                    IdReceita = simplesId,
                    ReceituarioList = Receita(simplesId),
                    IsEspecial = false
                };
                listReceituario.Add(simples);
            }

            var cliente = _context.Receituarios
                .Include(c => c.Clientes)
                .First(c => c.Id == simplesId || c.Id == especialId);

            ViewData["Nome"] = cliente.Clientes.Nome;
            ViewData["Endereco"] = cliente.Clientes.Endereco;
            ViewData["Telefone"] = cliente.Clientes.Telefone;
            //var receita = Receita(id);
            
         

            return View(listReceituario);
        }

        public IActionResult Detalhes(int simplesId = 0, int especialId = 0, bool print = false, int clienteid = 0, int router = 0)
        {
            ViewData["Id"] = simplesId;
            ViewData["Print"] = print;

            var simples = new ListReceituario();
            var especial = new ListReceituario();
            var listReceituario = new List<ListReceituario>();

            ViewData["especialId"] = 0;
            ViewData["simplesId"] = 0;

            ViewData["Verifica"] = clienteid != 0 || router != 0 ? true : false;
            ViewData["Cliente"] = clienteid;

            var cliente = _context.Receituarios
                .Include(c => c.Clientes)
                .First(c => c.Id == simplesId || c.Id == especialId);

            ViewData["Nome"] = cliente.Clientes.Nome;
            ViewData["Endereco"] = cliente.Clientes.Endereco;
            ViewData["Telefone"] = cliente.Clientes.Telefone;

            if (especialId != 0)
            {
                ViewData["especialId"] = especialId;
                especial = new ListReceituario()
                {
                    EspecialId = especialId,
                    ReceituarioList = Receita(especialId)
                };
                listReceituario.Add(especial);
            }


            if (simplesId != 0)
            {
                ViewData["simplesId"] = simplesId;
                simples = new ListReceituario()
                {
                    SimplesId = simplesId,
                    ReceituarioList = Receita(simplesId)
                };
                listReceituario.Add(simples);
            }        

  
            //var tipo = receita.Select(c => new { c.Tipo }).ToList();

            ViewData["Tipo"] = false;// tipo.Any(c => c.Tipo == 2);

            return View(listReceituario);
        }

        public List<Receituario> Receita(int id)
        {
            var receita = _context.ReceituarioMedicamentos
                .Include(c => c.Receituarios)
                    .ThenInclude(c => c.Clientes)
                .Where(c => c.RecedituarioId == id)
                .Select(c => new Receituario
                {
                    Medicamento = c.Medicamento,
                    DuracaoEm = c.Duracao,
                    DuracaoEmS = DuracaoEm[c.DuracaoEm - 1],
                    Dose = c.Dose,
                    Quantidade = c.Quantidade,
                    Forma = c.Forma,
                    FormaS = Forma[c.Forma - 1],
                    Duracao = c.Duracao,
                    AdmVia = c.AdmVia,
                    AdmViaS = AdmVia[c.AdmVia - 1],
                    Concentracao = c.Concentracao,
                    Tipo = c.Tipo
                })
                .ToList();

            return receita;
        }
    }
}