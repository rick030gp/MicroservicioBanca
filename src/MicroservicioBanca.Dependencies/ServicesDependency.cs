using MicroservicioBanca.Application;
using MicroservicioBanca.Application.Clientes;
using MicroservicioBanca.Application.Contracts.Clientes;
using MicroservicioBanca.Application.Contracts.Cuentas;
using MicroservicioBanca.Application.Contracts.Movimientos;
using MicroservicioBanca.Application.Cuentas;
using MicroservicioBanca.Domain.Clientes;
using MicroservicioBanca.Domain.Cuentas;
using MicroservicioBanca.Domain.Movimientos;
using MicroservicioBanca.Repository.Clientes;
using MicroservicioBanca.Repository.Cuentas;
using MicroservicioBanca.Repository.Movimientos;
using Microsoft.Extensions.DependencyInjection;

namespace MicroservicioBanca.Dependencies
{
    public static class ServicesDependency
    {
        public static void AddRegistration(this IServiceCollection services)
        {
            AddServicesDependency(services);
            AddRepositoriesDependency(services);
        }

        private static void AddServicesDependency(this IServiceCollection services)
        {
            services.AddTransient<IClienteAppService, ClienteAppService>();
            services.AddTransient<ClienteManager>();

            services.AddTransient<ICuentaAppService, CuentaAppService>();
            services.AddTransient<CuentaManager>();

            services.AddTransient<IMovimientoAppService, MovimientoAppService>();
            services.AddTransient<MovimientoManager>();
        }

        private static void AddRepositoriesDependency(this IServiceCollection services)
        {
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<ICuentaRepository, CuentaRepository>();
            services.AddTransient<IMovimientoRepository, MovimientoRepository>();
        }
    }
}
