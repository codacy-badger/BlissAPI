namespace Infrastructure.Context
{
    using MongoDB.Driver;

    public interface IMongoContext<T>
    {
        IMongoDatabase Database { get; }

        IMongoCollection<T> Collection { get; }
    }
}
