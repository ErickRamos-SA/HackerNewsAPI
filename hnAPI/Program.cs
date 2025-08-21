
using System.Text.Json.Serialization;
using hnAPI.Presentation.Extensions;

var builder = WebApplication.CreateSlimBuilder(args);


builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, hnAPI.Infrastructure.Options.HnApiJsonContext.Default);
});

// Add Story services and use cases
builder.Services.AddStoryServices(builder.Configuration);
builder.Services.AddUseCases();

var app = builder.Build();

// Map endpoints
hnAPI.Presentation.Endpoints.StoryEndpoints.MapStoryEndpoints(app);

app.Run();
