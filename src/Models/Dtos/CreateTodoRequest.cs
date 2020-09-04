using System.Diagnostics.Eventing.Reader;

namespace TodoAppMicroservice.Models.Dtos
{
    public class CreateTodoRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsComplete { get; set; }
    }
}
