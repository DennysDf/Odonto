using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odonto.Models.BDODONTO.Entidades
{
    public class Receituarios
    {
        public Receituarios()
        {
            ReceituarioMedicamentos = new HashSet<ReceituarioMedicamentos>();
        }

        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Clientes Clientes { get; set; }
        public DateTime DataCad { get; set; }
        public bool IsAtivo { get; set; }

        public ICollection<ReceituarioMedicamentos> ReceituarioMedicamentos { get; set; }
    }
}
