using MongoDB.Driver;
using System.Linq;

namespace hwAPI.Model.Core
{
    public class RepositoryContext<T> : IRepositoryContext<T>
    {
        private readonly IMongoDatabase _db;
        private string _CollectionString;
        public RepositoryContext(string connectString, string dbString,string CollectionString)
        {
            var client = new MongoClient(connectString);
            _db = client.GetDatabase(dbString);
            _CollectionString= CollectionString;
        }
        public override IMongoCollection<T> Collection => _db.GetCollection<T>(_CollectionString);

    }
}
