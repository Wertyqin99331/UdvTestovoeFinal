using Microsoft.EntityFrameworkCore;
using Serilog;
using VkPostAnalyzer.Data;
using VkPostAnalyzer.Services;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();
var accessToken = Environment.GetEnvironmentVariable("VK_ACCESS_TOKEN");
if (accessToken == null)
	throw new Exception("Токен не задан");

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

Log.Logger = new LoggerConfiguration()
	.WriteTo.File("logs/vk-analysis-.log", rollingInterval: RollingInterval.Day)
	.CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

builder.Services.AddTransient<LetterStatisticsAnalyzerService>();
builder.Services.AddTransient<VkService>(_ => new VkService(accessToken));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	await dbContext.Database.MigrateAsync();
}

app.Run();
