using System.ComponentModel;

namespace Boticatio.Cashback.Dominio
{
    public class CompraStatus
    {
        public int Id { get; set; }
        public string Descrição { get; set; }


    }
    public enum CompraStatusEnum
    {
        [Description("Em validação")]
        Validação = 1,
        [Description("Aprovado")]
        Aprovado = 2,
        [Description("Reprovado")]
        Reprovado = 3,
    }

}
