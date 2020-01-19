using System;
using System.Collections.Generic;
using System.Text;

namespace Boticatio.Cashback.Utils.Exceptions
{
    public class UsuarioNaoEncontradoException : Exception
    {
        public UsuarioNaoEncontradoException()
        {
        }

        public UsuarioNaoEncontradoException(string message)
            : base(message)
        {
        }

        public UsuarioNaoEncontradoException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
