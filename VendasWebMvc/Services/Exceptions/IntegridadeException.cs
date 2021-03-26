using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendasWebMvc.Services.Exceptions
{
    public class IntegridadeException : ApplicationException
    {
        public IntegridadeException(string mensagem) : base(mensagem)
        {

        }
    }
}
