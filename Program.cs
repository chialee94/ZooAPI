using Microsoft.EntityFrameworkCore;
using ZooAPI.Data;
using ZooAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();

// Register DbContext
builder.Services.AddDbContext<ZooContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ZooDb")));

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Allow Angular to call API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// Enable Swagger in Development (or always, if you prefer)
app.UseSwagger();
app.UseSwaggerUI();

// Enable CORS
app.UseCors("AllowAll");

// Map controllers
app.MapControllers();

// Seed database if empty
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ZooContext>();
    if (!context.Animals.Any())
    {
        context.Animals.AddRange(
            new Animal { Name = "Leo", Species = "Lion", HealthStatus = "Healthy" },
            new Animal { Name = "Ella", Species = "Elephant", HealthStatus = "Healthy" },
            new Animal { Name = "Milo", Species = "Monkey", HealthStatus = "Sick" }
        );
        context.SaveChanges();
    }
}

app.Run();