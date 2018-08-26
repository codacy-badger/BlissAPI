namespace API.Helpers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System;

    /// <summary>
    /// Extention to help with the model state return
    /// </summary>
    public class UnprocessableEntityObjectResult : ObjectResult
    {
        /// <summary>
        /// Return a especific error when the model state is not valid
        /// </summary>
        public UnprocessableEntityObjectResult(ModelStateDictionary modelState)
            : base(new SerializableError(modelState))
        {
            if (modelState == null)
                throw new ArgumentException(nameof(modelState));

            StatusCode = 422;
        }
    }
}
