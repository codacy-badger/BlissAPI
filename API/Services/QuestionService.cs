namespace API.Services
{
    using API.Helpers;
    using API.Models;
    using Infrastructure.Entities;
    using Infrastructure.Repository;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <inheritdoc />
    /// <summary>
    /// Class responsable for all CRUD to Questions
    /// </summary>
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRespository;

        public QuestionService(IQuestionRepository questionRespository)
        {
            _questionRespository = questionRespository;
        }

        /// <inheritdoc />
        public async Task Create(QuestionDto questionDto)
        {
            var questionEntity = new QuestionEntity();
            questionEntity.CopyPropertiesFrom(questionDto);

            questionEntity.Id = Guid.NewGuid();
            await _questionRespository.Create(questionEntity);

            questionDto.CopyPropertiesFrom(questionEntity);
        }

        /// <inheritdoc />
        public async Task<QuestionDto> Update(QuestionDto questionDto)
        {
            var check = await Get(questionDto.Id);
            if(check == null)
                return null;

            var questionEntity = new QuestionEntity();
            questionEntity.CopyPropertiesFrom(questionDto);

            await _questionRespository.Save(questionEntity);

            questionDto.CopyPropertiesFrom(questionEntity);

            return questionDto;
        }

        /// <inheritdoc />
        public async Task<QuestionDto> Get(Guid id)
        {
            var question = await _questionRespository.Get(id);

            if (question == null)
                return null;

            var questionDto = new QuestionDto();
            questionDto.CopyPropertiesFrom(question);

            return questionDto;
        }

        /// <inheritdoc />
        public async Task<List<QuestionDto>> Get(QuestionResourceParameters questionResourceParameters)
        {
            var listResult = await _questionRespository.Get(questionResourceParameters.Filter, questionResourceParameters.Limit, questionResourceParameters.Offset);

            if (listResult == null)
                return null;

            var listQuestionDto = new List<QuestionDto>();

            foreach (var question in listResult)
            {
                var questionDto = new QuestionDto();
                questionDto.CopyPropertiesFrom(question);

                listQuestionDto.Add(questionDto);
            }

            return listQuestionDto;
        }
    }
}
