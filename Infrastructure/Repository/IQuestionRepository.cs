namespace Infrastructure.Repository
{
    using Infrastructure.Entities;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IQuestionRepository
    {
        /// <summary>
        /// Create a new question in the collecion
        /// </summary>
        Task Create(QuestionEntity questionEntity);

        /// <summary>
        /// Get an especific question by id
        /// </summary>
        Task<QuestionEntity> Get(Guid id);

        /// <summary>
        /// Get an especific question by filtering(opctional) and pagining after
        /// </summary>
        Task<List<QuestionEntity>> Get(string filterQuery, int limit, int offset);

        /// <summary>
        /// Save a question
        /// </summary>
        Task Save(QuestionEntity questionEntity);
    }
}
