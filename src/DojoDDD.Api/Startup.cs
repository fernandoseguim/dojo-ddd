using DojoDDD.Api.Extensions.Cache;
using DojoDDD.Api.Extensions.DbContext;
using DojoDDD.Api.Extensions.DbContext.InMemory;
using DojoDDD.Api.Extensions.DbContext.RavenDb;
using DojoDDD.Api.Extensions.HealthCheck;
using DojoDDD.Api.Extensions.MassTransit;
using DojoDDD.Api.Extensions.Swagger;
using DojoDDD.Api.Extensions.Versioning;
using DojoDDD.Application.Abstractions.UseCases;
using DojoDDD.Application.UseCases;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Clients.Entities;
using DojoDDD.Domain.Products.Entities;
using DojoDDD.Domain.PuchaseOrders.Entities;
using DojoDDD.Domain.PuchaseOrders.Handlers;
using DojoDDD.Domain.PuchaseOrders.Rules.RuleBooks;
using DojoDDD.Infra.DbContext.InMemory;
using DojoDDD.Infra.DbContext.InMemory.Repositories;
using DojoDDD.Infra.DbContext.Models;
using DojoDDD.Infra.Providers.BusinessPeriod;
using DojoDDD.Infra.Serializers;
using Elastic.Apm.NetCoreAll;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
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

            services.AddVersioning();
            services.AddSwaggerDocumentation();

            services.AddHealthChecks(_configuration);

            services.AddRavenDbRepositories(_configuration);

            services.AddSingleton<RulesForRegisterNewPurchaseOrder>();
            services.AddScoped<IPurchaseOrderRegisterCommandHandler, PurchaseOrderRegisterCommandHandler>();

            services.AddScoped<IPurchaseOrderRegisterService, PurchaseOrderRegisterService>();
            services.AddScoped<IPurchaseOrderProcessingService, PurchaseOrderProcessingService>();

            services.Configure<BusinessPeriodOptions>(_configuration.GetSection("BusinessPeriod"));
            services.AddSingleton<IBusinessPeriodProvider, BusinessPeriodProvider>();

            services.AddMassTransitWithRabbitMq(_configuration);
            services.AddDistributedCache(_configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseAllElasticApm(_configuration);

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseHangfireDashboard();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseVersionedSwagger(provider);
        }
    }
}
