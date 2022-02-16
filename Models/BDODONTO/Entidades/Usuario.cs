using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odonto.Models.BDODONTO.Entidades
{
    public class Usuario
    {
        public Usuario()
        {
            Clientes = new HashSet<Clientes>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Extensao { get; set; }
        public string Email { get; set; }
        public string Conselho { get; set; }
        public string Especialidade { get; set; }
        public int Tipo { get; set; }
        public int ConsultorioId { get; set; }
        public bool IsAtivo { get; set; }
        public Consultorio Consultorio { get; set; }
        public DateTime? LastAcesso { get; set; }

        public ICollection<Clientes> Clientes { get; set; }
    }
}
