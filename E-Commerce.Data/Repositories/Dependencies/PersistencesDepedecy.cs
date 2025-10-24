using E_Commerce.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Data.Repositories.Dependencies
{
    public static class PersistencesDepedecy
    {
        public static void AddPersistencesLayerIoc(this IServiceCollection service, IConfiguration config)
        {
            #region Contexts
            if (config.GetValue<bool>("UseInMemoryDatabase"))
            {
                service.AddDbContext<E_commenceContext>(opt
                                                          => opt.UseInMemoryDatabase("AppDb"));
            }
            else
            {
                var connectionString = config.GetConnectionString("ConnectionDb");
                service.AddDbContext<E_commenceContext>(
                  (serviceProvider, opt) =>
                  {
                      opt.EnableSensitiveDataLogging();
                      opt.UseSqlServer(connectionString,
                      m => m.MigrationsAssembly(typeof(E_commenceContext).Assembly.FullName));
                  },
                    contextLifetime: ServiceLifetime.Scoped,
                    optionsLifetime: ServiceLifetime.Scoped
                 );
            }
            #endregion
        }
    }
}
