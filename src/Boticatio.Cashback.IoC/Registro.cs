﻿using Boticario.Cashback.Interface.Aplicação;
using Boticario.Cashback.Interface.Repositorio;
using Boticatio.Cashback.Application;
using Boticatio.Cashback.Repositorio;
using Microsoft.Extensions.DependencyInjection;

namespace Boticatio.Cashback.IoC
{
    public class Registro
    {

        public void RegistrarDependencias(IServiceCollection services)
        {
            //Application
            services.AddTransient<IRevendedorAplicação, RevendedorAplicação>();

            //Repositorio
            services.AddTransient<IRevendedorRepositorio, RevendedorRepositorio>();
        }

    }
}
