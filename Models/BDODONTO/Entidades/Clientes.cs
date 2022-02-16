using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odonto.Models.BDODONTO.Entidades
{
    public class Clientes
    {
        public Clientes()
        {
            Exames = new HashSet<Exames>();
            Procedimentos = new HashSet<Procedimentos>();
            ClienteAnamneses = new HashSet<ClienteAnamnese>();
            Receituarios = new HashSet<Receituarios>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string DataNasc { get; set; }
        public string Cidade { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Telefone2 { get; set; }
        public string Email { get; set; }
        public string EstadoCivil { get; set; }
        public string Convenio { get; set; }        
        public string NumConvenio { get; set; }
        public string Profissao { get; set; }
        public string InicioTrate { get; set; }
        public string Obs { get; set; }
        public string DentistaOrigem { get; set; }
        public string Extensao { get; set; }
        public DateTime DataCad { get; set; }
        public bool IsAtivo { get; set; }

        public int IdResp { get; set; }
        public Usuario Resp { get; set; }

        public ICollection<Exames> Exames { get; set; }
        public ICollection<Procedimentos> Procedimentos { get; set; }
        public ICollection<ClienteAnamnese> ClienteAnamneses { get; set; }
        public ICollection<Receituarios> Receituarios { get; set; }
    }
}
