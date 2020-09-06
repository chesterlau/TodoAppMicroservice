namespace TodoAppMicroservice.Models.Dtos
{
    public class GetTodoResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsComplete { get; set; }
    }
}
