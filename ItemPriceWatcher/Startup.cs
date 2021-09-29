using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ItemPriceWatcher.Data;
using ItemPriceWatcher.Services;
using ItemPriceWatcher.Data.Access;
using ItemPriceWatcher.Data.Models;
using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Cfg.MappingSchema;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace ItemPriceWatcher
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHostedService<PriceWatcherService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private void ConfigureNHibernate(IServiceCollection services)
        {
            var connectionString = string.Empty; // TODO: Figure out where to create the connection string!

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("Cannot initialize with an empty connection string.");
            }

            var mapper = new ModelMapper();
            mapper.AddMappings(typeof(Startup).Assembly.ExportedTypes);
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            var configuration = Fluently.Configure()
                                        .Database(PostgreSQLConfiguration.Standard.ConnectionString(c => c.Is(connectionString)))
                                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Startup>());

            for (int waitIteration = 0; waitIteration < 3; waitIteration++)
            {
                try
                {
                    var sessionFactory = configuration.BuildSessionFactory();
                    services.AddSingleton(sessionFactory);
                    services.AddScoped(factory => sessionFactory.OpenSession());
                    services.AddScoped<IMapperSession<WatchItem>, MapperSession<WatchItem>>();
                    services.AddScoped<IMapperSession<WatchItemLog>, MapperSession<WatchItemLog>>();
                    services.AddScoped<IMapperSession<Contact>, MapperSession<Contact>>();
                }
                catch (FluentNHibernate.Cfg.FluentConfigurationException)
                {
                    if (waitIteration == 2)
                    {
                        throw;
                    }

                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
                }
            }
        }
    }
}
