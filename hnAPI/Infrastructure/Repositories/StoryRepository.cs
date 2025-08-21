using hnAPI.Domain.Entities;
using hnAPI.Domain.Repositories;
using System.Net.Http.Json;

namespace hnAPI.Infrastructure.Repositories
{

    /// <summary>
    /// Repositorio que obtiene historias de Hacker News mediante HTTP.
    /// Implementa <see cref="IStoryRepository"/> y utiliza un <see cref="HttpClient"/> configurado.
    /// </summary>
    public class StoryRepository : IStoryRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        /// <summary>
        /// Crea una nueva instancia de <see cref="StoryRepository"/>.
        /// </summary>
        /// <param name="httpClient">Cliente HTTP usado para realizar las solicitudes a la API.</param>
        /// <param name="baseUrl">URL base de la API de Hacker News (por ejemplo: "https://hacker-news.firebaseio.com/v0/").</param>
        public StoryRepository(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }


        /// <summary>
        /// Recupera las mejores historias y devuelve las 'n' con mayor puntuación.
        /// </summary>
        /// <param name="n">Cantidad de historias a retornar.</param>
        /// <returns>Lista de <see cref="Story"/> ordenadas por puntuación. Puede ser una lista vacía si no se obtienen ids.</returns>
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

        /// <summary>
        /// Obtiene la información de una historia por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la historia en la API de Hacker News.</param>
        /// <returns>Una instancia de <see cref="Story"/> o null si ocurre un error o no existe.</returns>
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
