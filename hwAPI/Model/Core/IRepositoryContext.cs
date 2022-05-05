using MongoDB.Driver;

namespace hwAPI.Model.Core
{
    public class IRepositoryContext<T>
    {
        public virtual IMongoCollection<T> Collection { get; }
    }
}
