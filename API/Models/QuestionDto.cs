namespace API.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Main model(DDD) of the Question Service
    /// </summary>
    public class QuestionDto
    {
        /// <summary>
        /// Id of the question
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Drescrtion of the question
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// Url of the question image
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Url of the question thumb
        /// </summary>
        public string ThumbUrl { get; set; }

        /// <summary>
        /// Date of published
        /// </summary>
        public DateTimeOffset PublishedAt { get; set; }

        /// <summary>
        /// List of choices and votes
        /// </summary>
        public List<ChoiseDto> Choices { get; set; } = new List<ChoiseDto>();
    }

    /// <summary>
    /// Choice of the question
    /// TODO - This could be anohter file...
    /// </summary>
    public class ChoiseDto
    {
        /// <summary>
        /// Name of the choce
        /// </summary>
        public string Choice { get; set; }

        /// <summary>
        /// How many votes choice was receive
        /// </summary>
        public int Votes { get; set; }
    }
}
