using Microsoft.AspNetCore.Mvc.Formatters;
using Packt.Shared;
using static System.Console;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    WriteLine("Стандартные форматы отображения:");
    foreach (IOutputFormatter formatter in options.OutputFormatters)
    {
        OutputFormatter? mediaFormatter = formatter as OutputFormatter;
        if (mediaFormatter == null)
        {
            WriteLine($"{formatter.GetType().Name}");
        }
        else // класс форматтера вывода с поддерживаемыми медиаформатами
        {
            WriteLine($"{mediaFormatter.GetType().Name}, Media types: {string.Join(",", mediaFormatter.SupportedMediaTypes)}");
        }
    }
})
    .AddXmlDataContractSerializerFormatters()
    .AddXmlSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddNorthWindContext(); // контекст Northwind Db

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

app.Run();
