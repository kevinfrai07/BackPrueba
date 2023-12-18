using Microsoft.AspNetCore.Builder;
using ProviderWks.Infraestructure.Middleware;

namespace ProviderWks.Infraestructure.Extension
{
    /// <summary>
    /// Extension del contenedor de configuracion de servicios
    /// </summary>
    public static class ConfigureContainer
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}