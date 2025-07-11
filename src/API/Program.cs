using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext InMemory
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TasksDb"));

// Dependency Injection
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

// CORS (para el frontend Angular)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
    );
});

// ================= JWT CONFIGURATION =================
var jwtSecret = "EstaEsUnaClaveSuperSeguraJWT123456789!"; // ¡Cambia esto por una clave segura!
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "tu-app",
        ValidAudience = "tu-app",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
    };
});
// =====================================================

var app = builder.Build();

// Middleware global de manejo de excepciones
app.UseMiddleware<API.Middlewares.ExceptionMiddleware>();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

// ========== AUTENTICACIÓN Y AUTORIZACIÓN ==========
app.UseAuthentication(); // <-- ¡Agrega esto antes de UseAuthorization!
app.UseAuthorization();
// ===================================================

app.MapControllers();

app.Run();