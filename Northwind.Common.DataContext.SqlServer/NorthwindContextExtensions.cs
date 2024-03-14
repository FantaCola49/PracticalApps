using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Packt.Shared
{
    public static class NorthwindContextExtensions
    {
        /// <summary>
        /// Добавит NorthwindContext в специализированную коллекцию сервисов. Использует провайдер SqlServer
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString">Строка подключения</param>
        /// <returns></returns>
        public static IServiceCollection AddNorthWindContext(this IServiceCollection services,
            string connectionString = "Data Source=.; TrustServerCertificate=true; Initial Catalog=Northwind; Integrated Security = true; MultipleActiveResultsets=true")
        {
            services.AddDbContext<NorthwindContext>(options =>
            options.UseSqlServer(connectionString));

            return services;
        }
    }
}
