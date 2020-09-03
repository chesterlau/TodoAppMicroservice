using System.Collections.Generic;

namespace TodoAppMicroservice.Models.Dtos
{
    public class GetTodosResponse
    {
        public IEnumerable<Todo> Todos { get; set; }
    }
}
