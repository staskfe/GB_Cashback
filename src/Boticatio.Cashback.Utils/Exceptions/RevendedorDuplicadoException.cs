using System;
using System.Collections.Generic;
using System.Text;

namespace Boticatio.Cashback.Utils.Exceptions
{
    public class RevendedorDuplicadoException : Exception
    {
        public RevendedorDuplicadoException()
        {
        }

        public RevendedorDuplicadoException(string message)
            : base(message)
        {
        }

        public RevendedorDuplicadoException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
