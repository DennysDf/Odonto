using Odonto.Models.BDODONTO.Context;
using Odonto.Models.BDODONTO.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Odonto.Areas.Odonto.Models.Clientes
{
    public class ProcedimentosVM
    {
        public ProcedimentosVM()
        {

        }

        public ProcedimentosVM(int clienteId)
        {
            ClienteId = clienteId;
        }

        public ProcedimentosVM(DBOdontoContext _context, int id, int clienteId )
        {
            var procedimento = _context.Procedimentos.First(c => c.Id == id);
            Id = procedimento.Id;
            Data = procedimento.DataRealizacao;
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

        public Procedimentos Insert() => new Procedimentos() { ClienteId = ClienteId, Descricao = Descricao, DataRealizacao = Data, Titulo = Titulo };

        public Procedimentos Edit(Procedimentos model)
        {
            model.Titulo = Titulo;
            model.Descricao = Descricao;
            model.DataRealizacao = Data;
            return model;
        }
    }
}
