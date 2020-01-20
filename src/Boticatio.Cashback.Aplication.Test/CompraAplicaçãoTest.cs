using Boticario.Cashback.Interface.Repositorio;
using Boticatio.Cashback.Aplication.Test.Util;
using Boticatio.Cashback.Application;
using Boticatio.Cashback.Dominio;
using Boticatio.Cashback.Utils.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Boticatio.Cashback.Aplication.Test
{
    [TestClass]
    public class CompraAplicaçãoTest
    {

        public Mock<ICompraRepositorio> _compraRepositorio;
        public Mock<IRevendedorRepositorio> _revendedorRepositorio;
        public CompraAplicação app;
        public CompraAplicaçãoTest()
        {
            _compraRepositorio = new Mock<ICompraRepositorio>();
            _revendedorRepositorio = new Mock<IRevendedorRepositorio>();
            app = new CompraAplicação(_compraRepositorio.Object, _revendedorRepositorio.Object);

        }

        [TestMethod]
        public void DeveCadastrarCompraStatusValidacaoCashbackMenosQueMil()
        {
            //Arrange
            var compra = TestHelper.CreateCompra(1);
            _revendedorRepositorio.Setup(x => x.ChecarValidaçãoCPF(It.IsAny<int>())).Returns(false);

            //Act
            var result = app.Add(compra);

            //Assert
            Assert.IsTrue(result.Status_Id == (int)CompraStatusEnum.Validação);
            Assert.IsTrue(result.PorcentagemCashback == 10);
            Assert.IsTrue(result.ValorCashback == 1);
        }

        [TestMethod]
        public void DeveCadastrarCompraStatusValidacaoCashbackMenosQueMilEQuinhentos()
        {
            //Arrange
            var compra = TestHelper.CreateCompra(1);
            compra.Valor = 1100;

            _revendedorRepositorio.Setup(x => x.ChecarValidaçãoCPF(It.IsAny<int>())).Returns(false);

            //Act
            var result = app.Add(compra);

            //Assert
            _revendedorRepositorio.Verify(x => x.ChecarValidaçãoCPF(It.IsAny<int>()), Times.Once());
            Assert.IsTrue(result.Status_Id == (int)CompraStatusEnum.Validação);
            Assert.IsTrue(result.PorcentagemCashback == 15);//15%
            Assert.IsTrue(result.ValorCashback == 165); //15% de 1100
        }

        [TestMethod]
        public void DeveCadastrarCompraStatusValidacaoCashbackMaisQueMilEQuinhentos()
        {
            //Arrange
            var compra = TestHelper.CreateCompra(1);
            compra.Valor = 1600;

            _revendedorRepositorio.Setup(x => x.ChecarValidaçãoCPF(It.IsAny<int>())).Returns(false);

            //Act
            var result = app.Add(compra);

            //Assert
            _revendedorRepositorio.Verify(x => x.ChecarValidaçãoCPF(It.IsAny<int>()), Times.Once());
            Assert.IsTrue(result.Status_Id == (int)CompraStatusEnum.Validação);
            Assert.IsTrue(result.PorcentagemCashback == 20);//20%
            Assert.IsTrue(result.ValorCashback == 320); //20% de 1100
        }

        [TestMethod]
        public void DeveCadastrarCompraStatusAprovadoCashbackMaisQueMilEQuinhentos()
        {
            //Arrange
            var compra = TestHelper.CreateCompra(1);

            _revendedorRepositorio.Setup(x => x.ChecarValidaçãoCPF(It.IsAny<int>())).Returns(true);

            //Act
            var result = app.Add(compra);

            //Assert
            _revendedorRepositorio.Verify(x => x.ChecarValidaçãoCPF(It.IsAny<int>()), Times.Once());
            Assert.IsTrue(result.Status_Id == (int)CompraStatusEnum.Aprovado);
            Assert.IsTrue(result.PorcentagemCashback == 10);
            Assert.IsTrue(result.ValorCashback == 1);
        }
        [TestMethod]
        [ExpectedException(typeof(CashbackErrorException))]
        public void DeveRetornarErroAoCalcularCashback()
        {
            //Arrange
            var compra = TestHelper.CreateCompra(1);
            compra.Valor = 0;
            _revendedorRepositorio.Setup(x => x.ChecarValidaçãoCPF(It.IsAny<int>())).Returns(true);

            //Act
            app.Add(compra);

            //Assert
            Assert.Fail();
        }


        [TestMethod]
        public void DeveEditarCompra()
        {
            //Arrange
            var compra = TestHelper.CreateCompra(1);
            compra.Status_Id = (int)CompraStatusEnum.Validação;

            //Act
            var result = app.Editar(compra);

            //Assert
            Assert.IsTrue(result.Status_Id == (int)CompraStatusEnum.Validação);
            Assert.IsTrue(result.PorcentagemCashback == 10);
            Assert.IsTrue(result.ValorCashback == 1);
        }


        [TestMethod]
        [ExpectedException(typeof(StatusErrorException))]
        public void DeveRetornarExcecaoAoEditarCompraStatusAprovado()
        {
            //Arrange
            var compra = TestHelper.CreateCompra(1);
            compra.Status_Id = (int)CompraStatusEnum.Aprovado;

            //Act
            app.Editar(compra);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void DeveRemoverCompra()
        {
            //Arrange
            var compra = TestHelper.CreateCompra(1);
            compra.Status_Id = (int)CompraStatusEnum.Validação;
            _compraRepositorio.Setup(x => x.GetPeloId(It.IsAny<int>())).Returns(compra);

            //Act
            app.Remover(compra.Id);

            //Assert
            _compraRepositorio.Verify(x => x.GetPeloId(It.IsAny<int>()), Times.Once());
        }


        [TestMethod]
        [ExpectedException(typeof(StatusErrorException))]
        public void DeveRetornarExcecaoAoRemoverCompraStatusAprovado()
        {
            //Arrange
            var compra = TestHelper.CreateCompra(1);
            compra.Status_Id = (int)CompraStatusEnum.Aprovado;

            _compraRepositorio.Setup(x => x.GetPeloId(It.IsAny<int>())).Returns(compra);
            //Act
            app.Remover(compra.Id);

            //Assert
            Assert.Fail();
        }


       

    }
}
