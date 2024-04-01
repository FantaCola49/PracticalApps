using Packt.Shared;

namespace Nothwind.WebApi.Repositories;

/// <summary>
/// CRUD - интерфейс для работы с БД Northwind
/// </summary>
public interface ICustomerRepository
{
    /// <summary>
    /// Асинхронно создаст клиента
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    Task<Customer> CreateAsync(Customer c);
    /// <summary>
    /// Асинхронно получит список всех клиентов
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Customer>> RetrieveAllAsync();
    /// <summary>
    /// Асинхронно получить клиента по Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Customer?> RetrieveAsync(string id);
    /// <summary>
    /// Асинхронно обновить по Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    Task<Customer?> UpdateAsync(string id, Customer c);
    /// <summary>
    /// Асинхронно удалит по Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool?> DeleteAsync(string id);
}
