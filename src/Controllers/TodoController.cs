using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TodoAppMicroservice.Models;
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
        [ProducesResponseType(typeof(GetTodosResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
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

        [HttpPost, Route("/api/Todos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTodo(CreateTodoRequest createTodoRequest)
        {
            try
            {
                Todo todo = new Todo
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = createTodoRequest.Name,
                    Description = createTodoRequest.Description,
                    IsComplete = createTodoRequest.IsComplete
                };

                await _todoRepository.AddItemAsync(todo);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception caught!");
                return BadRequest(new ApiResult { Error = "An error has occured" });
            }
        }

    }
}
