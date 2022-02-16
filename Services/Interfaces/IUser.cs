using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odonto.Services.Interfaces
{
    public interface IUser
    {
        int Id { get; }
        int Tipo { get;  }
        string Nome { get; }
        string Consultorio { get; }
        string Endereco { get;  }
        string Img { get;  }
        int ClinicaId { get; }
        string Telefone { get; }
        string Especialidade { get; }
        string Conselho { get; }
    }
}
