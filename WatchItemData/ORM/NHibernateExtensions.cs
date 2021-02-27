using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;

namespace WatchItemData.ORM
{
    public static class NHibernateExtensions
    {
        public static IServiceCollection AddNHibernate<T>(this IServiceCollection services, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("Cannot initialize with an empty connection string.");
            }

            var mapper = new ModelMapper();
            mapper.AddMappings(typeof(NHibernateExtensions).Assembly.ExportedTypes);
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            var configuration = Fluently.Configure()
                                        .Database(MySQLConfiguration.Standard.ConnectionString(c => c.Is(connectionString)))
                                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<WatchItem>());

            ConnectSession<T>(configuration, services);

            return services;
        }

        private static void ConnectSession<T>(FluentConfiguration configuration, IServiceCollection services)
        {
            var connected = false;
            for (int waitIteration = 0; waitIteration < 3 && !connected; waitIteration++)
            {
                try
                {
                    var sessionFactory = configuration.BuildSessionFactory();
                    services.AddSingleton(sessionFactory);
                    services.AddScoped(factory => sessionFactory.OpenSession());
                    services.AddScoped<IMapperSession<T>, MapperSession<T>>();
                    connected = true;
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