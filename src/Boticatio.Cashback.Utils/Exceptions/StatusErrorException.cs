using System;
using System.Collections.Generic;
using System.Text;

namespace Boticatio.Cashback.Utils.Exceptions
{
    public class StatusErrorException : Exception
    {
        public StatusErrorException()
        {
        }

        public StatusErrorException(string message)
            : base(message)
        {
        }

        public StatusErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
