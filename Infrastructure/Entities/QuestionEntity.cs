namespace Infrastructure.Entities
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Entity representing the Question in MongoDB
    /// </summary>
    public class QuestionEntity
    {
        /// <summary>
        /// Id of the question
        /// </summary>
        [BsonId]
        [BsonIgnoreIfDefault]
        public Guid Id { get; set; }

        /// <summary>
        /// Question
        /// </summary>
        [BsonElement("Question")]
        public string Question { get; set; }

        /// <summary>
        /// Url of images
        /// </summary>
        [BsonElement("ImageUrl")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Url of Thumb
        /// </summary>
        [BsonElement("ThumbUrl")]
        public string ThumbUrl { get; set; }

        /// <summary>
        /// Date of publish
        /// </summary>
        [BsonElement("PublishedAt")]
        [BsonRepresentation(BsonType.Document)]
        public DateTimeOffset PublishedAt { get; set; }

        /// <summary>
        /// Gets or sets the list of choises
        /// </summary>
        [BsonElement("ListChoiseDtos")]
        public List<ChoiseEntity> Choices { get; set; } = new List<ChoiseEntity>();
    }
    public class ChoiseEntity
    {
        /// <summary>
        /// Choise
        /// </summary>
        [BsonElement("Choice")]
        public string Choice { get; set; }

        /// <summary>
        /// Votes
        /// </summary>
        [BsonElement("Votes")]
        public int Votes { get; set; }
    }
}
