using HashRestAPI.Interfaces;
using HashRestAPI.Models;
using HashRestAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HashRestAPI.Controllers
{
    [ApiController]
    [Route("hashes")]
    public class HashRestAPIController : ControllerBase
    {
        private readonly ILogger<HashRestAPIController> _logger;
        private readonly IHashServices _hashServices;

        public HashRestAPIController(ILogger<HashRestAPIController> logger, IHashServices hasServices)
        {
            _logger = logger;
            _hashServices = hasServices;
        }

        [HttpGet(Name = "hashes")]
        public HashesGetResponse Get()
        {
            var result = new List<HashResult>();

            try
            {
                result = _hashServices.GetHashes();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

            }

            var response = new HashesGetResponse()
            {
                hashes = result
            };

            return response;
        }

        [HttpPost(Name = "hashes")]
        public IActionResult Post()
        {
            try
            {
                _hashServices.PostHashes();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return new StatusCodeResult(500);
            }

            return Ok();
        }

    }
}