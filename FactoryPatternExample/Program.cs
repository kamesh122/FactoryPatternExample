using FactoryPatternExample.DataAccess;
using FactoryPatternExample.Model;
using FactoryPatternExample.Service.Handlers;
using FactoryPatternExample.Service.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestDb"));

// Register Update Handlers
builder.Services.AddScoped<IUpdateHandler<User>, UserUpdateHandler>();
builder.Services.AddScoped<IUpdateHandler<Product>, ProductUpdateHandler>();

// Register Factory
builder.Services.AddScoped<UpdateHandlerFactory>();

// Add Controllers
builder.Services.AddControllers();
 
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();

