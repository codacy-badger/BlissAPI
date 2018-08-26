namespace BlissAPI.Controllers
{
    using API.Controllers;
    using API.Models;
    using API.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class QuestionControllerTest
    {
        #region Public tests

        #region Post

        /// <summary>
        /// Test when the QuestionForCreationDto is null or the json was in a bad format
        /// </summary>
        [Test]
        public async Task PostWithNullParametersBadRequest()
        {
            //Arrange
            Mock<IQuestionService> moqQuestionService = new Mock<IQuestionService>();
            var controller = new QuestionController(moqQuestionService.Object);

            //Act
            var result = await controller.Post(null);

            //Assert
            Assert.IsNotNull(result);
            ProcessAcationResult<BadRequestResult>(result, StatusCodes.Status400BadRequest);
        }


        /// <summary>
        /// Test with PublishedAt are null
        /// </summary>
        [Test]
        public async Task PostWithMissingPublishedAtUnprocessableEntity()
        {
            //Arrange
            Mock<IQuestionService> moqQuestionService = new Mock<IQuestionService>();
            var controller = new QuestionController(moqQuestionService.Object);
            var creationDto = new QuestionForCreationDto
            {
                ImageUrl = "ImageUrl",
                Choices = new List<ChoiseForCreationDto>(),
                PublishedAt = null,
                Question = "Question",
                ThumbUrl = "ThumbUrl"
            };

            controller.ModelState.AddModelError("PublishedAt Null", "PublishedAt Required");

            //Act
            var result = await controller.Post(creationDto);

            //Assert
            Assert.IsNotNull(result);
            ProcessAcationResult<API.Helpers.UnprocessableEntityObjectResult>(result, StatusCodes.Status422UnprocessableEntity);
        }

        /// <summary>
        /// Test for when all parameters are passed
        /// </summary>
        [Test]
        public async Task PostWithAllDataNeededOk()
        {
            Mock<IQuestionService> moqQuestionService = new Mock<IQuestionService>();
            var controller = new QuestionController(moqQuestionService.Object);
            var creationDto = new QuestionForCreationDto
            {
                ImageUrl = "ImageUrl",
                Choices = new List<ChoiseForCreationDto>(),
                PublishedAt = DateTimeOffset.Now,
                Question = "Question",
                ThumbUrl = "ThumbUrl"
            };

            //Act
            var result = await controller.Post(creationDto);

            //Assert
            Assert.IsNotNull(result);
            ProcessAcationResult<OkObjectResult>(result, StatusCodes.Status200OK);
        }

        #endregion

        #region Get

        [Test]
        public async Task GetWithNullParametersBadRequest()
        {
            //Arrange
            Mock<IQuestionService> moqQuestionService = new Mock<IQuestionService>();
            var controller = new QuestionController(moqQuestionService.Object);

            //Act
            var result = await controller.Get(null);

            //Assert
            Assert.IsNotNull(result);
            ProcessAcationResult<BadRequestResult>(result, StatusCodes.Status400BadRequest);
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Method to process in a generic way the ActionResult from the controller
        /// </summary>
        /// <typeparam name="T">Type expected</typeparam>
        /// <param name="result">Result from the controller</param>
        /// <param name="statusCodes">Status code expected</param>
        private void ProcessAcationResult<T>(IActionResult result, int statusCodes)
        {
            Assert.IsInstanceOf<T>(result);
            dynamic status = ((T)result);
            Assert.IsNotNull(status);
            Assert.AreEqual(status.StatusCode, statusCodes);
        }

        #endregion
    }
}
