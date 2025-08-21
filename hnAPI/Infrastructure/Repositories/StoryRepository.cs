using hnAPI.Domain.Entities;
using hnAPI.Domain.Repositories;
using System.Net.Http.Json;

namespace hnAPI.Infrastructure.Repositories
{

    public class StoryRepository : IStoryRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public StoryRepository(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }


        public async Task<List<Story>> GetTopStoriesAsync(int n)
        {
            var ids = await _httpClient.GetFromJsonAsync($"{_baseUrl}beststories.json", hnAPI.Infrastructure.Options.HnApiJsonContext.Default.ListInt32);
            if (ids == null) return new List<Story>();

            var stories = new List<Story?>();
            var storiesLock = new object();
            await Parallel.ForEachAsync(ids, new ParallelOptions { MaxDegreeOfParallelism = 10 }, async (id, _) =>
            {
                var story = await GetStoryAsync(id);
                if (story != null)
                {
                    lock (storiesLock)
                    {
                        stories.Add(story);
                    }
                }
            });
            return stories
                .OrderByDescending(s => s!.Score)
                .Take(n)
                .ToList()!;
        }

        private async Task<Story?> GetStoryAsync(int id)
        {
            try
            {
                var url = $"{_baseUrl}item/{id}.json";
                var response = await _httpClient.GetFromJsonAsync(url, hnAPI.Infrastructure.Options.HnApiJsonContext.Default.Story);
                return response;
            }
            catch
            {
                return null;
            }
        }
    }
}
