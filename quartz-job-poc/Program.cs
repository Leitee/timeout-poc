using Quartz;
using quartz_job_poc.Jobs;
using quartz_job_poc.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configure in-memory repository
builder.Services.AddSingleton<InMemoryIdentificationRepository>();

// Configure Quartz
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
});

builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
});

// Register services
builder.Services.AddSingleton<IdentificationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Simple test endpoint
app.MapGet("/test", async (IdentificationService identificationService) =>
{
    var identification = await identificationService.CreateAsync("Test");
    return $"job scheduled";
});

app.MapGet("/test-multiple", async (IdentificationService identificationService) =>
{
    var results = new List<string>();
    for (int i = 0; i < 5; i++)
    {
        var identification = await identificationService.CreateAsync($"Test{i}");
        results.Add(identification.Id);
    }
    return $"Scheduled 5 jobs with IDs: {string.Join(", ", results)}";
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
