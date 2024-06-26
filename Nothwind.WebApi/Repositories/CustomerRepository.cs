﻿using Microsoft.EntityFrameworkCore.ChangeTracking; // EntityEntry<T>
using Packt.Shared; // Customer
using System.Collections.Concurrent; // ConcurrentDictionary

namespace Nothwind.WebApi.Repositories;

public class CustomerRepository : ICustomerRepository
{
    // статические потокобезопасное поле словаря для кэширования клиентов
    private static ConcurrentDictionary<string, Customer>? customersCache;
    // используем поле контекст данных экземпляра, поскольку оно не должно кешироваться из-за внутреннего кеширования
    private NorthwindContext db;

    public CustomerRepository(NorthwindContext injectedContext)
    {
        db = injectedContext;
        // при инициализации программы создаём многопоточный словарь для экономии памяти
        if (customersCache is null)
        {
            customersCache = new ConcurrentDictionary<string, Customer>(
              db.Customers.ToDictionary(c => c.CustomerId));
        }
    }

    /// <summary>
    /// Асинхронно создаст клиента
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public async Task<Customer?> CreateAsync(Customer c)
    {
        // Нормализуация значения CustomerId в прописные
        c.CustomerId = c.CustomerId.ToUpper();

        EntityEntry<Customer> added = await db.Customers.AddAsync(c);
        int affected = await db.SaveChangesAsync();
        if (affected == 1)
        {
            if (customersCache is null)
            {
                return c;
            }
            return customersCache.AddOrUpdate(c.CustomerId, c, UpdateCache);
        }
        return null;
    }

    /// <summary>
    /// Асинхронно удалит клиента
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool?> DeleteAsync(string id)
    {
        id = id.ToUpper();
        //удаляем из БД
        Customer? c = db.Customers.Find(id);
        if (c is null) return null!;

        db.Customers.Remove(c);
        int affected = await db.SaveChangesAsync();
        // выходим если сохранить БД не получилось
        if (affected != 1) return false;

        //удаляем из кеша
        if (customersCache is not null)
        {
            if (customersCache.TryGetValue(id, out Customer cust))
            {
                return customersCache.TryRemove(id, out cust);
            }
        }
        return null!;
    }

    /// <summary>
    /// Асинхронно получит список всех клиентов
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<Customer>> RetrieveAllAsync()
    {
        // в целях экономии времени, используем кеш
        return Task.FromResult(customersCache is null ? Enumerable.Empty<Customer>() // если пусто
                                                       : customersCache.Values);     // если чё-то есть
    }

    /// <summary>
    /// Асинхронно получить клиента по Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<Customer?> RetrieveAsync(string id)
    {
        // в целях производительности извлекаем из кеша
        id = id.ToUpper();
        if (customersCache is null) return null!;
        customersCache.TryGetValue(id, out Customer? cust);
        return Task.FromResult(cust);
    }

    /// <summary>
    /// Асинхронно обновить по Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public async Task<Customer?> UpdateAsync(string id, Customer c)
    {
        id = id.ToUpper();
        c.CustomerId = c.CustomerId.ToUpper();
        // обновляем в БД
        db.Customers.Update(c);
        int affected = await db.SaveChangesAsync();
        if (affected == 1)
        {
            // обновляем в Кеш
            return UpdateCache(id, c);
        }
        else return null!;
    }

    /// <summary>
    /// Обновит клиента в многопоточнром словаре
    /// </summary>
    /// <param name="id"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    private Customer UpdateCache(string id, Customer c)
    {
        Customer? old;
        if (customersCache is not null)
        {
            if (customersCache.TryGetValue(id, out old))
            {
                if (customersCache.TryUpdate(id, c, old))
                {
                    return c;
                }
            }
        }
        return null!;
    }
}
