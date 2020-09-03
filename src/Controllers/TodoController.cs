using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TodoAppMicroservice.Models.Dtos;
using TodoAppMicroservice.Services;

namespace TodoAppMicroservice.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : Controller
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ILogger<TodoController> _logger;

        public TodoController(ITodoRepository todoRepository, ILogger<TodoController> logger)
        {
            _todoRepository = todoRepository;
            _logger = logger;
        }

        [HttpGet, Route("/api/Todos")]
        [ProducesResponseType(typeof(GetTodosResponse), 200)]
        [ProducesResponseType(typeof(ApiResult), 400)]
        public async Task<IActionResult> GetTodos()
        {
            try
            {
                var result = await _todoRepository.GetItemsAsync("SELECT * FROM c");

                var response = new GetTodosResponse
                { 
                    Todos = result
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception caught!");
                return BadRequest(new ApiResult { Error = "An error has occured" });
            }
        }

    }
}
