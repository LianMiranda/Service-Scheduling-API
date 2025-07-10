using Environment = System.Environment;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using ServiceScheduling.Application;
using ServiceScheduling.Infrastructure.Aws;
using ServiceScheduling.Infrastructure.Configurations;
using ServiceScheduling.Infrastructure.Data;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.Configure<AwsS3Settings>(options =>
{
    options.AccessKey = Environment.GetEnvironmentVariable("AWS_KEY_ID") ?? string.Empty;
    options.SecretKey = Environment.GetEnvironmentVariable("AWS_KEY_SECRET") ?? string.Empty;
    options.Region = Environment.GetEnvironmentVariable("AWS_REGION") ?? string.Empty;
    options.BucketName = Environment.GetEnvironmentVariable("AWS_BUCKET") ?? string.Empty;
});

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