using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAppMicroservice.Models;
using TodoAppMicroservice.Settings;

namespace TodoAppMicroservice.Services
{
    public class CosmosDbRepository : ITodoRepository
    {
        private Container _container;

        public CosmosDbRepository(
            IOptions<CosmosDbSettings> cosmosDbSettings)
        {
            CosmosClient cosmosClient = new CosmosClient(cosmosDbSettings.Value.Account, cosmosDbSettings.Value.Key);
            _container = cosmosClient.GetContainer(cosmosDbSettings.Value.DatabaseName, cosmosDbSettings.Value.ContainerName);
        }

        public async Task AddItemAsync(Todo todo)
        {
            await _container.CreateItemAsync<Todo>(todo, new PartitionKey(todo.id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await _container.DeleteItemAsync<Todo>(id, new PartitionKey(id));
        }

        public async Task<Todo> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Todo> response = await _container.ReadItemAsync<Todo>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Todo>> GetItemsAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<Todo>(new QueryDefinition(queryString));
            List<Todo> results = new List<Todo>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, Todo todo)
        {
            await _container.UpsertItemAsync<Todo>(todo, new PartitionKey(id));
        }
    }
}
