using System.ComponentModel.DataAnnotations;

namespace TodoAppMicroservice.Models.Dtos
{
    public class UpdateTodoRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool IsComplete { get; set; }
    }
}
