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
using VideotapesGalore.WebApi.Utils;

namespace VideotapesGalore.WebApi.Controllers
{
    /// <summary>
    /// Used to manipulate and get information about users in system
    /// </summary>
    [Route ("api/v1/users")]
    public class UsersController : Controller {

        /// <summary>service used to fetch and manipulate data on users</summary>
        private readonly IUserService _userService;
        /// <summary>service used to fetch and manipulate tape data</summary>
        private readonly ITapeService _tapeService;
        /// <summary>service used to fetch and manipulate review data</summary>
        private readonly IReviewService _reviewService;
        /// <summary>service used to fetch and manipulate review data</summary>
        private readonly IRecommendationService _recommendationService;

        /// <summary>
        /// Set the services as dependency injection for user routes
        /// </summary>
        /// <param name="userService">user service</param>
        /// <param name="tapeService">tape service</param>
        /// <param name="reviewService">review service</param>
        /// <param name="recommendationService">recommendation service</param>
        public UsersController(IUserService userService, ITapeService tapeService, IReviewService reviewService, IRecommendationService recommendationService)
        { 
            this._userService = userService;
            this._tapeService = tapeService;
            this._reviewService = reviewService;
            this._recommendationService = recommendationService;
        }


        /********************************
         *                              *
         *      USER CRUD FUNCTIONS     *
         *                              *
         ********************************/

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
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route ("")]
        [Produces ("application/json")]
        [ProducesResponseType (200, Type = typeof(IEnumerable<UserDTO>))]
        [ProducesResponseType (400, Type = typeof(ExceptionModel))]
        [ProducesResponseType (500, Type = typeof(ExceptionModel))]
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
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route ("{id:int}", Name = "GetUserById")]
        [Produces ("application/json")]
        [ProducesResponseType (200, Type = typeof(UserDetailDTO))]
        [ProducesResponseType (404, Type = typeof(ExceptionModel))]
        [ProducesResponseType (500, Type = typeof(ExceptionModel))]
        public IActionResult GetUserById(int id) =>
            Ok(_userService.GetUserById(id));

        /// <summary>
        /// Creates a new user and adds to system
        /// </summary>
        /// <param name="User">The user input model</param>
        /// <returns>A status code of 201 created and a set Location header for new user</returns>
        /// <response code="201">User created</response>
        /// <response code="412">User input model improperly formatted</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [Route ("")]
        [Consumes ("application/json")]
        [Produces ("application/json")]
        [ProducesResponseType (201)]
        [ProducesResponseType (412, Type = typeof(ExceptionModel))]
        [ProducesResponseType (500, Type = typeof(ExceptionModel))]
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
        /// <response code="500">Internal server error</response>
        [HttpPut ("{id:int}")]
        [Consumes ("application/json")]
        [Produces ("application/json")]
        [ProducesResponseType (204)]
        [ProducesResponseType (404, Type = typeof(ExceptionModel))]
        [ProducesResponseType (412, Type = typeof(ExceptionModel))]
        [ProducesResponseType (500, Type = typeof(ExceptionModel))]
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
        /// <param name="id">Id associated with user of the system</param>
        /// <returns>A status code of 204 no content.</returns>
        /// <response code="204">User removed</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete ("{id:int}")]
        [ProducesResponseType (204)]
        [Produces ("application/json")]
        [ProducesResponseType (404, Type = typeof(ExceptionModel))]
        [ProducesResponseType (500, Type = typeof(ExceptionModel))]
        public IActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);
            return NoContent();
        }

        /****************************
         *                          *
         *      USER BORROWS        *
         *                          *
         ****************************/

        /// <summary>
        /// Gets all borrow records of tapes that given user currently has on loan
        /// </summary>
        /// <param name="id">Id associated with user of the system</param>
        /// <returns>A status code of 200 along with all borrows of tapes that user currently has on loan</returns>
        /// <response code="200">Returns list of borrows for tapes that user has on loan</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet ("{id:int}/tapes")]
        [ProducesResponseType (200, Type = typeof(IEnumerable<TapeBorrowRecordDTO>))]
        [ProducesResponseType (404, Type = typeof(ExceptionModel))]
        [ProducesResponseType (500, Type = typeof(ExceptionModel))]
        public IActionResult GetUserBorrowRecords(int id)
        {
            var BorrowRecords = _tapeService.GetTapesForUserOnLoan(id);
            return Ok(BorrowRecords);
        }

        /// <summary>
        /// Registers given tape on loan (on today's date) for a given user
        /// </summary>
        /// <param name="userId">Id associated with user of the system</param>
        /// <param name="tapeId">Id associated with tape of the system</param>
        /// <returns>A status code of 204 no content.</returns>
        /// <response code="204">Tape registered on loan by user</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPost ("{userId:int}/tapes/{tapeId:int}")]
        [Consumes ("application/json")]
        [Produces ("application/json")]
        [ProducesResponseType (201)]
        [ProducesResponseType (404, Type = typeof(ExceptionModel))]
        [ProducesResponseType (500, Type = typeof(ExceptionModel))]
        public IActionResult CreateBorrowRecord(int userId, int tapeId)
        {
             // Check if input model is valid, output all errors if not
            if (!ModelState.IsValid) { 
                IEnumerable<string> errorList = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);
                throw new InputFormatException("User input model improperly formatted.", errorList);
            }
            _tapeService.CreateBorrowRecord(tapeId, userId);
            return Created($"{userId}/tapes/{tapeId}", null);
        }

        /// <summary>
        /// Updates borrow record for user and tape. BEWARE: since this route is available to admins only  
        /// full flexibility is provided for input model dates despite possible inconsistencies as result 
        /// in terms of system's restrictions and functionality, so use carefully.
        /// (Executing bad PUT command here can f.x. can violate that a given tape can't be on loan at the same time)
        /// </summary>
        /// <param name="userId">Id associated with user of the system</param>
        /// <param name="tapeId">Id associated with tape of the system</param>
        /// <param name="BorrowRecord">Borrow record to register with any return and borrow date</param>
        /// <returns>A status code of 204 no content.</returns>
        /// <response code="204">Tape registered on loan by user</response>
        /// <response code="404">User not found</response>
        /// <response code="404">Tape not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut ("{userId:int}/tapes/{tapeId:int}")]
        [Consumes ("application/json")]
        [Produces ("application/json")]
        [ProducesResponseType (204)]
        [ProducesResponseType (404, Type = typeof(ExceptionModel))]
        [ProducesResponseType (500, Type = typeof(ExceptionModel))]
        public IActionResult UpdateBorrowRecord(int userId, int tapeId, [FromBody] BorrowRecordInputModel BorrowRecord)
        {
             // Check if input model is valid, output all errors if not
            if (!ModelState.IsValid) { 
                IEnumerable<string> errorList = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);
                throw new InputFormatException("User input model improperly formatted.", errorList);
            }
            _tapeService.UpdateBorrowRecord(tapeId, userId, BorrowRecord);
            return NoContent();
        }


        /// <summary>
        /// Returns a tape in system (return date is set to today)
        /// </summary>
        /// <param name="userId">Id associated with user of the system</param>
        /// <param name="tapeId">Id associated with tape of the system</param>
        /// <returns></returns>
        /// <response code="204">Tape returned</response>
        /// <response code="404">Tape not found</response>
        /// <response code="404">User not found</response>
        /// <response code="404">No borrow record for user and tape found</response>
        /// <response code="412">For all matching borrow records, tape has already returned by user</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete ("{userId:int}/tapes/{tapeId:int}")]
        [ProducesResponseType (204)]
        [ProducesResponseType (404, Type = typeof(ExceptionModel))]
        [ProducesResponseType (500, Type = typeof(ExceptionModel))]
         public IActionResult ReturnTape(int userId, int tapeId)
        {
            _tapeService.ReturnTape(tapeId, userId);
            return NoContent();
        }


        /****************************
         *                          *
         *      USER REVIEWS        *
         *                          *
         ****************************/

        /// <summary>
        /// Gets all reviews by a given user
        /// </summary>
        /// <param name="userId">Id associated with user of the system</param>
        /// <returns>A status code of 200 with list of reviews by user</returns>
        /// <response code="200">Success</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet ("{userId:int}/reviews")]
        [ProducesResponseType (200, Type= typeof(IEnumerable<ReviewDTO>))]
        [ProducesResponseType (404, Type = typeof(ExceptionModel))]
        [ProducesResponseType (500, Type = typeof(ExceptionModel))]
         public IActionResult GetUserReviews(int userId)
        {
            return Ok(_reviewService.GetUserReviewsById(userId));
        }

        /// <summary>
        /// Gets a review for a given tape in system for given user
        /// </summary>
        /// <param name="userId">Id associated with user of the system</param>
        /// <param name="tapeId">Id associated with tape of the system</param>
        /// <returns>A status code of 200 to indicate success</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Tape not found</response>
        /// <response code="404">User not found</response>
        /// <response code="404">No review for user for tape found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route ("{userId:int}/reviews/{tapeId:int}", Name = "GetUserReviewForTape")]
        [ProducesResponseType (200, Type = typeof(ReviewDTO))]
        [ProducesResponseType (404, Type = typeof(ExceptionModel))]
        [ProducesResponseType (500, Type = typeof(ExceptionModel))]
         public IActionResult GetUserReviewForTape(int userId, int tapeId)
        {
            return Ok(_reviewService.GetUserReviewForTape(userId, tapeId));
        }

        /// <summary>
        /// Submits review for a given tape in system for given user
        /// </summary>
        /// <param name="userId">Id associated with user of the system</param>
        /// <param name="tapeId">Id associated with tape of the system</param>
        /// <param name="Review">Input model for review including rating for tape</param>
        /// <returns>A status code of 204 to indicate success</returns>
        /// <response code="204">Tape reviewed</response>
        /// <response code="404">Tape not found</response>
        /// <response code="404">User not found</response>
        /// <response code="412">Input model improperly formatted</response>
        /// <response code="500">Internal server error</response>
        [HttpPost ("{userId:int}/reviews/{tapeId:int}")]
        [Consumes ("application/json")]
        [Produces ("application/json")]
        [ProducesResponseType (204)]
        [ProducesResponseType (404, Type = typeof(ExceptionModel))]
        [ProducesResponseType (500, Type = typeof(ExceptionModel))]
         public IActionResult ReviewTape(int userId, int tapeId, [FromBody] ReviewInputModel Review)
        {
            // Check if input model is valid, output all errors if not
            if (!ModelState.IsValid) { 
                IEnumerable<string> errorList = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);
                throw new InputFormatException("Review input model improperly formatted.", errorList);
            }
            _reviewService.CreateUserReview(userId, tapeId, Review);
            return CreatedAtRoute("GetUserReviewForTape", new { userId, tapeId }, null);
        }

        /// <summary>
        /// Deletes a review for a given tape in system for given user
        /// </summary>
        /// <param name="userId">Id associated with user of the system</param>
        /// <param name="tapeId">Id associated with tape of the system</param>
        /// <returns>A status code of 204 to indicate success</returns>
        /// <response code="204">Review deleted</response>
        /// <response code="404">Tape not found</response>
        /// <response code="404">User not found</response>
        /// <response code="404">No review for user for tape found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete ("{userId:int}/reviews/{tapeId:int}")]
        [ProducesResponseType (204)]
        [ProducesResponseType (404, Type = typeof(ExceptionModel))]
        [ProducesResponseType (500, Type = typeof(ExceptionModel))]
         public IActionResult DeleteUserReviewForTape(int userId, int tapeId)
        {
            _reviewService.DeleteUserReview(userId, tapeId);
            return NoContent();
        }

        /// <summary>
        /// Updates a review for a given tape in system for given user
        /// </summary>
        /// <param name="userId">Id associated with user of the system</param>
        /// <param name="tapeId">Id associated with tape of the system</param>
        /// <param name="Review">Input model for review including rating for tape</param>
        /// <returns>A status code of 204 to indicate success</returns>
        /// <response code="204">Review deleted</response>
        /// <response code="404">Tape not found</response>
        /// <response code="404">User not found</response>
        /// <response code="404">No review for user for tape found</response>
        /// <response code="412">Review input model improperly formatted</response>
        /// <response code="500">Internal server error</response>
        [HttpPut ("{userId:int}/reviews/{tapeId:int}")]
        [Consumes ("application/json")]
        [Produces ("application/json")]
        [ProducesResponseType (204)]
        [ProducesResponseType (404, Type = typeof(ExceptionModel))]
        [ProducesResponseType (412, Type = typeof(ExceptionModel))]
        [ProducesResponseType (500, Type = typeof(ExceptionModel))]
         public IActionResult UpdateUserReviewForTape(int userId, int tapeId, [FromBody] ReviewInputModel Review)
        {
            // Check if input model is valid, output all errors if not
            if (!ModelState.IsValid) { 
                IEnumerable<string> errorList = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);
                throw new InputFormatException("Review input model improperly formatted.", errorList);
            }
            _reviewService.EditUserReview(userId, tapeId, Review);
            return NoContent();
        }


        /********************************
         *                              *
         *      USER RECOMMENDATION     *
         *                              *
         ********************************/

        /// <summary>
        /// Gets tape recommendation for user explicitly showing the reason for recommendation as well
        /// </summary>
        /// <param name="id">Id associated with user of the system</param>
        /// <returns>User by id if found</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Recommendation unavailable</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route ("{id:int}/recommendation")]
        [Produces ("application/json")]
        [ProducesResponseType (200, Type = typeof(TapeRecommendationDTO))]
        [ProducesResponseType (404, Type = typeof(ExceptionModel))]
        [ProducesResponseType (500, Type = typeof(ExceptionModel))]
        public IActionResult GetRecommendationForUser(int id) =>
            Ok(_recommendationService.GetRecommendationForUser(id));


        /********************************
         *                              *
         *      USER INITIALIZATION     *
         *                              *
         ********************************/

        /// <summary>
        /// RESTRICTED ROUTE, ONLY ACCESSIBLE WITH SECRET KEY. 
        /// Initializes users and borrow records from local initialization file if no users are in system. 
        /// Note that before calling this route, tapes need to have been initialized already so that borrow records can be registered.
        /// </summary>
        /// <response code="204">Users and borrows initialized</response>
        /// <response code="401">Client not authorized for initialization</response>
        /// <response code="400">Users already initialized in some form</response>
        /// <response code="500">Internal server error</response>
        [HttpPost ("seed")]
        [Authorize(Policy="InitializationAuth")]
        [Produces ("application/json")]
        [ProducesResponseType (204)]
        [ProducesResponseType(401, Type = typeof(ExceptionModel))]
        [ProducesResponseType(400, Type = typeof(ExceptionModel))]
        [ProducesResponseType (500, Type = typeof(ExceptionModel))]
        public IActionResult InitializeUsersAndBorrows()
        {
            // We do not initialize unless database is empty for safety reasons
            if(_userService.GetAllUsers().Count > 0) {
                return BadRequest(new ExceptionModel {
                    StatusCode = (int) HttpStatusCode.BadRequest,
                    Message = "Users have already been initialized in some form"
                });
            }
            // We do not initialize users unless tapes have been initialized due to borrow records
            if(_tapeService.GetAllTapes().Count == 0) {
                return BadRequest(new ExceptionModel {
                    StatusCode = (int) HttpStatusCode.BadRequest,
                    Message = "Tapes need to be initialized before users. Call [POST] /tapes/initialize before."
                });
            }
            // Otherwise add users from initialization file
            using (StreamReader r = StreamReaderFactory.GetStreamReader("../Resources/usersAndBorrows.json")) {
                string json = r.ReadToEnd();
                dynamic usersJSON = JsonConvert.DeserializeObject(json);
                foreach(var userJSON in usersJSON) {
                    // Generate input model from json for user
                    UserInputModel user = SeedingUtils.ConvertJsonToUserInputModel(userJSON);
                    // Check if tape input model is valid
                    if (!ModelState.IsValid) {
                        IEnumerable<string> errorList = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);
                        throw new InputFormatException("User in initialization file improperly formatted.", errorList);
                    }
                    // Create new user if input model was valid
                    Console.WriteLine($"adding user and user records for user {userJSON.id} of {usersJSON.Count}");
                    int userId = _userService.CreateUser(user);

                    SeedingUtils.CreateTapesForUser(userJSON, _tapeService);
                }
            }
            return NoContent();
        }
    }
}
