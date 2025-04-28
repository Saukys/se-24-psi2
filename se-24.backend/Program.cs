using Microsoft.EntityFrameworkCore;
using se_24.backend.Data;
using se_24.backend.Services;
using se_24.backend.src.FileManipulation;
using se_24.backend.src.Interfaces;
using se_24.backend.src.Repositories;
using se_24.shared.src.Games.FinderGame;
using se_24.shared.src.Games.ReadingGame;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure database
builder.Services.AddDbContext<se_24.backend.Data.AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services
builder.Services.AddScoped<IAuthService, AuthService>();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddScoped<IScoreRepository, ScoreRepository>();
builder.Services.AddScoped<IReadingLevelRepository, ReadingLevelRepository>();
builder.Services.AddScoped<IFinderLevelRepository, FinderLevelRepository>();
builder.Services.AddScoped<ILevelFilesRepository, LevelFilesRepository>();
builder.Services.AddScoped<ILevelLoader<Level>, LevelLoader<Level>>();
builder.Services.AddScoped<ILevelLoader<ReadingLevel>, LevelLoader<ReadingLevel>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();


app.Run();


