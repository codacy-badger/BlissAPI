namespace Infrastructure.Repository
{
    using MongoDB.Driver;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMongoRespository
    {
        /// <summary>
        /// Find an item in the Collection by filter
        /// </summary>
        /// <typeparam name="T">Type of the Entity</typeparam>
        /// <param name="filter">Filters to apply on find</param>
        Task<List<T>> Find<T>(FilterDefinition<T> filter);

        /// <summary>
        /// Find all itens in a Collection
        /// </summary>
        /// <typeparam name="T">Type of the Entity</typeparam>
        /// <param name="limit">Number of the itens per page</param>
        /// <param name="offset">Number of the page</param>
        Task<List<T>> Find<T>(int limit, int offset);

        /// <summary>
        /// Find an item in the Collection by filter and pagining after
        /// </summary>
        /// <typeparam name="T">Type of the Entity</typeparam>
        /// <param name="filter">Filters to apply on find</param>
        /// <param name="limit">Number of the itens per page</param>
        /// <param name="offset">Number of the page</param>
        Task<List<T>> Find<T>(FilterDefinition<T> filter, int limit, int offset);

        /// <summary>
        /// Insert a new item in a Colletion
        /// </summary>
        /// <typeparam name="T">Type of the item</typeparam>
        /// <param name="newModel">Model to insert</param>
        Task<T> Insert<T>(T newModel);

        /// <summary>
        /// Update a existe item on the Collection
        /// </summary>
        /// <typeparam name="T">Type of the item to be updated</typeparam>
        /// <param name="filter">Filter to find a item to be updated</param>
        /// <param name="update">Item to update</param>
        Task<UpdateResult> Update<T>(FilterDefinition<T> filter, UpdateDefinition<T> update);

        /// <summary>
        /// Save a especific item on the collecion
        /// </summary>
        /// <typeparam name="T">Type of the item to be saved</typeparam>
        /// <param name="filter">Filter to aplly a item to be saved</param>
        /// <param name="replacement">Item to save</param>
        Task<ReplaceOneResult> Save<T>(FilterDefinition<T> filter, T replacement);
    }
}
