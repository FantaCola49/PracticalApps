using Packt.Shared; // метод расширения AddNorthwindContext

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
            app.UseHsts();
        }
        app.UseRouting(); // начало маршрутизации конечной точки
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
