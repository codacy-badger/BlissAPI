namespace API.Controllers
{
    using API.Helpers;
    using API.Models;
    using API.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Controller for the Question API
    /// </summary>
    [Route("/question/api/v1")]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        /// <summary>
        /// Post to create a new resource of question
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(QuestionDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] QuestionForCreationDto creationDto)
        {
            if (creationDto == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return new Helpers.UnprocessableEntityObjectResult(ModelState);

            var questionDto = new QuestionDto();
            questionDto.CopyPropertiesFrom(creationDto);

            await _questionService.Create(questionDto);

            return Ok(questionDto);
        }

        /// <summary>
        /// Get a epecific question, filtering by "question" or "choise"
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<QuestionDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Get(QuestionResourceParameters questionResourceParameters)
        {
            if (questionResourceParameters == null)
                return BadRequest();

            var result = await _questionService.Get(questionResourceParameters);

            if (result == null || result.Count == 0)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Get a especific question by id
        /// </summary>
        [HttpGet]
        [Route("{Id}")]
        [ProducesResponseType(typeof(QuestionDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _questionService.Get(id);

            if (result == null)
                NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Update a especific question
        /// </summary>
        [HttpPatch]
        [ProducesResponseType(typeof(QuestionDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Patch([FromBody] QuestionForUpdateDto questionForUpdateDto)
        {
            if (questionForUpdateDto == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return new Helpers.UnprocessableEntityObjectResult(ModelState);

            var questionDto = new QuestionDto();
            questionDto.CopyPropertiesFrom(questionForUpdateDto);

            var result = await _questionService.Update(questionDto);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
