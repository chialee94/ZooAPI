using Microsoft.EntityFrameworkCore;
using ZooAPI.Data;
using ZooAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();

// Register DbContext
builder.Services.AddDbContext<ZooContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ZooDb")));

// Allow Angular to call API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowAll");

app.MapControllers();

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