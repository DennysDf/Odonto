using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odonto.Models.BDODONTO.Entidades
{
    public class ReceituarioMedicamentos
    {
        public int Id { get; set; }
        public string Medicamento { get; set; }
        public int? MedicamentoId { get; set; }
        public Medicamentos Medicamentos { get; set; }
        public int RecedituarioId { get; set; }
        public Receituarios Receituarios { get; set; }
        public string Concentracao { get; set; }
        public int Forma { get; set; }
        public string Dose { get; set; }
        public int AdmVia { get; set; }
        public int Tipo { get; set; }
        public int Duracao { get; set; }
        public int DuracaoEm { get; set; }
        public int Quantidade { get; set; }
    }
}
