using Serilog;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Order.Policies;
using Order.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();
builder.Host.UseSerilog((ctx, logger) =>
{
    logger
    .WriteTo.Console()
    .MinimumLevel.Information();
});
// Add services to the container.
builder.Services.AddSingleton(new ServerPolicy().RetryForever);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Order API",
        Description = "An API for managing orders."
    });
});

builder.Services.AddDbContext<OrderContext>(opt =>
{
    var connString = builder.Configuration.GetConnectionString("OrderDatabase");
    opt.UseSqlServer(connString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.Run();
