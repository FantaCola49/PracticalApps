using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Packt.Shared
{
    public static class NorthwindContextExtensions
    {
        public static IServiceCollection AddNorthWindContext(this IServiceCollection services, string relativePath = "..")
        {
            string databasePath = Path.Combine(relativePath, "Northwind.db");
            services.AddDbContext<NorthwindContext>(options =>
            options.UseSqlite($"Data Source ={databasePath}"));

            return services;
        }
    }
}
