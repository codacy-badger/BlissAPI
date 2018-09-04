namespace Infrastructure.Repository
{
    using Infrastructure.Context;
    using Infrastructure.Entities;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class QuestionRepository : MongoRespository<QuestionEntity>, IQuestionRepository
    {
        public QuestionRepository(IMongoContext<QuestionEntity> mongoContext)
            : base(mongoContext)
        {
        }

        /// <inheritdoc />
        public async Task Create(QuestionEntity questionEntity)
        {
            await Insert(questionEntity);
        }

        /// <inheritdoc />
        public async Task<QuestionEntity> Get(Guid id)
        {
            FilterDefinition<QuestionEntity> filter = Builders<QuestionEntity>.Filter.Eq(o => o.Id, id);
            var result = await Find(filter);

            return result.FirstOrDefault();
        }

        /// <inheritdoc />
        public async Task<List<QuestionEntity>> Get(string filterQuery, int limit, int offset)
        {
            List<QuestionEntity> result;

            if (!string.IsNullOrEmpty(filterQuery))
            {
                FilterDefinition<QuestionEntity> filter = Builders<QuestionEntity>.Filter.Where(o => o.Question.ToLowerInvariant().Contains(filterQuery.ToLowerInvariant()));
                filter |= Builders<QuestionEntity>.Filter.ElemMatch(q => q.Choices, q => q.Choice.ToLowerInvariant().Contains(filterQuery.ToLowerInvariant()));
                result = await Find(filter, limit, offset);
            }
            else
            {
                result = await Find(limit, offset);
            }
            
            return result;
        }

        /// <inheritdoc />
        public async Task Save(QuestionEntity questionEntity)
        {
            FilterDefinition<QuestionEntity> filter = Builders<QuestionEntity>.Filter.Eq(o => o.Id, questionEntity.Id);

            await this.Save(filter, questionEntity).ConfigureAwait(false);
        }
    }
}
