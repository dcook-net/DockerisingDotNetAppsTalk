using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MeetupMembersApi.Mongo
{
    public class MongoDataRepository<T> : IDataRepository<T> where T : class, IHaveAUniqueId
    {
        private readonly IMongoCollection<T> _collection;

        public MongoDataRepository(IMongoCollectionProvider<T> mongoCollectionProvider)
        {
            _collection = mongoCollectionProvider.GetCollection();
        }

        public async Task Create(T objectToSave, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(objectToSave, new InsertOneOptions(), cancellationToken);
        }

        public async Task<T> Read(string id, CancellationToken cancellationToken) 
        {
            T result;

            using (var response = await _collection.FindAsync(x => x.Id == id, new FindOptions<T>(), cancellationToken))
            {
                  result = await response.FirstOrDefaultAsync(cancellationToken);
            }
            
            return result;
        }

        public Task<bool> Delete(string id, CancellationToken cancellationToken)
        {
            var deleteResult = _collection.DeleteOne(x => x.Id == id);

            return Task.FromResult(deleteResult.DeletedCount == 1);
        }

        public async Task<List<T>> ReadAll(CancellationToken cancellationToken)
        {
            return await _collection.Find(_ => true).ToListAsync(cancellationToken);
        }

        public async Task<T> Update(T objectToUpdate, CancellationToken cancellationToken)
        {
            return await _collection.FindOneAndReplaceAsync(x => x.Id == objectToUpdate.Id, objectToUpdate, null, cancellationToken);
        }
    }
} 