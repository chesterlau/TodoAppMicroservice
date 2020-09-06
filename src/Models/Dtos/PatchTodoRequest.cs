namespace TodoAppMicroservice.Models.Dtos
{
    public class PatchTodoRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool? IsComplete { get; set; }
    }
}
