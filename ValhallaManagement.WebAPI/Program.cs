using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ValhallaManagement.Application.Interfaces;
using ValhallaManagement.Application.Interfaces.Impl;
using ValhallaManagement.Application.UseCase;
using ValhallaManagement.Core.Interfaces;
using ValhallaManagement.Core.Services;
using ValhallaManagement.Infrastructure.Services;
using ValhallaManagement.Infrastructure.Data;
using System;
using ValhallaManagement.Application.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Configurar Entity Framework Core con SQLite
builder.Services.AddDbContext<ApDbContext>(options =>
    options.UseSqlite("Data Source=Vikingos.db"));

// Registrar ICacheService
builder.Services.AddSingleton<ICacheService>(sp =>
    new SQLiteCacheService("Data Source=cache.db"));

// Registrar el repositorio de vikingos
builder.Services.AddScoped<IVikingoRepository, VikingoRepository>();

// Registrar el servicio de vikingos
builder.Services.AddScoped<IVikingoService, VikingoService>();

// Registrar los casos de uso de vikingos
builder.Services.AddScoped<CrearVikingoUseCase>();
builder.Services.AddScoped<ObtenerVikingosUseCase>();
builder.Services.AddScoped<ObtenerVikingoPorIdUseCase>();
builder.Services.AddScoped<ActualizarVikingoUseCase>();
builder.Services.AddScoped<EliminarVikingoUseCase>();

// Configurar controladores
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
