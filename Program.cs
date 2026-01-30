using System;
using DISLAMS_Assignment.Application.Services;
using DISLAMS_Assignment.Infrastructure.Persistence;
using DISLAMS_Assignment.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("AttendanceDb")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AttendanceRepository>();

// Services
builder.Services.AddScoped<AttendanceService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
