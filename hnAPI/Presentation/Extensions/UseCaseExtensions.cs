using hnAPI.Application.UseCases;
using hnAPI.Domain.Repositories;

namespace hnAPI.Presentation.Extensions
{
    public static class UseCaseExtensions
    {
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
