using Ecommerce.Domain.Configuration;
using Ecommerce.Domain.Shared.Entites;
using Microsoft.Extensions.Options;
using MongoDB.Driver;



namespace Ecommerce.DB.Data
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
        public IMongoCollection<User> Users => _database.GetCollection<User>("users");
    }
}
