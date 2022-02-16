using Odonto.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;   
using System.Security.Claims;


namespace Odonto.Services.Implementation
{
    public class User : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public User(IHttpContextAccessor accessor )
        {
            _accessor = accessor;
            Id = Int32.Parse(_accessor.HttpContext.User.FindFirstValue("Id"));
            Tipo = Int32.Parse(_accessor.HttpContext.User.FindFirstValue("Tipo"));
            ClinicaId = Int32.Parse(_accessor.HttpContext.User.FindFirstValue("ClinicaId"));
            Nome = _accessor.HttpContext.User.Identity.Name;
            Email = _accessor.HttpContext.User.FindFirstValue("Email");
            Consultorio = _accessor.HttpContext.User.FindFirstValue("Consultorio");
            Endereco = _accessor.HttpContext.User.FindFirstValue("Endereco");
            Img = _accessor.HttpContext.User.FindFirstValue("Img");
            Telefone  = _accessor.HttpContext.User.FindFirstValue("Telefone");
            Especialidade = _accessor.HttpContext.User.FindFirstValue("Especialidade");
            Conselho = _accessor.HttpContext.User.FindFirstValue("Conselho");
        }

        public int Id { get;  }
        public int ClinicaId { get; }
        public int Tipo { get; }
        public string Nome { get;  }
        public string Email { get;  }        
        public string Consultorio { get;  }
        public string Endereco { get; }
        public string Img { get; set; }
        public string Conselho { get; }
        public string Especialidade { get; }
        public string Telefone { get; set; }
    }
}
