using hnAPI.Application.UseCases;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hnAPI.Presentation.Endpoints
{
    /// <summary>
    /// Clase estática que define endpoints mínimos relacionados con historias de Hacker News.
    /// Registra rutas que delegan en los casos de uso de la capa de aplicación.
    /// </summary>
    public static class StoryEndpoints
    {
        /// <summary>
        /// Registra los endpoints relacionados con historias en el <see cref="IEndpointRouteBuilder"/>.
        /// Mapea rutas mínimas que invocan los casos de uso correspondientes.
        /// </summary>
        /// <param name="app">Instancia de <see cref="IEndpointRouteBuilder"/> usada para mapear las rutas.</param>
        public static void MapStoryEndpoints(this IEndpointRouteBuilder app)
        {
           app.MapGet("/tecnologynews", async ([FromServices] GetTopStoriesUseCase useCase, int n) =>
            {
               var stories = await useCase.ExecuteAsync(n);

               return Results.Ok(stories);
            })
            .WithName("GetTopStories");
        }
    }
}
