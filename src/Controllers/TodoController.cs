using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ITodoRepository _todoRepository;
        private readonly ILogger<TodoController> _logger;

        public TodoController(IMapper mapper, ITodoRepository todoRepository, ILogger<TodoController> logger)
        {
            _mapper = mapper;
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
                _logger.LogInformation($"Retrieving a list of to-dos");

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

        [HttpGet, Route("/api/Todos/{id}")]
        [ProducesResponseType(typeof(GetTodoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTodo(Guid id)
        {
            try
            {
                _logger.LogInformation($"Retrieving to-do based on id: {id.ToString()}");

                var todo = await _todoRepository.GetItemAsync(id.ToString());
                
                var response = _mapper.Map<GetTodoResponse>(todo);

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
                _logger.LogInformation($"Creating a new to-do");

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

        [HttpPut, Route("/api/Todos/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTodo(Guid id, [FromBody] UpdateTodoRequest updateTodoRequest)
        {
            try
            {
                _logger.LogInformation($"Updating exisitng to-do with id: {id.ToString()}");

                var todo = await _todoRepository.GetItemAsync(id.ToString());

                todo.Name = updateTodoRequest.Name;
                todo.Description = updateTodoRequest.Description;
                todo.IsComplete = updateTodoRequest.IsComplete;

                await _todoRepository.UpdateItemAsync(id.ToString(), todo);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception caught!");
                return BadRequest(new ApiResult { Error = "An error has occured" });
            }
        }

        [HttpDelete, Route("/api/Todos/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            try
            {
                _logger.LogInformation($"Deleting exisitng to-do with id: {id.ToString()}");

                await _todoRepository.DeleteItemAsync(id.ToString());

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception caught!");
                return BadRequest(new ApiResult { Error = "An error has occured" });
            }
        }

        [HttpPatch, Route("/api/Todos/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PatchTodo(Guid id, [FromBody] PatchTodoRequest patchTodoRequest)
        {
            try
            {
                _logger.LogInformation($"Patching exisitng to-do with id: {id.ToString()}");

                var todo = await _todoRepository.GetItemAsync(id.ToString());

                var result = _mapper.Map(patchTodoRequest, todo);

                await _todoRepository.UpdateItemAsync(id.ToString(), result);

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
