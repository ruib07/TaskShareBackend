using Microsoft.EntityFrameworkCore;
using TaskShare.EntityFramework;

namespace TaskShare.API.Configurations.Persistance
{
    public static class PersistanceConfiguration
    {
        public static void AddCustomDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("TaskShareDb");

            services.AddDbContext<TaskShareDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlServerOptions =>
                {
                    var assembly = typeof(TaskShareDbContext).Assembly;
                    var assemblyName = assembly.GetName();

                    sqlServerOptions.MigrationsAssembly(assemblyName.Name);
                    sqlServerOptions.EnableRetryOnFailure(
                        maxRetryCount: 2,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });
            });
        }
    }
}
