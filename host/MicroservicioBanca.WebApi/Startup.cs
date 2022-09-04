using AutoMapper;
using MicroservicioBanca.Application;
using MicroservicioBanca.Application.Clientes;
using MicroservicioBanca.Application.Contracts.Clientes;
using MicroservicioBanca.Dependencies;
using MicroservicioBanca.Domain.Clientes;
using MicroservicioBanca.Repository.Clientes;
using MicroservicioBanca.Repository.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace MicroservicioBanca.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MicroservicioBancaDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Banca")));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MicroservicioBanca.WebApi", Version = "v1" });
            });

            // Register the service and implementation for the database context
            services.AddScoped<IMicroservicioBancaDbContext>(provider => provider.GetService<MicroservicioBancaDbContext>());
            //Inversion of control
            services.AddRegistration();

            // Mapper
            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new MicroservicioBancaAutoMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MicroservicioBanca.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
