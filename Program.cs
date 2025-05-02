using Microsoft.OpenApi.Models;
using MyTimeoutApp.Handlers;
using MyTimeoutApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Register our services
builder.Services.AddSingleton<TimeoutService>();
builder.Services.AddTransient<MyCommandHandler>();

// Add controllers
builder.Services.AddControllers();

// Add OpenAPI/Swagger support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
