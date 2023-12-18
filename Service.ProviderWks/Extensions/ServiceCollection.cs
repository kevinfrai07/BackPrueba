using Microsoft.Extensions.DependencyInjection;
using ProviderWks.Persistence;

namespace Service.ProviderWks.Extensions
{
    public static class ServiceCollection
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollection).Assembly));
        }
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        }
    }
}
