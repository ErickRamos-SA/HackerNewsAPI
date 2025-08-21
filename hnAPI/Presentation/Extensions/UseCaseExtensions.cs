using hnAPI.Application.UseCases;
using hnAPI.Domain.Repositories;

namespace hnAPI.Presentation.Extensions
{
    /// <summary>
    /// Clase estática que contiene métodos de extensión para registrar casos de uso (UseCases)
    /// en el contenedor de dependencias de la aplicación.
    /// </summary>
    public static class UseCaseExtensions
    {
        /// <summary>
        /// Registra los casos de uso en el <see cref="IServiceCollection"/>.
        /// Añade las dependencias necesarias para los UseCases.
        /// </summary>
        /// <param name="services">Colección de servicios donde se añadirán las dependencias.</param>
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<GetTopStoriesUseCase>(sp =>
            {
                var repo = sp.GetRequiredService<IStoryRepository>();
                return new GetTopStoriesUseCase(repo);
            });
            return services;
        }
    }
}
