using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Store.DatabaseContext;
using Store.Interfaces;
using Store.Services;
using Store.UniversalMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ContextDatabase>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("TestDBString")), ServiceLifetime.Scoped);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddSingleton<JWTTokensGenerator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => Results.Redirect("/swagger"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();