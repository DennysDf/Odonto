using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Odonto.Models.BDODONTO.Context;
using Odonto.Models.BDODONTO.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Odonto.Areas.Odonto.Models.Receituario
{
    public class ReceituarioVM
    {
        public ReceituarioVM()
        {

        }

        public ReceituarioVM(DBOdontoContext _context, int id, int pacienteId, int router)
        {
            Pacientes = new SelectList(_context.Clientes.Where(c => c.IsAtivo).Select(c => new { c.Id, c.Nome }).OrderBy(c => c.Nome).ToList(), "Id", "Nome");
            Router = router;
            var medicamentos = _context.Medicamentos.Select(c => new { c.Nome }).GroupBy(c => new { c.Nome }).Select(c => new { Id = 1, Nome = c.Key.Nome }).OrderBy(c => c.Nome).ToList();
            Medicamentos = new SelectList(medicamentos, "Id", "Nome");
            ReceituarioList = new List<Receituario>();

            if (id != 0)
            {
                PacienteId = _context.Receituarios.First(c => c.Id == id).ClienteId;
                Id = id;
                var receita = _context.ReceituarioMedicamentos
                    .Include(c => c.Receituarios)
                        .ThenInclude(c => c.Clientes)
                    .Where(c => c.RecedituarioId == id && c.Receituarios.IsAtivo)
                    .Select(c => new Receituario
                    {
                        Medicamento = c.Medicamento,
                        DuracaoEm = c.DuracaoEm,
                        DuracaoEmS = DuracaoEm[c.DuracaoEm - 1],
                        Dose = c.Dose,
                        Quantidade = c.Quantidade,
                        Forma = c.Forma,
                        FormaS = Forma[c.Forma - 1],
                        Duracao = c.Duracao,
                        AdmVia = c.AdmVia,
                        AdmViaS = AdmVia[c.AdmVia - 1],
                        Concentracao = c.Concentracao,
                        Tipo = c.Tipo,
                    })
                    .ToList();
                ReceituarioList = receita;
            }

            if (pacienteId != 0)
                PacienteId = pacienteId;

        }

        public int Id { get; set; }
        public List<ReceituarioMedicamentos> MedicamentosList { get; set; }
        public DateTime DateOrder { get; set; }
        public string Data { get; internal set; }
        public int Qtd { get; internal set; }
        public int Router { get; set; }
        public SelectList Pacientes { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public int PacienteId { get; set; }
        public SelectList Medicamentos { get; set; }
        public int MedicamentosId { get; set; }
        public List<Receituario> ReceituarioList { get; set; }
        public static string[] Forma = { "Comprimidos", "Cápsulas", "Drágeas", "Pós", "Granulados", "Soluções", "Gotas", "Xaropes", "Suspesões", "Elixires", "Pomadas", "Cremes", "Géis", "Pastas" };
        public static string[] AdmVia = { "Oral", "Sublingual", "Retal", "Bucal", "Intramuscular", "Intravenosa", "Subcutânea", "Respiratória", "Ocular", "Intranasal", "Respiratória", "Vaginal", "Local" };
        public static string[] DuracaoEm = { "Dias", "Semanas", "Meses" };
        public int EspecialId { get; set; }
        public int SimplesId { get; set; }
    }

    public class Receituario
    {
        [Required(ErrorMessage = Global.Required)]
        public string Medicamento { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public string Concentracao { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public int Forma { get; set; }
        public string FormaS { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public int AdmVia { get; set; }
        public string AdmViaS { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public int Tipo { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public string Dose { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public int? Quantidade { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public int? Duracao { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public int DuracaoEm { get; set; }
        public string DuracaoEmS { get; set; }
        public int Index { get; set; }
        public bool Exist { get; set; }
    }
    
    public class ListReceituario
    {
        public int IdReceita { get; set; }
        public int SimplesId { get; set; }
        public int EspecialId { get; set; }
        public bool IsEspecial { get; set; }
        public List<Receituario> ReceituarioList { get; set; }
    }


}
