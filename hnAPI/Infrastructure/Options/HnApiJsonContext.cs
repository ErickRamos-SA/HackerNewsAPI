using System.Text.Json.Serialization;
using hnAPI.Domain.Entities;

namespace hnAPI.Infrastructure.Options
{
    [JsonSerializable(typeof(List<int>))]
    [JsonSerializable(typeof(Story))]
    [JsonSerializable(typeof(List<hnAPI.Application.DTOs.StoryDto>))]
    public partial class HnApiJsonContext : JsonSerializerContext
    {
    }
}
