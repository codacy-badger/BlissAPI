namespace Infrastructure.Repository
{
    using Infrastructure.Context;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TU"></typeparam>
    public abstract class MongoRespository<TU> 
    {
        private readonly IMongoContext<TU> _mongoContext;

        protected MongoRespository(IMongoContext<TU> mongoContext)
        {
            _mongoContext = mongoContext;
        }

        /// <summary>
        /// Find an item in the Collection by filter
        /// </summary>
        /// <typeparam name="T">Type of the Entity</typeparam>
        /// <param name="filter">Filters to apply on find</param>
        public async Task<List<T>> Find<T>(FilterDefinition<T> filter)
        {
            IAsyncCursor<T> task = await GetCollection<T>().FindAsync(filter);
            List<T> list = await task.ToListAsync();
            return list;
        }

        /// <summary>
        /// Find an item in the Collection by filter and pagining after
        /// </summary>
        /// <typeparam name="T">Type of the Entity</typeparam>
        /// <param name="filter">Filters to apply on find</param>
        /// <param name="limit">Number of the itens per page</param>
        /// <param name="offset">Number of the page</param>
        public async Task<List<T>> Find<T>(FilterDefinition<T> filter, int limit, int offset)
        {
            var skip = limit * (offset - 1);

            var list = await GetCollection<T>().Find(filter).Skip(skip).Limit(limit).ToListAsync();

            return list;
        }

        /// <summary>
        /// Find all itens in a Collection
        /// </summary>
        /// <typeparam name="T">Type of the Entity</typeparam>
        /// <param name="limit">Number of the itens per page</param>
        /// <param name="offset">Number of the page</param>
        public async Task<List<T>> Find<T>(int limit, int offset)
        {
            var skip = limit * (offset - 1);

            var list = await GetCollection<T>().Find(_ => true).Skip(skip).Limit(limit).ToListAsync();

            return list;
        }

        /// <summary>
        /// Insert a new item in a Colletion
        /// </summary>
        /// <typeparam name="T">Type of the item</typeparam>
        /// <param name="newModel">Model to insert</param>
        public async Task<T> Insert<T>(T newModel)
        {
            await this.GetCollection<T>().InsertOneAsync(newModel);

            return newModel;
        }

        /// <summary>
        /// Update a existe item on the Collection
        /// </summary>
        /// <typeparam name="T">Type of the item to be updated</typeparam>
        /// <param name="filter">Filter to find a item to be updated</param>
        /// <param name="update">Item to update</param>
        public async Task<UpdateResult> Update<T>(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            return await GetCollection<T>().UpdateOneAsync(filter, update);
        }

        /// <summary>
        /// Save a especific item on the collecion
        /// </summary>
        /// <typeparam name="T">Type of the item to be saved</typeparam>
        /// <param name="filter">Filter to aplly a item to be saved</param>
        /// <param name="replacement">Item to save</param>
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
