using System.Collections.Generic;
using System.Threading.Tasks;
using TodoAppMicroservice.Models;

namespace TodoAppMicroservice.Services
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetItemsAsync(string queryString);
        Task<Todo> GetItemAsync(string id);
        Task AddItemAsync(Todo todo);
        Task UpdateItemAsync(string id, Todo todo);
        Task DeleteItemAsync(string id);
    }
}
