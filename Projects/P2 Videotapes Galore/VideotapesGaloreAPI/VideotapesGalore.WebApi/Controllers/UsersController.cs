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
        /// gets list of users filtered by whether they had tapes on loan at given date and for at least the given duration of days
        /// </summary>
        /// <param name="LoanDate">Loan date to filter user list by whether they had tapes on loan on that date</param>
        /// <param name="LoanDuration">Loan duration to filter user list by whether they had had tapes for as least as many days as loan duration</param>
        /// <returns>
        /// Returns list of informaton on all users in the system if no query parameters are provided. 
        /// If query parameters are provided, returns a list of users filtered by whether they had tapes 
        /// on loan at provided query parameter loan date and for at least the given query parameter loan duration of days
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
            if(String.IsNullOrEmpty(LoanDate) && String.IsNullOrEmpty(LoanDuration)) {
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
            return Ok(_userService.GetUsersReportAtDateForDuration(loanDate, loanDuration));
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
        /// Creates a new user and adds to system
        /// </summary>
        /// <param name="User">The user input model</param>
        /// <returns>A status code of 201 created and a set Location header for new user</returns>
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
        /// Updates existing user within the system
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

        /// <summary>
        /// Gets all borrow records of tapes that given user currently has on loan
        /// </summary>
        /// <param name="Id">Id associated with user of the system</param>
        /// <returns>A status code of 200 along with all borrows of tapes that user currently has on loan</returns>
        /// <response code="200">Returns list of borrows for tapes that user has on loan</response>
        /// <response code="404">User not found</response>
        [HttpGet ("{id:int}/tapes")]
        [ProducesResponseType (200, Type = typeof(IEnumerable<TapeBorrowRecordDTO>))]
        [ProducesResponseType (404, Type = typeof(ExceptionModel))]
        public IActionResult GetUserBorrowRecords(int Id)
        {
            var BorrowRecords = _tapeService.GetTapesForUserOnLoan(Id);
            return Ok(BorrowRecords);
        }

        /// <summary>
        /// Registers given tape on loan (on today's date) for a given user
        /// </summary>
        /// <param name="UserId">Id associated with user of the system</param>
        /// <param name="TapeId">Id associated with tape of the system</param>
        /// <returns>A status code of 204 no content.</returns>
        /// <response code="204">Tape registered on loan by user</response>
        /// <response code="404">User not found</response>
        [HttpPost ("{userId:int}/tapes/{tapeId:int}")]
        [Consumes ("application/json")]
        [ProducesResponseType (201)]
        [ProducesResponseType (404, Type = typeof(ExceptionModel))]
        public IActionResult CreateBorrowRecord(int UserId, int TapeId)
        {
             // Check if input model is valid, output all errors if not
            if (!ModelState.IsValid) { 
                IEnumerable<string> errorList = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);
                throw new InputFormatException("User input model improperly formatted.", errorList);
            }
            _tapeService.CreateBorrowRecord(TapeId, UserId);
            return Created($"{UserId}/tapes/{TapeId}", null);
        }

        /// <summary>
        /// Updates borrow record for user and tape. BEWARE: since this route is available to admins only  
        /// full flexibility is provided for input model dates despite possible inconsistencies as result 
        /// in terms of system's restrictions and functionality, so use carefully.
        /// (Executing bad PUT command here can f.x. can violate that a given tape can't be on loan at the same time)
        /// </summary>
        /// <param name="UserId">Id associated with user of the system</param>
        /// <param name="TapeId">Id associated with tape of the system</param>
        /// <param name="BorrowRecord">Borrow record to register with any return and borrow date</param>
        /// <returns>A status code of 204 no content.</returns>
        /// <response code="204">Tape registered on loan by user</response>
        /// <response code="404">User not found</response>
        /// <response code="404">Tape not found</response>
        [HttpPut ("{userId:int}/tapes/{tapeId:int}")]
        [Consumes ("application/json")]
        [ProducesResponseType (204)]
        [ProducesResponseType (404, Type = typeof(ExceptionModel))]
        public IActionResult UpdateBorrowRecord(int UserId, int TapeId, [FromBody] BorrowRecordInputModel BorrowRecord)
        {
             // Check if input model is valid, output all errors if not
            if (!ModelState.IsValid) { 
                IEnumerable<string> errorList = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);
                throw new InputFormatException("User input model improperly formatted.", errorList);
            }
            _tapeService.UpdateBorrowRecord(TapeId, UserId, BorrowRecord);
            return NoContent();
        }


        /// <summary>
        /// Returns a tape in system (return date is set to today)
        /// </summary>
        /// <param name="UserId">Id associated with user of the system</param>
        /// <param name="TapeId">Id associated with tape of the system</param>
        /// <returns></returns>
        /// <response code="204">Tape returned</response>
        /// <response code="404">Tape not found</response>
        /// <response code="404">User not found</response>
        /// <response code="404">No borrow record for user and tape found</response>
        /// <response code="412">For all matching borrow records, tape has already returned by user</response>
        [HttpDelete ("{userId:int}/tapes/{tapeId:int}")]
        [ProducesResponseType (204)]
        [ProducesResponseType (404, Type = typeof(ExceptionModel))]
         public IActionResult ReturnTape(int UserId, int TapeId)
        {
            _tapeService.ReturnTape(TapeId, UserId);
            return NoContent();
        }
        /// <summary>
        /// RESTRICTED ROUTE, ONLY ACCESSIBLE WITH SECRET KEY.  
        /// Initializes users and borrow records from local initialization file if no users are in system. 
        /// (Routine takes around 5-15 minutes on average)
        /// </summary>
        /// <response code="204">Users and borrows initialized</response>
        /// <response code="401">Client not authorized for initialization</response>
        /// <response code="400">Users already initialized in some form</response>
        [HttpPost ("initialize")]
        [Authorize(Policy="InitializationAuth")]
        [ProducesResponseType (204)]
        [ProducesResponseType(401, Type = typeof(ExceptionModel))]
        [ProducesResponseType(400, Type = typeof(ExceptionModel))]
        public IActionResult InitializeUsersAndBorrows()
        {
            // We do not initialize unless database is empty for safety reasons
            if(_userService.GetAllUsers().Count > 0) {
                return BadRequest(new ExceptionModel {
                    StatusCode = (int) HttpStatusCode.BadRequest,
                    Message = "Users have already been initialized in some form"
                });
            }
            // Otherwise add users from initialization file
            using (StreamReader r = new StreamReader("./Resources/usersAndBorrows.json")) {
                string json = r.ReadToEnd();
                dynamic usersJSON = JsonConvert.DeserializeObject(json);
                foreach(var userJSON in usersJSON) {
                    // Generate input model from json for user
                    UserInputModel user = new UserInputModel {
                        Name = $"{userJSON.first_name} {userJSON.last_name}",
                        Email = userJSON.email,
                        Phone = userJSON.phone,
                        Address = userJSON.address,
                    };
                    // Check if tape input model is valid
                    if (!ModelState.IsValid) {
                        IEnumerable<string> errorList = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);
                        throw new InputFormatException("User in initialization file improperly formatted.", errorList);
                    }
                    // Create new user if input model was valid
                    Console.WriteLine($"adding user and user records for user {userJSON.id} of {usersJSON.Count}");
                    int userId = _userService.CreateUser(user);

                    // Create all borrows associated with user after user was added
                    if(userJSON.tapes != null) {
                        foreach(var borrowRecord in userJSON.tapes) {
                            // Generate input model from json for borrow record
                            BorrowRecordInputModel record = new BorrowRecordInputModel {
                                BorrowDate = Convert.ChangeType(borrowRecord.borrow_date, typeof(DateTime)),
                                ReturnDate = borrowRecord.return_date != null ? Convert.ChangeType(borrowRecord.return_date, typeof(DateTime)) : null
                            };
                            // Check if borrow record input model is valid
                            if (!ModelState.IsValid) {
                                IEnumerable<string> errorList = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);
                                throw new InputFormatException("Tapes borrow for user in initialization file improperly formatted.", errorList);
                            }
                            // Otherwise add to database
                            _tapeService.CreateBorrowRecord((int) borrowRecord.id, (int) userJSON.id, record);
                        }
                    }
                }
            }
            return NoContent();
        }
    }
}
