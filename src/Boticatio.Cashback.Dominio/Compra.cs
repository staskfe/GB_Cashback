
using System;



namespace Boticatio.Cashback.Dominio
{
    public class Compra
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public float Valor { get; set; }
        public DateTime Data { get; set; }

        public int Revendedor_Id { get; set; }
        public Revendedor Revendedor { get; set; }

        public int Status_Id { get; set; }
        public CompraStatus Status { get; set; }

        public int PorcentagemCashback { get; set; }
        public double ValorCashback { get; set; }


        public bool StatusEmValidação()
        {
            return this.Status_Id == (int)CompraStatusEnum.Validação;
        }

        public void AplicarCashback()
        {
            if (Valor <= 1000)
            {
                CalcularCashback(10);
            }
            else if (Valor > 1000 && Valor <= 1500)
            {
                CalcularCashback(15);
            }
            else if (Valor > 1500)
            {
                CalcularCashback(20);
            }
            else
            {
                //Erro
            }

          
        }

        public void CalcularCashback(int porcentagem)
        {
            PorcentagemCashback = porcentagem;
            ValorCashback = ((double)PorcentagemCashback / 100) * Valor;
        }
    }
}
