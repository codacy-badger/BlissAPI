namespace Infrastructure.Repository
{
    using Infrastructure.Context;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <inheritdoc />
    public class MongoRespository<TY> : IMongoRespository
    {
        private readonly IMongoContext<TY> _mongoContext;

        public MongoRespository(IMongoContext<TY> mongoContext)
        {
            _mongoContext = mongoContext;
        }

        /// <inheritdoc />
        public async Task<List<T>> Find<T>(FilterDefinition<T> filter)
        {
            IAsyncCursor<T> task = await GetCollection<T>().FindAsync(filter);
            List<T> list = await task.ToListAsync();
            return list;
        }

        /// <inheritdoc />
        public async Task<List<T>> Find<T>(FilterDefinition<T> filter, int limit, int offset)
        {
            var skip = limit * (offset - 1);

            var list = await GetCollection<T>().Find(filter).Skip(skip).Limit(limit).ToListAsync();

            return list;
        }

        /// <inheritdoc />
        public async Task<List<T>> Find<T>(int limit, int offset)
        {
            var skip = limit * (offset - 1);

            var list = await GetCollection<T>().Find(_ => true).Skip(skip).Limit(limit).ToListAsync();

            return list;
        }

        /// <inheritdoc />
        public async Task<T> Insert<T>(T newModel)
        {
            await this.GetCollection<T>().InsertOneAsync(newModel);

            return newModel;
        }

        /// <inheritdoc />
        public async Task<UpdateResult> Update<T>(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            return await GetCollection<T>().UpdateOneAsync(filter, update);
        }

        /// <inheritdoc />
        public async Task<ReplaceOneResult> Save<T>(FilterDefinition<T> filter, T replacement)
        {
            UpdateOptions options = new UpdateOptions { IsUpsert = true };

            return await GetCollection<T>().ReplaceOneAsync(filter, replacement, options);
        }

        /// <summary>
        /// Get collection in database
        /// </summary>
        private IMongoCollection<T> GetCollection<T>()
        {
            MongoCollectionSettings collectionSettings = new MongoCollectionSettings { GuidRepresentation = GuidRepresentation.Standard };
            return _mongoContext.Database.GetCollection<T>(_mongoContext.Collection.CollectionNamespace.CollectionName, collectionSettings);
        }
    }
}
