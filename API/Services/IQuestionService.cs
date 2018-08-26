namespace API.Services
{
    using API.Helpers;
    using API.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Service responsabel for control all CRUD and search for the question repository
    /// </summary>
    public interface IQuestionService
    {
        /// <summary>
        /// Craete a new question in the respository
        /// </summary>
        Task Create(QuestionDto questionDto);

        /// <summary>
        /// Update a question
        /// </summary>
        Task<QuestionDto> Update(QuestionDto questionDto);

        /// <summary>
        /// Get a question by id
        /// </summary>
        Task<QuestionDto> Get(Guid id);

        /// <summary>
        /// Get a question by filtering for "question" or "choice" and pagining
        /// </summary>
        Task<List<QuestionDto>> Get(QuestionResourceParameters questionResourceParameters);
    }
}
