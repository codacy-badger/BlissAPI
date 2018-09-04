namespace UnitTest.Controller
{
    using API.Controllers;
    using API.Models;
    using API.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class QuestionControllerTest
    {
        #region Public tests

        #region Post

        /// <summary>
        /// Test when the QuestionForCreationDto is null or the json was in a bad format
        /// </summary>
        [Fact]
        public static async Task PostWithNullParametersBadRequest()
        {
            //Arrange
            Mock<IQuestionService> moqQuestionService = new Mock<IQuestionService>();
            var controller = new QuestionController(moqQuestionService.Object);

            //Act
            var result = await controller.Post(null);

            //Assert
            Assert.NotNull(result);
            ProcessAcationResult<BadRequestResult>(result, StatusCodes.Status400BadRequest);
        }


        /// <summary>
        /// Test with PublishedAt are null
        /// </summary>
        [Fact]
        public static async Task PostWithMissingPublishedAtUnprocessableEntity()
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
            Assert.NotNull(result);
            ProcessAcationResult<API.Helpers.UnprocessableEntityObjectResult>(result, StatusCodes.Status422UnprocessableEntity);
        }

        /// <summary>
        /// Test for when all parameters are passed
        /// </summary>
        [Fact]
        public static async Task PostWithAllDataNeededOk()
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
            Assert.NotNull(result);
            ProcessAcationResult<OkObjectResult>(result, StatusCodes.Status200OK);
        }

        #endregion

        #region Get

        [Fact]
        public static async Task GetWithNullParametersBadRequest()
        {
            //Arrange
            Mock<IQuestionService> moqQuestionService = new Mock<IQuestionService>();
            var controller = new QuestionController(moqQuestionService.Object);

            //Act
            var result = await controller.Get(null);

            //Assert
            Assert.NotNull(result);
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
        private static void ProcessAcationResult<T>(IActionResult result, int statusCodes)
        {
            Assert.IsType<T>(result);
            dynamic status = ((T)result);
            Assert.NotNull(status);
            Assert.Equal(status.StatusCode, statusCodes);
        }

        #endregion
    }
}
