using hnAPI.Application.UseCases;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hnAPI.Presentation.Endpoints
{
    public static class StoryEndpoints
    {
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
