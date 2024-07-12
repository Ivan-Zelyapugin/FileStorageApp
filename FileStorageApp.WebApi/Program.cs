using FileStorageApp.Application.Common.Mappings;
using FileStorageApp.Application.Interfaces;
using FileStorageApp.Application;
using FileStorageApp.Persistence;
using System.Reflection;
using FileStorageApp.WebApi.Migrations;
using Minio;
using FileStorageApp.Application.Files.Commands.UploadFile;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMinioClient>(sp =>
    new MinioClient()
        .WithEndpoint("minio:9000")
        .WithCredentials("9lcK2gU7KbpWxPMZ4tdj", "hh4yZCdBP9f5hdtkQRz3AtKOteq2Wzjatx7qcrHf")
        .Build());

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IFileRepository).Assembly));
});

builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddScoped<IMigrationService, MigrationService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var migrationService = services.GetRequiredService<IMigrationService>();
    migrationService.RunMigrations();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseCors("AllowAll");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Urls.Add("http://*:90");

app.Run();
