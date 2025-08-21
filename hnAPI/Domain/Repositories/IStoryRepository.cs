using hnAPI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hnAPI.Domain.Repositories
{
    public interface IStoryRepository
    {
        Task<List<Story>> GetTopStoriesAsync(int n);
    }
}
