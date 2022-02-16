using Microsoft.AspNetCore.Http;
using Odonto.Models.BDODONTO.Context;
using Odonto.Models.BDODONTO.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Odonto.Areas.Odonto.Models.Clientes
{
    public class ExamesVM
    {
        public ExamesVM()
        {

        }

        public ExamesVM(int clienteId)
        {
            ClienteId = clienteId;
        }

        public ExamesVM(DBOdontoContext _context, int id, int clienteId)
        {
            var procedimento = _context.Exames.First(c => c.Id == id);
            Id = procedimento.Id;
            Data = procedimento.DataRealizao;
            Titulo = procedimento.Titulo;
            Descricao = procedimento.Descricao;
            ClienteId = clienteId;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = Global.Required)]
        [MinLength(10, ErrorMessage = "Data invalida.")]
        public string Data { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public string Titulo { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public string Descricao { get; set; }
        public int ClienteId { get; set; }
        public DateTime DateOrder { get; set; }
        [Required(ErrorMessage = Global.Required)]
        public IFormFile Foto { get; set; }
        public string Imagem { get; set; }

        public Exames Insert() => new Exames() { Extensao = Foto != null ? Path.GetFileName(Foto.FileName) : null, ClienteId = ClienteId, Descricao = Descricao, DataRealizao = Data, Titulo = Titulo };

        public Exames Edit(Exames model)
        {
            model.Titulo = Titulo;
            model.Descricao = Descricao;
            model.DataRealizao = Data;
            return model;
        }

    }
}
