using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using ServiceScheduling.Application.Interfaces;
using ServiceScheduling.Application.Services;
using ServiceScheduling.Domain.Interfaces;
using ServiceScheduling.Infra.Data;
using ServiceScheduling.Infra.Repositories;
using ServiceScheduling.Infra.Security;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
builder.Services.AddDbContext<ContextDatabase>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
