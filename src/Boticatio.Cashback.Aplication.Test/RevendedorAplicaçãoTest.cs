using Boticario.Cashback.Interface.Repositorio;
using Boticatio.Cashback.Aplication.Test.Util;
using Boticatio.Cashback.Application;
using Boticatio.Cashback.Utils.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Boticatio.Cashback.Aplication.Test
{
    [TestClass]
    public class RevendedorAplicaçãoTest
    {

        public Mock<IRevendedorRepositorio> _revendedorRepositorio;
        public RevendedorAplicação app;
        public RevendedorAplicaçãoTest()
        {
            _revendedorRepositorio = new Mock<IRevendedorRepositorio>();
            app = new RevendedorAplicação(_revendedorRepositorio.Object);

        }

        [TestMethod]
        public void DeveAdicionarRevendedor()
        {
            //Arrange
            var revendedor = TestHelper.CreateRevendedor(1);
            _revendedorRepositorio.Setup(x => x.ValidarEmailDuplicado(It.IsAny<string>())).Returns(false);
            //Act
            app.Add(revendedor);

            //Assert
            Assert.IsTrue(revendedor.Senha == "k??s?4???k?N?Z?WG????/I?R???[K");// senha == "1"
            _revendedorRepositorio.Verify(x => x.ValidarEmailDuplicado(It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(RevendedorDuplicadoException))]
        public void DeveRetornarQueJaExisteRevendedor()
        {
            //Arrange
            var revendedor = TestHelper.CreateRevendedor(1);
            _revendedorRepositorio.Setup(x => x.ValidarEmailDuplicado(It.IsAny<string>())).Returns(true);
            //Act
            app.Add(revendedor);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void DeveValidarLogin()
        {
            //Arrange
            var revendedor = TestHelper.CreateRevendedor(1);

            var revendedorBd = TestHelper.CreateRevendedor(1);
            revendedorBd.Senha = revendedor.CriarHash();

            _revendedorRepositorio.Setup(x => x.GetRevendedorByEmail(It.IsAny<string>())).Returns(revendedorBd);
            //Act
            var result = app.ValidarLogin(revendedor);

            //Assert
            Assert.IsNotNull(result);
            _revendedorRepositorio.Verify(x => x.GetRevendedorByEmail(It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(UsuarioNaoEncontradoException))]
        public void DeveNaoEncontrarUsuario()
        {
            //Arrange
            var revendedor = TestHelper.CreateRevendedor(1);

            //Act
            app.ValidarLogin(revendedor);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(UsuarioNaoEncontradoException))]
        public void DeveRetonarSenhaErrada()
        {
            //Arrange
            var revendedor = TestHelper.CreateRevendedor(1);
            var revendedorBd = TestHelper.CreateRevendedor(1);
            revendedorBd.Senha = "adasdasdas";

            _revendedorRepositorio.Setup(x => x.GetRevendedorByEmail(It.IsAny<string>())).Returns(revendedorBd);

            //Act
            app.ValidarLogin(revendedor);

            //Assert
            Assert.Fail();
        }

    }
}
