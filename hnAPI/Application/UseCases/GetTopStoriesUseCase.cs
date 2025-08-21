using hnAPI.Domain.Repositories;
using hnAPI.Application.DTOs;
using hnAPI.Domain.Entities;

namespace hnAPI.Application.UseCases
{
    public class GetTopStoriesUseCase
    {
        private readonly IStoryRepository _storyRepository;
        public GetTopStoriesUseCase(IStoryRepository storyRepository)
        {
            _storyRepository = storyRepository;
        }

        public async Task<List<StoryDto>> ExecuteAsync(int n)
        {
            var stories = await _storyRepository.GetTopStoriesAsync(n);
            return stories.Select(s => new StoryDto(
                string.IsNullOrWhiteSpace(s.Title) ? "(no title)" : s.Title,
                string.IsNullOrWhiteSpace(s.Url) ? string.Empty : s.Url,
                string.IsNullOrWhiteSpace(s.By) ? "anonymous" : s.By,
                s.Time > 0 ? DateTimeOffset.FromUnixTimeSeconds(s.Time).UtcDateTime : DateTime.UnixEpoch,
                s.Score,
                s.Kids?.Length ?? 0
            )).ToList();
        }
    }
}
