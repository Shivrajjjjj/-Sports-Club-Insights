using FastApiOpenAiProcessor.Services;
using FastApiOpenAiProcessor.Models;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddScoped<FastApiService>();
builder.Services.AddScoped<OpenAiService>();

// Bind OpenAI key from config
builder.Services.Configure<OpenAiOptions>(builder.Configuration.GetSection("OpenAI"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();