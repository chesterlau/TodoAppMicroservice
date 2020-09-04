using Newtonsoft.Json;

namespace TodoAppMicroservice.Models
{
    public class Todo
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsComplete { get; set; }
    }
}
