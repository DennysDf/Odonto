using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odonto.Models.BDODONTO.Entidades
{
    public class Anamnese
    {
        public Anamnese()
        {
            ClienteAnamneses = new HashSet<ClienteAnamnese>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Tipo { get; set; }
        public DateTime DataCad { get; set; }
        public bool IsAtivo { get; set; }
        public ICollection<ClienteAnamnese> ClienteAnamneses { get; set; }
    }
}
