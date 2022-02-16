using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Odonto.Models.BDODONTO.Entidades
{
    public class Medicamentos
    {
        public Medicamentos()
        {
            Receituarios = new HashSet<ReceituarioMedicamentos>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("principio_ativo")]
        public string PrincipioAtivo { get; set; }
        [Column("laboratorio")]
        public string Laboratorio { get; set; }
        [Column("nome")]
        public string Nome { get; set; }
        [Column("apresentacao")]
        public string Apresentacao { get; set; }


        public ICollection<ReceituarioMedicamentos> Receituarios { get; set; }
    }
}
