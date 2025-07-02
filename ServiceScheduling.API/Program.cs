using ServiceScheduling.Application.UseCases;
using Environment = System.Environment;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using ServiceScheduling.Application;
using ServiceScheduling.Application.Interfaces;
using ServiceScheduling.Domain.Interfaces;
using ServiceScheduling.Infra.Data;
using ServiceScheduling.Infra.Repositories;
using ServiceScheduling.Infra.Security;
using ServiceScheduling.Infrastructure;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString,
    ServerVersion.AutoDetect(connectionString), b => b.MigrationsAssembly("ServiceScheduling.API")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();