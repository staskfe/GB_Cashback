using Boticatio.Cashback.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boticatio.Cashback.Aplication.Test.Util
{
    public static class TestHelper
    {
        public static Revendedor CreateRevendedor(int id)
        {
            return new Revendedor
            {
                Id = id,
                CPF = "fake",
                Email = "fake",
                Nome = "fake",
                Senha = "1"
            };
        }
    }
}
