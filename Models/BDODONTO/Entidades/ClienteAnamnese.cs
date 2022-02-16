using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odonto.Models.BDODONTO.Entidades
{
    public class ClienteAnamnese
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Clientes Cliente { get; set; }
        public int AnamneseId { get; set; }
        public Anamnese Anamnese { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCad { get; set; }
        public bool IsAtivo { get; set; }
    }
}
