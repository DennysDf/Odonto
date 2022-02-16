using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Odonto.Models.BDODONTO.Entidades;
using Odonto.Areas.Odonto.Models.Receituario;


namespace Odonto.Areas.Odonto.Models.Clientes
{
    public class ClientesVM
    {
        public ClientesVM()
        {

        }

        public AnamneseVM Anamnese { get; set; }
        public List<ProcedimentosVM> Procedimentos { get; set; }
        public List<ReceituarioVM> Receituarios { get; set; }
        public List<ExamesVM> Exames { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public string Nome { get; set; }
        [MinLength(10, ErrorMessage ="Data invalida.")]
        public string DataNasc { get; set; }
        public string Cidade { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public string Endereco { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public string Telefone { get; set; }
        public string Telefone2 { get; set; }
        public string Email { get; set; }
        public string EstadoCivil { get; set; }
        public string Convenio { get; set; }
        public string Profisao { get; set; }
        public string NumConvenio { get; set; }
        public string Profissao { get; set; }
        public string InicioTrate { get; set; }
        public string Obs { get; set; }
        public string DentistaOrigem { get; set; }
        public IFormFile Foto { get; set; }
        public string DataNascimento { get; internal set; }
        public string Imagem { get; set; }
        
    }
}
