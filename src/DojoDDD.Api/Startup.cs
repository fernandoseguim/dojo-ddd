using DojoDDD.Api.Extensions.MassTransit;
using DojoDDD.Application.Abstractions.UseCases;
using DojoDDD.Application.UseCases;
using DojoDDD.Domain.Abstractions.Handlers;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Entities;
using DojoDDD.Domain.Handlers;
using DojoDDD.Domain.Rules.RuleBooks;
using DojoDDD.Infra.DbContext;
using DojoDDD.Infra.Providers.BusinessPeriod;
using DojoDDD.Infra.Repositories;
using DojoDDD.Infra.Serializers;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DojoDDD.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration) => _configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ContractResolver = new EmptyCollectionContractResolver();
                        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    });;

            services.AddLogging();

            services.AddSingleton<DataStore>();
            services.AddSingleton<IQueryableRepository<Client>, ClientRepository>();
            services.AddSingleton<IQueryableRepository<Product>, ProductsRepository>();
            services.AddSingleton<IQueryableRepository<PurchaseOrder>, PurchaseOrderRepository>();
            services.AddSingleton<IEntityRepository<PurchaseOrder>, PurchaseOrderRepository>();

            services.AddSingleton<RulesForRegisterNewPurchaseOrder>();
            services.AddScoped<IPurchaseOrderRegisterCommandHandler, PurchaseOrderRegisterCommandHandler>();

            services.AddScoped<IPurchaseOrderRegisterService, PurchaseOrderRegisterService>();
            services.AddScoped<IPurchaseOrderProcessingService, PurchaseOrderProcessingService>();

            services.Configure<BusinessPeriodOptions>(_configuration.GetSection("BusinessPeriod"));
            services.AddSingleton<IBusinessPeriodProvider, BusinessPeriodProvider>();

            services.AddMassTransitWithRabbitMq(_configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseHangfireDashboard();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
