using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Store.DatabaseContext;
using Store.Interfaces;
using Store.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Store", Version = "v1" });

    // Сортировка: User → Product → всё остальное
    c.OrderActionsBy(apiDesc =>
    {
        var controller = apiDesc.ActionDescriptor.RouteValues["controller"];
        return controller switch
        {
            "User" => "1",
            "Product" => "2",
            _ => "3"
        };
    });
});
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ContextDatabase>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("TestDBString")), ServiceLifetime.Scoped);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();

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