using Academy.Cart.Presentation.Modules;
using Academy.Cart.Infrastructure.Persistence;
using Academy.Cart.Infrastructure.Repositories;
using Academy.Cart.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Config BD
builder.Services.AddDbContext<CartDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CartConnection")));

// Dependencies
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddMediatR(Assembly.Load("Academy.Cart.Application"));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPresentationEndpoints();

app.Run();
