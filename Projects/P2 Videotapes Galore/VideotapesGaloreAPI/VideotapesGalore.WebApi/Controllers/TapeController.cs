using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.Exceptions;
using VideotapesGalore.Models.InputModels;

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

        /// <summary>
        /// Gets list of all tapes in system
        /// </summary>
        /// <returns>a list of all tapes</returns>
        /// <response code="200">Success</response>
        /// <response code="400">LoanDate improperly formatted</response>
        [HttpGet]
        [Route ("")]
        [Produces ("application/json")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TapeDTO>))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TapeBorrowRecordDetailsDTO>))]
        [ProducesResponseType(400, Type = typeof(ExceptionModel))]
        public IActionResult GetAllTapes([FromQuery] string LoanDate)
        {
            // If no loan date provided as query parameter all tapes are returned
            if(String.IsNullOrEmpty(LoanDate)) return Ok(_tapeService.GetAllTapes());
            // Otherwise we return record of all tape borrows that were borrowed on date specified
            else {
                DateTime BorrowDate;
                if (!DateTime.TryParse(LoanDate, out BorrowDate)) throw new ParameterFormatException("LoanDate");
                /* TODO */
                else return Ok(_tapeService.GetAllTapes());
            }
        }

        /// <summary>
        /// Gets tapes by id
        /// </summary>
        /// <param name="id">Id associated with video tape of the system</param>
        /// <returns>A single tape if found</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Tape not found</response>
        /// <response code="400">Id improperly formatted</response>
        [HttpGet]
        [Route ("{id:int}", Name = "GetTapeById")]
        [Produces ("application/json")]
        [ProducesResponseType(200, Type = typeof(TapeDTO))]
        [ProducesResponseType(404, Type = typeof(ExceptionModel))]
        [ProducesResponseType(400, Type = typeof(ExceptionModel))]
        public IActionResult GetTapeById(int id) 
        {
            // TODO validate int parameter?
            return Ok(_tapeService.GetTapeById(id));
        }

        /// <summary>
        /// Creates a new video tape for system
        /// </summary>
        /// <param name="Tape">The video tape input model</param>
        /// <returns>A status code of 201 created and a set Location header if model is correctly formatted, otherwise error.</returns>
        /// <response code="201">Video tape created</response>
        /// <response code="412">Video tape input model improperly formatted</response>
        [HttpPost]
        [Route ("")]
        [Consumes ("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(412, Type = typeof(ExceptionModel))]
        public IActionResult CreateTape([FromBody] TapeInputModel Tape)
        {
            if (!ModelState.IsValid) throw new InputFormatException("Video tape input model improperly formatted.");
            int id = _tapeService.CreateTape(Tape);
            return CreatedAtRoute("GetTapeById", new { id }, null);
        }

        /// <summary>
        /// Updates tape within the system
        /// </summary>
        /// <param name="id">Id associated with tape of the system</param>
        /// <param name="Tape">The video tape input model</param>
        /// <returns>A status code of 204 no content.</returns>
        /// <response code="204">Video tape updated</response>
        /// <response code="400">Id improperly formatted</response>
        /// <response code="404">Video tape not found</response>
        /// <response code="412">Video tape input model improperly formatted</response>
        [HttpPut ("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(ExceptionModel))]
        [ProducesResponseType(404, Type = typeof(ExceptionModel))]
        [ProducesResponseType(412, Type = typeof(ExceptionModel))]
        public IActionResult EditTape(int id, [FromBody] TapeInputModel Tape)
        {
            // TODO validate int param?
            if (!ModelState.IsValid) { throw new InputFormatException("Video tape input model improperly formatted."); }
            _tapeService.EditTape(id, Tape);
            return NoContent();
        }

        /// <summary>
        /// Deletes tape from the system
        /// </summary>
        /// <param name="Id">Id associated with tape of the system</param>
        /// <returns>A status code of 204 no content.</returns>
        /// <response code="204">Video tape removed</response>
        /// <response code="400">Id improperly formatted</response>
        /// <response code="404">Video tape not found</response>
        [HttpDelete ("{id:int}")]
        [ProducesResponseType (204)]
        [ProducesResponseType(400, Type = typeof(ExceptionModel))]
        [ProducesResponseType(404, Type = typeof(ExceptionModel))]
        public IActionResult DeleteTape(int Id)
        {
            // TODO validate int param?
            _tapeService.DeleteTape(Id);
            return NoContent();
        }
    }
}
