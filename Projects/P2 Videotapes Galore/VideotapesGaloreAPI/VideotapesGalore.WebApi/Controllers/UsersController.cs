using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.Exceptions;
using VideotapesGalore.Models.InputModels;
using VideotapesGalore.Services.Interfaces;

namespace VideotapesGalore.WebApi.Controllers
{
    /// <summary>
    /// Used to manipulate and get information about users in system
    /// </summary>
    [Route ("api/v1/users")]
    public class UsersController : Controller {

        /// <summary>
        /// service used to fetch data
        /// </summary>
        private readonly IUserService _userService;
        /// <summary>
        /// service used to fetch tape data
        /// </summary>
        private readonly ITapeService _tapeService;

        /// <summary>
        /// Set the user service to use
        /// </summary>
        /// <param name="userService">user service</param>
        /// <param name="tapeService">tape service</param>
        public UsersController(IUserService userService, ITapeService tapeService) { 
            this._userService = userService;
            this._tapeService = tapeService;
        }

        /// <summary>
        /// Gets list of all users in system or, if query parameter loan date and/or loan duration is provided,
        /// gets a report of users as well as their history of tape borrows in accordance to dates and duration provided
        /// </summary>
        /// <param name="LoanDate">
        /// query parameter which if provided will get report of users and borrows that
        /// had tapes on loan at provided loan date
        /// </param>
        /// <param name="LoanDuration">
        /// query parameter which if provided will get report of users and borrows that
        /// have had tapes on loan for at least as many days as loan duration parameter provided
        /// </param>
        /// <returns>
        /// Returns list of informaton on all users in the system if no query parameters are provided
        /// If LoanDate query parameter is provided, all users that had a tape on loan at LoanDate is returned in a report
        /// If LoanDuration query parameter is provided, users that have had a tape on loan for that many days is returned in a report
        /// If both LoanDate and LoanDuration query parameters are provided, a report on users that had tapes on loan at LoanDate AND had had tapes on loan for LoanDuration days are returned
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="400">LoanDate improperly formatted</response>
        /// <response code="400">LoanDuration improperly formatted</response>
        [HttpGet]
        [Route ("")]
        [Produces ("application/json")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDTO>))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserAndBorrowedTapesDTO>))]
        [ProducesResponseType(400, Type = typeof(ExceptionModel))]
        public IActionResult GetAllUsers([FromQuery] string LoanDate, [FromQuery] string LoanDuration)
        {
            // If no query parameters are provided all tapes are returned
            if(String.IsNullOrEmpty(LoanDate) && String.IsNullOrEmpty(LoanDate)) {
                return Ok(_userService.GetAllUsers());
            }
            DateTime? loanDate = null;
            int? loanDuration = null;
            // If loan date is provided, check if it's valid
            if(!String.IsNullOrEmpty(LoanDate)) {
                DateTime date;
                if (!DateTime.TryParse(LoanDate, out date)) throw new ParameterFormatException("LoanDate");
                loanDate = date;
            }
            // If loan date is provided, check if it's valid
            if(!String.IsNullOrEmpty(LoanDuration)) {
                int duration;
                if (!int.TryParse(LoanDuration, out duration)) throw new ParameterFormatException("LoanDuration");
                else loanDuration = duration;
            }
            return Ok(_userService.GetAllUsersAndBorrows(loanDate, loanDuration));
        }

        /// <summary>
        /// Gets user by id along with user's borrow history
        /// </summary>
        /// <param name="id">Id associated with user of the system</param>
        /// <returns>User by id if found</returns>
        /// <response code="200">Success</response>
        /// <response code="404">User not found</response>
        [HttpGet]
        [Route ("{id:int}", Name = "GetUserById")]
        [Produces ("application/json")]
        [ProducesResponseType(200, Type = typeof(UserBorrowRecordDTO))]
        [ProducesResponseType(404, Type = typeof(ExceptionModel))]
        public IActionResult GetUserById(int id) =>
            Ok(_userService.GetUserById(id));

        /// <summary>
        /// Creates a new user for system
        /// </summary>
        /// <param name="User">The user input model</param>
        /// <returns>A status code of 201 created and a set Location header if model is correctly formatted, otherwise error.</returns>
        /// <response code="201">User created</response>
        /// <response code="412">User input model improperly formatted</response>
        [HttpPost]
        [Route ("")]
        [Consumes ("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(412, Type = typeof(ExceptionModel))]
        public IActionResult CreateUser([FromBody] UserInputModel User)
        {
            // Check if input model is valid, output all errors if not
            if (!ModelState.IsValid) {
                IEnumerable<string> errorList = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);
                throw new InputFormatException("User input model improperly formatted.", errorList);
            }
            // Create new tape if input model was valid
            int id = _userService.CreateUser(User);
            return CreatedAtRoute("GetUserById", new { id }, null);
        }

        /// <summary>
        /// Updates user within the system
        /// </summary>
        /// <param name="id">Id associated with user of the system</param>
        /// <param name="User">The user input model</param>
        /// <returns>A status code of 204 no content.</returns>
        /// <response code="204">User updated</response>
        /// <response code="404">User not found</response>
        /// <response code="412">User input model improperly formatted</response>
        [HttpPut ("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(ExceptionModel))]
        [ProducesResponseType(412, Type = typeof(ExceptionModel))]
        public IActionResult EditUser(int id, [FromBody] UserInputModel User)
        {
            // Check if input model is valid, output all errors if not
            if (!ModelState.IsValid) { 
                IEnumerable<string> errorList = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);
                throw new InputFormatException("User input model improperly formatted.", errorList);
            }
            // Edit user if input model was valid
            _userService.EditUser(id, User);
            return NoContent();
        }

        /// <summary>
        /// Deletes user from the system
        /// </summary>
        /// <param name="Id">Id associated with user of the system</param>
        /// <returns>A status code of 204 no content.</returns>
        /// <response code="204">User removed</response>
        /// <response code="404">User not found</response>
        [HttpDelete ("{id:int}")]
        [ProducesResponseType (204)]
        [ProducesResponseType (404, Type = typeof(ExceptionModel))]
        public IActionResult DeleteUser(int Id)
        {
            _userService.DeleteUser(Id);
            return NoContent();
        }

        [HttpGet ("{id:int}/tapes")]
        [ProducesResponseType (200, Type = typeof(IEnumerable<TapeBorrowRecordDTO>))]
        [ProducesResponseType (404, Type = typeof(ExceptionModel))]
        public IActionResult GetUserBorrowRecords(int Id)
        {
            var BorrowRecords = _tapeService.GetTapesForUserOnLoan(Id);
            return Ok(BorrowRecords);
        }

        [HttpPost ("{userId:int}/tapes/{tapeId:int}")]
        [Consumes ("application/json")]
        [ProducesResponseType (201)]
        [ProducesResponseType (404, Type = typeof(ExceptionModel))]
        public IActionResult CreateBorrowRecord(int UserId, int TapeId, [FromBody] BorrowRecordInputModel BorrowRecord)
        {
             // Check if input model is valid, output all errors if not
            if (!ModelState.IsValid) { 
                IEnumerable<string> errorList = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);
                throw new InputFormatException("User input model improperly formatted.", errorList);
            }
            _tapeService.CreateBorrowRecord(TapeId, UserId, BorrowRecord);
            return Created($"{UserId}/tapes/{TapeId}", null);
        }
    }
}
