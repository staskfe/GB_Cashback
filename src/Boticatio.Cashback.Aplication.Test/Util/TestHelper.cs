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

        public static Compra CreateCompra(int id)
        {
            return new Compra
            {
                Id = id,
                Codigo = "fake",
                Data = DateTime.Now,
                Revendedor_Id = 1,
                Valor = 10,
            };
        }
    }
}
