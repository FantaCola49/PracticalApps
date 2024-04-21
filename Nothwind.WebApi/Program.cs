using Microsoft.AspNetCore.Mvc.Formatters;
using Packt.Shared;
using Nothwind.WebApi.Repositories;
using static System.Console;
using Swashbuckle.AspNetCore.SwaggerUI; // SubmitMethod
using Microsoft.AspNetCore.HttpLogging; // HttpLoggingFields

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("https://localhost:5002/");

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
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc("v1",
        new()
        {
            Title = "Northwind Service WebApi",
            Version = "v1",
        });
});
builder.Services.AddNorthWindContext(); // контекст Northwind Db
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>(); // зависимость с ограниченной областью действия
// CORS - Cross-Origin Resource Sharing
builder.Services.AddCors();
// логгирование http - запросов
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.All;
    options.RequestBodyLogLimit = 4096; // по умолчанию 32 Кбайт
    options.ResponseBodyLogLimit = 4096; // по умолчанию 32 Кбайт
});
var app = builder.Build();

// Конфигурация HTTP - конвейера
app.UseCors(configurePolicy: options =>
{
    options.WithMethods("GET", "POST", "PUT", "DELETE");
    options.WithOrigins("https://localhost:5001"); // допускается только запросы от клиента MVC
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json",
                "Northwind Service API Version 1");
            c.SupportedSubmitMethods(new[]
            {
                SubmitMethod.Get, SubmitMethod.Post,
                SubmitMethod.Put, SubmitMethod.Delete
            });
        });
    // оператор для добавления HTTP - протоколирования перед вызовом маршрутизации
    app.UseHttpLogging();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
