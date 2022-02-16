using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odonto.Models.BDODONTO.Entidades
{
    public class Consultorio
    {
        public Consultorio()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public bool IsAtivo { get; set; }
        public DateTime DataCad { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }
    }
}
