using Microsoft.AspNetCore.Mvc;
using Packt.Shared;
using Nothwind.WebApi.Repositories;

namespace Nothwind.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerRepository _repo;

    // конструктор внедряет репозиторий, зарегистрированный в Startup
    public CustomersController(ICustomerRepository repo)
    {  
        _repo = repo; 
    }

    // GET: api/customers
    // GET: api/customers/?country=[country]
    // всегда возвращается список клиентов (может быть пустым)
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Customer>))]
    public async Task<IEnumerable<Customer>> GetCustomers(string? country)
    {
        if (string.IsNullOrWhiteSpace(country))
            return await _repo.RetrieveAllAsync();
        
        else
            return (await _repo.RetrieveAllAsync()).Where(x => x.Country == country);
    }

    // GET: api/customers/[id]
    [HttpGet("{id}", Name = nameof(GetCustomer))]
    [ProducesResponseType(200, Type = typeof(Customer))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetCustomer(string id)
    {
        Customer? c = await _repo.RetrieveAsync(id);
        if (c == null)
            return NotFound(); // 404

        return Ok(c); // 200 - OK, с клиентом в теле
    }

    // POST: api/customers
    // BODY: Customer (JSON, XML)
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Customer))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] Customer c)
    {
        if (c== null) 
            return BadRequest(); // 400 code

        Customer addedCustomer = await _repo.CreateAsync(c);
        if (addedCustomer == null)
            return BadRequest("Не удалось создать пользователя в репозитории"); // некорректный запрос - 400 code
        else
        {
            return CreatedAtRoute( // 201
                routeName: nameof(GetCustomer),
                routeValues: new { id = addedCustomer.CustomerId.ToLower() },
                value: addedCustomer);
        }
    }

    // PUT: api/customers/[id]
    // BODY: Customer (JSON, XML)
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(string id, [FromBody] Customer c)
    {
        if (c == null ||
            id != c.CustomerId)
            return BadRequest("Указаны неверные аргументы"); // 404 - ресурс не найден

        id = id.ToUpper();
        c.CustomerId = c.CustomerId.ToUpper();
        
        Customer? existing = await _repo.RetrieveAsync(id);
        if (existing == null)
            return NotFound(); // 400 ресурс не найден

        await _repo.UpdateAsync(id, c);
        return new NoContentResult(); // 204 - отсутствует контент
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(string id)
    {

        // пример реализации класса ProblemDetails
        if (id.Equals("bad"))
        {
            ProblemDetails problemDetails = new()
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://localhost:5001/customers/failed-to-delete",
                Title = $"Customer ID {id} found but failed to delete.",
                Detail = "Ещё больше деталей вроде компании и т.д.",
                Instance = HttpContext.Request.Path
            };
            return BadRequest(problemDetails);
        }


        Customer? existing = await _repo.RetrieveAsync(id);
        if (existing == null)
        {
            return NotFound(); // 404 не найден
        }
        bool? deleted = await _repo.DeleteAsync(id);
        if (deleted.HasValue && deleted.Value)
        {
            return new NoContentResult(); // 204 контент отсутствует
        }
        else
        {
            return BadRequest(//400 - некорректный запрос
                              $"Клиент с id={id} не найден в базе данных!");
        }
    }


}
