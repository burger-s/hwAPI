using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hwAPI.Model.Core
{
    public class TheRepository<T> : ITheRepository<T>
    {
        private readonly IRepositoryContext<T> _context;

        public TheRepository(IRepositoryContext<T> context)
        {
            _context = context;
        }

        public async Task Create(T Tobj)
        {
            await _context.Collection.InsertOneAsync(Tobj);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context
                            .Collection
                            .Find(_ => true)
                            .ToListAsync();
        }

        public Task<T> Get<TFeild>(Expression<Func<T,TFeild>> func , TFeild id)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(func, id);
            return _context
                    .Collection
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }

        public async Task<bool> Update(T Tobj, Expression<Func<T, bool>> func)
        {
            ReplaceOneResult updateResult =
                await _context
                        .Collection
                        .ReplaceOneAsync(
                            filter: func,
                            replacement: Tobj);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
            throw new System.NotImplementedException();
        }

        public async Task<bool> Delete<TFeild>(Expression<Func<T, TFeild>> func, TFeild id)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(func, id);
            DeleteResult deleteResult = await _context
                                                .Collection
                                                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<long> GetNextId()
        {
            return await _context.Collection.CountDocumentsAsync(new BsonDocument()) + 1;
        }
    }
}
