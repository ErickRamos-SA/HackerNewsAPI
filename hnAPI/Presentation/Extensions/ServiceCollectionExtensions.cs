using hnAPI.Infrastructure.Options;
using hnAPI.Infrastructure.Repositories;
using hnAPI.Domain.Repositories;

namespace hnAPI.Presentation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registra los servicios relacionados con las historias de Hacker News.
        /// </summary>
        /// <param name="services">Colección de servicios donde se registrarán los servicios.</param>
        /// <param name="configuration">Configuración de la aplicación.</param>
        /// <returns>La colección de servicios.</returns>
        public static IServiceCollection AddStoryServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<StoryApiOptions>(configuration.GetSection("StoryApi"));
            services.AddHttpClient();
            services.AddTransient<IStoryRepository>(sp =>
            {
                var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                var options = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<StoryApiOptions>>().Value;
                return new StoryRepository(httpClientFactory.CreateClient(), options.BaseUrl);
            });
            return services;
        }
    }
}
