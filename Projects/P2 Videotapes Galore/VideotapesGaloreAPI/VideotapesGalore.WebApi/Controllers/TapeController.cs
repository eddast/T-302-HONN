using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VideotapesGalore.Models.DTOs;

namespace VideotapesGalore.WebApi.Controllers
{
    /// <summary>
    /// Used to manipulate and get information about tapes in system
    /// </summary>
    [Route ("api/v1/tapes")]
    public class TapeController : Controller {

        /// <summary>
        /// service used to fetch data
        /// </summary>
        private readonly ITapeService _tapeService;

        /// <summary>
        /// Set the tape service to use
        /// </summary>
        /// <param name="tapeService">tape service</param>
        public TapeController(ITapeService tapeService)
        {
            this._tapeService = tapeService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }
        /// <summary>
        /// Gets list of all tapes in system
        /// </summary>
        /// <returns>a list of all tapes</returns>
        [HttpGet]
        [Route ("")]
        [Produces ("application/json")]
        [ProducesResponseType (200, Type = typeof(IEnumerable<TapeDTO>))]
        public IActionResult GetAllAuthors()
        {
        return Ok(_tapeService.GetAllTapes());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
