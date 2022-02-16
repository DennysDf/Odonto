using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odonto.Services.Interfaces
{
    public interface INotificacao
    {
        void Success(string message);
        void Error();
    }
}
