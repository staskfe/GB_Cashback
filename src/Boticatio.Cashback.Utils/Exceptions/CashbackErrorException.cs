using System;
using System.Collections.Generic;
using System.Text;

namespace Boticatio.Cashback.Utils.Exceptions
{
    public class CashbackErrorException : Exception
    {
        public CashbackErrorException()
        {
        }

        public CashbackErrorException(string message)
            : base(message)
        {
        }

        public CashbackErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
