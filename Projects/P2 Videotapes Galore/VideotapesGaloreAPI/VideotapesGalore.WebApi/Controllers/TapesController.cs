using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.Exceptions;
using VideotapesGalore.Models.InputModels;
using VideotapesGalore.Services.Interfaces;

namespace VideotapesGalore.WebApi.Controllers
{
    /// <summary>
    /// Everything related to manipulating and reading information about tapes in system
    /// </summary>
    [Route ("api/v1/tapes")]
    public class TapesController : Controller {

        /// <summary>
        /// service used to fetch data
        /// </summary>
        private readonly ITapeService _tapeService;

        /// <summary>
        /// Set the tape service to use
        /// </summary>
        /// <param name="tapeService">tape service</param>
        public TapesController(ITapeService tapeService) =>
            this._tapeService = tapeService;

        /// <summary>
        /// Gets list of all tapes in system or, if query parameter loan date is provided,
        /// gets all tapes filtered by whether they were on loan at that given date.
        /// </summary>
        /// <param name="LoanDate">
        /// query parameter which if provided will get report of tapes and borrows that
        /// were on loan at provided loan date
        /// </param>
        /// <returns>a list of all tapes or a report on tape borrows relative to loan date if provided</returns>
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
            if(String.IsNullOrEmpty(LoanDate)) {
                return Ok(_tapeService.GetAllTapes());
            }
            // Otherwise return record of all tape borrows on date specified
            // If loan date is an invalid date, throw 400 parameter format exception
            else {
                DateTime BorrowDate;
                if (!DateTime.TryParse(LoanDate, out BorrowDate)) throw new ParameterFormatException("LoanDate");
                return Ok(_tapeService.GetTapeReportAtDate(BorrowDate));
            }
        }

        /// <summary>
        /// Gets tapes by id along with tape's borrow history
        /// </summary>
        /// <param name="id">Id associated with video tape of the system</param>
        /// <returns>A single tape if found</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Tape not found</response>
        [HttpGet]
        [Route ("{id:int}", Name = "GetTapeById")]
        [Produces ("application/json")]
        [ProducesResponseType(200, Type = typeof(TapeDetailDTO))]
        [ProducesResponseType(404, Type = typeof(ExceptionModel))]
        public IActionResult GetTapeById(int id) =>
            Ok(_tapeService.GetTapeById(id));

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
            // Check if input model is valid, output all errors if not
            if (!ModelState.IsValid) {
                IEnumerable<string> errorList = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);
                throw new InputFormatException("Video tape input model improperly formatted.", errorList);
            }
            // Create new tape if input model was valid
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
        /// <response code="404">Video tape not found</response>
        /// <response code="412">Video tape input model improperly formatted</response>
        [HttpPut ("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(ExceptionModel))]
        [ProducesResponseType(412, Type = typeof(ExceptionModel))]
        public IActionResult EditTape(int id, [FromBody] TapeInputModel Tape)
        {
            // Check if input model is valid, output all errors if not
            if (!ModelState.IsValid) { 
                IEnumerable<string> errorList = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);
                throw new InputFormatException("Video tape input model improperly formatted.", errorList);
            }
            // Edit tape if input model was valid
            _tapeService.EditTape(id, Tape);
            return NoContent();
        }

        /// <summary>
        /// Deletes tape from the system
        /// </summary>
        /// <param name="Id">Id associated with tape of the system</param>
        /// <returns>A status code of 204 no content.</returns>
        /// <response code="204">Video tape removed</response>
        /// <response code="404">Video tape not found</response>
        [HttpDelete ("{id:int}")]
        [ProducesResponseType (204)]
        [ProducesResponseType(404, Type = typeof(ExceptionModel))]
        public IActionResult DeleteTape(int Id)
        {
            _tapeService.DeleteTape(Id);
            return NoContent();
        }

        /// <summary>
        /// RESTRICTED ROUTE, ONLY ACCESSIBLE WITH SECRET KEY.  
        /// Initializes tapes from local initialization file if no tapes are in system. 
        /// (Routine takes around 5-15 minutes on average)
        /// </summary>
        /// <response code="204">Tapes initialized</response>
        /// <response code="401">Client not authorized for initialization</response>
        /// <response code="400">Tapes already initialized in some form</response>
        [HttpPost ("initialize")]
        [Authorize(Policy="InitializationAuth")]
        [ProducesResponseType (204)]
        [ProducesResponseType(401, Type = typeof(ExceptionModel))]
        [ProducesResponseType(400, Type = typeof(ExceptionModel))]
        public IActionResult InitializeTapes()
        {
            // We do not initialize unless database is empty for safety reasons
            if(_tapeService.GetAllTapes().Count > 0 ) {
                return BadRequest(new ExceptionModel {
                    StatusCode = (int) HttpStatusCode.BadRequest,
                    Message = "Tapes have already initialized in some form"
                });
            }
            // Otherwise add tapes from initialization file
            using (StreamReader r = new StreamReader("./Resources/tapes.json")) {
                string json = r.ReadToEnd();
                dynamic tapesJSON = JsonConvert.DeserializeObject(json);
                foreach(var tapeJSON in tapesJSON)
                {
                    // Generate input model from json tape
                    TapeInputModel tape = new TapeInputModel {
                        Title = tapeJSON.title,
                        Director = $"{tapeJSON.director_first_name} {tapeJSON.director_last_name}",
                        Type = tapeJSON.type,
                        ReleaseDate = tapeJSON.release_date,
                        EIDR = tapeJSON.eidr
                    };
                    // Check if tape input model is valid
                    if (!ModelState.IsValid) {
                        IEnumerable<string> errorList = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);
                        throw new InputFormatException("Video tape in initialization file improperly formatted.", errorList);
                    }
                    // Create new tape if input model was valid
                    Console.WriteLine($"adding tape {tapeJSON.id} of {tapesJSON.Count}");
                    _tapeService.CreateTape(tape);
                }
            }
            return NoContent();
        }
    }
}
