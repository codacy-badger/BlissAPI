namespace API.Controllers
{
    using Infrastructure.Settings;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using System.Threading.Tasks;

    /// <summary>
    /// Controller responsable for check the helth of the API
    /// </summary>
    [Route("/health/api/v1")]
    public class HealthController : Controller
    {
        private readonly IOptions<Settings> _options;

        public HealthController(IOptions<Settings> options)
        {
            this._options = options;
        }

        /// <summary>
        /// Get the Health for MongoDB
        /// TODO - This could be made by another way or improved to get more information or logging what happens
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status201Created)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var client = new MongoClient(_options.Value.ConnectionString);
                client.StartSession();
            }
            catch
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }

            return Ok();
        }
    }
}
