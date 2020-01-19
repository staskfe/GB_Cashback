using Boticario.Cashback.Interface.Aplicação;
using Boticario.Cashback.Interface.Repositorio;
using Boticatio.Cashback.Dominio;
using Boticatio.Cashback.Utils.Exceptions;
using System.Collections.Generic;

namespace Boticatio.Cashback.Application
{
    public class RevendedorAplicação : IRevendedorAplicação
    {
        public readonly IRevendedorRepositorio _revendedorRepositorio;
        public RevendedorAplicação(IRevendedorRepositorio revendedorRepositorio)
        {
            _revendedorRepositorio = revendedorRepositorio;
        }

        public void Add(Revendedor revendedor)
        {
            revendedor.Senha = revendedor.CriarHash();
            _revendedorRepositorio.Add(revendedor);
        }

        public IEnumerable<Revendedor> Listar()
        {
            return _revendedorRepositorio.Listar();
        }

        public Revendedor ValidarLogin(Revendedor revendedor)
        {
            var usuario = _revendedorRepositorio.GetRevendedorByEmail(revendedor.Email);
            if (usuario == null)
            {
                throw new UsuarioNaoEncontradoException("Não foi encontrado usuario com essa email");
            }
            if(usuario.Senha != revendedor.CriarHash())
            {
                throw new UsuarioNaoEncontradoException("Não foi encontrado usuario com essa senha");
            }

            return usuario;

        }
    }
}
