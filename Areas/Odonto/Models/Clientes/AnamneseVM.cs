
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odonto.Areas.Odonto.Models.Clientes
{
    public class AnamneseVM
    {
        public List<Anamneses> AnamnesesList { get; set; }
    }

    public class Anamneses
    {
        public string Id { get; set; }
        public int ClienteId { get; set; }
        public int AnamneseId { get; set; }
        public string Descricao { get; set; }
        public int Tipo { get; set; }
        public string DescricaoAnamnese { get; set; }
        public bool IsNot { get; set; }
    }
}
