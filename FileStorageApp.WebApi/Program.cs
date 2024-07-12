var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Подключение к бд
/*var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));*/

// Подключение к minio
/*builder.Services.AddSingleton<IMinioClient>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new MinioClient()
        .WithEndpoint(config["Minio:Endpoint"])
        .WithCredentials(config["Minio:AccessKey"], config["Minio:SecretKey"])
        .Build();
});*/

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
