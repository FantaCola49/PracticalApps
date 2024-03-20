using Packt.Shared; // метод расширения AddNorthwindContext
using static System.Console;

namespace Northwind.Web;

public class Startup
{
    /// <summary>
    /// Сервисы настроек
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddNorthWindContext();
    }
    /// <summary>
    /// Настройка сайта при запуске программы
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            // включает промежуточное ПО для использования Hyper Strict-Transport-Security
            app.UseHsts();
        }
        app.UseRouting(); // начало маршрутизации конечной точки
        app.Use(async(HttpContext context, Func <Task> next)  => 
        {
            RouteEndpoint? Rep = context.GetEndpoint() as RouteEndpoint;
            if (Rep is not null)
            {
                WriteLine($"Endpoint name:{Rep.DisplayName}");
                WriteLine($"Endpoint route pattern: {Rep.RoutePattern.RawText}");
            }

            if (context.Request.Path == "/bonjour")
            {
                // в случае совпадения URL-пути становится возвращаемым делегатом, поэтому следующий делегат не вызывается
                await context.Response.WriteAsync("Bonjour Monde!");
                return;
            }

            // можно заменить запрос перед вызовом следующего делегата
            await next();
            // можно изменить ответ после вызова следующего делегата
        });
        // Перенаправление запросов на https
        app.UseHttpsRedirection();
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapGet("/hello", () => "Hello World!");
        });

    }
}
