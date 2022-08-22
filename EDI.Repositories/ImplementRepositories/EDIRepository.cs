using EDI.Contracts.Repository;
using EDI.Entities.Entities;
using Microsoft.Azure.Cosmos;

namespace EDI.Repositories.ImplementRepositories
{
    public class EDIRepository : IEDIRepository
    {
        private Container _context;

        public EDIRepository(CosmosClient dbClient, string databaseName, string containerName)
        {
            _context = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task<Tuple<ItemContainer, bool>> AddAsync(ItemContainer entity)
        {
            try
            {
                var result = await _context.CreateItemAsync(entity, new PartitionKey(entity.ContainerId));
                return Tuple.Create(new ItemContainer(), true);
            }
            catch (CosmosException)
            {
                throw;
            }
            
        }

        public async Task<List<ItemContainer>> GetAllAsync()
        {
            try
            {
                var query = _context.GetItemQueryIterator<ItemContainer>(new QueryDefinition("SELECT * FROM Item"));
                List<ItemContainer> results = new List<ItemContainer>();
                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    results.AddRange(response.ToList());
                }
                return results;
            }
            catch (CosmosException)
            {
                throw;
            }
        }

        public async Task<List<ItemContainer>> GetByFilterAsync(string queryString)
        {
            try
            {
                var query = _context.GetItemQueryIterator<ItemContainer>(new QueryDefinition(queryString));
                List<ItemContainer> results = new List<ItemContainer>();
                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    results.AddRange(response.ToList());
                }
                return results;
            }
            catch (CosmosException)
            {
                throw;
            }
        }

        public async Task<ItemContainer> GetByIdAsync(string id)
        {
            try
            {
                ItemResponse<ItemContainer> response = await _context.ReadItemAsync<ItemContainer>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException)
            {
                throw;
            }
        }
        
    }
}
