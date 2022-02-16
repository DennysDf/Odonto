using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odonto.Models.BDODONTO.Entidades
{
    public class Procedimentos
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string DataRealizacao { get; set; }
        public DateTime DataCad { get; set; }
        public int ClienteId { get; set; }
        public Clientes Cliente { get; set; }
        public bool IsAtivo { get; set; }
    }
}
