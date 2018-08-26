﻿namespace API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Model for the cretion of the Question - Controller responsable
    /// </summary>
    public class QuestionForCreationDto
    {
        /// <summary>
        /// Drescrtion of the question
        /// </summary>
        [Required]
        public string Question { get; set; }

        /// <summary>
        /// Url of the question image
        /// </summary>
        [Required]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Url of the question thumb
        /// </summary>
        [Required]
        public string ThumbUrl { get; set; }

        /// <summary>
        /// Date of published
        /// </summary>
        [Required]
        public DateTimeOffset? PublishedAt { get; set; }

        /// <summary>
        /// List of choices and votes
        /// </summary>
        [Required]
        public List<ChoiseForCreationDto> Choices { get; set; }
    }

    /// <summary>
    /// Choice of the question
    /// TODO - This could be anohter file...
    /// </summary>
    public class ChoiseForCreationDto
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
