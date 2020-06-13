using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using WatchItemData.WatchItemAccess.ORM.Sessions;

namespace WatchItemData.WatchItemAccess.ORM
{
    public static class NHibernateExtensions
    {
        public static IServiceCollection AddNHibernate<T>(this IServiceCollection services, string connectionString)
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(typeof(NHibernateExtensions).Assembly.ExportedTypes);
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            var configuration = Fluently.Configure()
                                        .Database(MySQLConfiguration.Standard.ConnectionString(c => c.Is(connectionString)))
                                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<WatchItem>());

            var sessionFactory = configuration.BuildSessionFactory();

            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => sessionFactory.OpenSession());
            services.AddScoped<IMapperSession<T>, MapperSession<T>>();

            return services;
        }
    }
}