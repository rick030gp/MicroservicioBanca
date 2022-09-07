using MicroservicioBanca.Clientes;
using MicroservicioBanca.Cuentas;
using MicroservicioBanca.Movimientos;
using Microsoft.Extensions.DependencyInjection;

namespace MicroservicioBanca
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
