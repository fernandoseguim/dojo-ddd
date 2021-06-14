using DojoDDD.Application;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Aggregates;
using DojoDDD.Domain.Entities;
using DojoDDD.Infra.DbContext;
using DojoDDD.Infra.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DojoDDD.Api
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration) => _configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddLogging();

            services.AddSingleton<DataStore>();
            services.AddSingleton<IQueryableRepository<Client>, ClientRepository>();
            services.AddSingleton<IQueryableRepository<Product>, ProductsRepository>();
            services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
            services.AddTransient<IEntityRepository<PurchaseOrder>, OrdemCompraRepositorio>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
