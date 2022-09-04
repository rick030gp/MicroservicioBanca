using MicroservicioBanca.Application.Clientes;
using MicroservicioBanca.Application.Contracts.Clientes;
using MicroservicioBanca.Domain.Clientes;
using MicroservicioBanca.Repository.Clientes;
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
        }

        private static void AddRepositoriesDependency(this IServiceCollection services)
        {
            services.AddTransient<IClienteRepository, ClienteRepository>();
        }
    }
}
