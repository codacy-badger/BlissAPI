namespace Infrastructure.Repository
{
    using Infrastructure.Entities;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <inheritdoc />
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IMongoRespository _mongoRespository;

        public QuestionRepository(IMongoRespository mongoRespository)
        {
            _mongoRespository = mongoRespository;
        }

        /// <inheritdoc />
        public async Task Create(QuestionEntity questionEntity)
        {
            await _mongoRespository.Insert(questionEntity);
        }

        /// <inheritdoc />
        public async Task<QuestionEntity> Get(Guid id)
        {
            FilterDefinition<QuestionEntity> filter = Builders<QuestionEntity>.Filter.Eq(o => o.Id, id);
            var result = await _mongoRespository.Find(filter);

            return result.FirstOrDefault();
        }

        /// <inheritdoc />
        public async Task<List<QuestionEntity>> Get(string filterQuery, int limit, int offset)
        {
            List<QuestionEntity> result;

            if (!string.IsNullOrEmpty(filterQuery))
            {
                FilterDefinition<QuestionEntity> filter = Builders<QuestionEntity>.Filter.Where(o => o.Question.ToLower().Contains(filterQuery.ToLower()));
                filter |= Builders<QuestionEntity>.Filter.ElemMatch(q => q.Choices, q => q.Choice.ToLower().Contains(filterQuery.ToLower()));
                result = await _mongoRespository.Find(filter, limit, offset);
            }
            else
            {
                result = await _mongoRespository.Find<QuestionEntity>(limit, offset);
            }
            
            return result;
        }

        /// <inheritdoc />
        public async Task Save(QuestionEntity questionEntity)
        {
            FilterDefinition<QuestionEntity> filter = Builders<QuestionEntity>.Filter.Eq(o => o.Id, questionEntity.Id);

            await this._mongoRespository.Save(filter, questionEntity);
        }
    }
}
