namespace Infrastructure.Context
{
    using Infrastructure.Entities;
    using Infrastructure.Settings;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    public sealed class QuestionContext : IMongoContext<QuestionEntity>
    {
        public IMongoDatabase Database { get; }

        public QuestionContext(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            Database = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<QuestionEntity> Collection => Database.GetCollection<QuestionEntity>("Questions");
    }
}
