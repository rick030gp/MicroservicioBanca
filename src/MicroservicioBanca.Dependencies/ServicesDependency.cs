using MicroservicioBanca.Application.Clientes;
using MicroservicioBanca.Application.Contracts.Clientes;
using MicroservicioBanca.Domain.Clientes;
using MicroservicioBanca.Repository.Clientes;
using MicroservicioBanca.Repository.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MicroservicioBanca.Dependencies
{
    public static class ServicesDependency
    {
        public static IServiceCollection AddRegistration(this IServiceCollection services)
        {
            AddServicesDependency(services);
            AddRepositoriesDependency(services);

            return services;
        }

        private static IServiceCollection AddServicesDependency(this IServiceCollection services)
        {
            services.AddTransient<IClienteAppService, ClienteAppService>();
            return services;
        }

        private static IServiceCollection AddRepositoriesDependency(this IServiceCollection services)
        {
            services.AddTransient<IMicroservicioBancaDbContext, MicroservicioBancaDbContext>();
            services.AddTransient<IClienteRepository, ClienteRepository>();
            return services;
        }
    }
}
