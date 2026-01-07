using FastApiOpenAiProcessor.Services;
using FastApiOpenAiProcessor.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<FastApiService>();
builder.Services.AddScoped<OpenAiService>();
builder.Services.Configure<OpenAiOptions>(builder.Configuration.GetSection("OpenAI"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔹 Enable CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5265") // Blazor WASM origin
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// 🔹 Use CORS
app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
