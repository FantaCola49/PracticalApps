namespace Northwind.Web;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseHsts();
        }
        app.UseRouting(); // начало маршрутизации конечной точки
        app.UseHttpsRedirection();
        app.UseEndpoints(endpoints =>
        { 
            endpoints.MapGet("/", () => "Hello World!");
        });

    }
}
