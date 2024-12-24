using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Get the connection string from appsettings.json
var connString = builder.Configuration.GetConnectionString("BookStoreAppDBconnection");

// Configure services
builder.Services.AddDbContext<BookStoreContext>(options =>
    options.UseSqlServer(connString)); // Add DbContext with SQL Server connection

builder.Services.AddControllers(); // Add support for controllers

// Add Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Serilog for logging
builder.Host.UseSerilog((ctx, lc) =>
    lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();       // Enable Swagger middleware
    app.UseSwaggerUI();     // Enable Swagger UI
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");    // Apply CORS policy
app.UseAuthorization();

app.MapControllers();       // Map controller routes

app.Run();