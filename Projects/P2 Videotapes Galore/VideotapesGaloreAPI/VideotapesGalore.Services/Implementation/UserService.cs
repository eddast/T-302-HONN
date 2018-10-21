using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.Exceptions;
using VideotapesGalore.Models.InputModels;
using VideotapesGalore.Repositories.Interfaces;
using VideotapesGalore.Services.Interfaces;

namespace VideotapesGalore.Services.Implementation
{
    /// <summary>
    /// Gets users data from repository in appropriate form
    /// Conducts any buisness logic necessary on data
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// User repository
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Borrow record repository
        /// </summary>
        private readonly IBorrowRecordRepository _borrowRecordRepository;

        /// <summary>
        /// Borrow record repository
        /// </summary>
        private readonly ITapeRepository _tapeRepository;
        /// <summary>
        /// Review repository
        /// </summary>
        private readonly IReviewRepository _reviewRepository;

        /// <summary>
        /// Initialize repository
        /// </summary>
        /// <param name="userRepository">Which implementation of user repository to use</param>
        /// <param name="borrowRecordRepository">Which implementation of borrow record repository to use</param>
        /// <param name="tapeRepository">Which implementation of tape repository to use</param>
        /// <param name="reviewRepository">Which implementation of review repository to use</param>
        public UserService(IUserRepository userRepository, IBorrowRecordRepository borrowRecordRepository, ITapeRepository tapeRepository, IReviewRepository reviewRepository)
        {
            this._userRepository = userRepository;
            this._borrowRecordRepository = borrowRecordRepository;
            this._tapeRepository = tapeRepository;
            this._reviewRepository = reviewRepository;
        }

        /// <summary>
        /// Gets a list of all users in system
        /// </summary>
        /// <returns>List of users dtos</returns>
        public List<UserDTO> GetAllUsers() =>
            _userRepository.GetAllUsers();

        /// <summary>
        /// Returns report on users with borrowed tapes on given loan date (if provided, otherwise on this day)
        /// for at least the amount of days corresponding to loan duration (if provided, otherwise any duration)
        /// </summary>
        /// <param name="LoanDate">Date to base record of users borrowing tapes on</param>
        /// <param name="LoanDuration">Duration in days to base record of users borrowing tapes for</param>
        /// <returns>List of user borrow record as report</returns>
        public List<UserDTO> GetUsersReportAtDateForDuration(DateTime? LoanDate, int? LoanDuration)
        {
            DateTime loanDate = LoanDate.HasValue ? LoanDate.Value : DateTime.Now;
            var allUsersAndBorrows = Mapper.Map<List<UserDTO>>(_userRepository.GetAllUsers());
            List<UserDTO> usersFiltered = new List<UserDTO>();
            var borrowRecords = _borrowRecordRepository.GetAllBorrowRecords();
            foreach (var user in allUsersAndBorrows) {
                // Get borrow records for each user
                var userBorrowRecords = borrowRecords.Where(u => u.UserId == user.Id);
                foreach (var record in userBorrowRecords) {
                    // Get detailed borrow record for each user borrow IF tape was on loan at provided loan date
                    // AND for a given duration (if provided)
                    if ( MatchesLoanDate(loanDate, record.BorrowDate, record.ReturnDate) &&
                         MatchesDuration(LoanDuration, loanDate, record.BorrowDate) ) {
                            usersFiltered.Add(user);
                            break;
                    }
                }
            }
            return usersFiltered;
        }

        /// <summary>
        /// Gets user by id with his or her borrow history, throws exception if user is not found by id
        /// </summary>
        /// <param name="Id">Id associated with user in system</param>
        /// <returns>A user detail dto with borrow history<</returns>
        public UserDetailDTO GetUserById(int Id)
        {
            var user = _userRepository.GetAllUsers().FirstOrDefault(u => u.Id == Id);
            if (user == null) throw new ResourceNotFoundException($"User with id {Id} was not found.");
            var borrowRecords = _borrowRecordRepository.GetAllBorrowRecords().Where(t => t.UserId == Id);
            var userDetails = Mapper.Map<UserDetailDTO>(user);
            userDetails.History = borrowRecords;
            return userDetails;
        }

        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="User">new user to add</param>
        /// <returns>the Id of new user</returns>
        public int CreateUser(UserInputModel User) =>
            _userRepository.CreateUser(User);

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="Id">Id associated with user in system to update</param>
        /// <param name="User">New information on user to swap old information out for</param>
        public void EditUser(int Id, UserInputModel User)
        {
            var oldUser = _userRepository.GetAllUsers().FirstOrDefault(u => u.Id == Id);
            if (oldUser == null) throw new ResourceNotFoundException($"User with id {Id} was not found.");
            else _userRepository.EditUser(Id, User);
        }

        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="Id">Id associated with user in system to delete</param>
        public void DeleteUser(int Id)
        {
            var user = _userRepository.GetAllUsers().FirstOrDefault(u => u.Id == Id);
            if (user == null) throw new ResourceNotFoundException($"User with id {Id} was not found.");
            else {
                RemoveBorrowRecordsForUser(Id);
                RemoveReviewsForUser(Id);
                _userRepository.DeleteUser(Id);
            };
        }

        /// <summary>
        /// Compares loan date to borrow and return date of tape, returns true if it's in between
        /// </summary>
        /// <param name="LoanDate">loan date for tape</param>
        /// <param name="BorrowDate">date of borrow for tape</param>
        /// <param name="ReturnDate">return date for tape</param>
        /// <returns></returns>
        private bool MatchesLoanDate(DateTime LoanDate, DateTime BorrowDate, DateTime? ReturnDate)
        { 
            if(ReturnDate.HasValue) return DateTime.Compare(LoanDate, ReturnDate.Value) < 0 && DateTime.Compare(LoanDate, BorrowDate) >= 0;
            else return DateTime.Compare(LoanDate, BorrowDate) >= 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        private void RemoveBorrowRecordsForUser(int UserId)
        {
            var userRecords = _borrowRecordRepository.GetAllBorrowRecords().Where(b => b.UserId == UserId);
            foreach(var Record in userRecords) {
                _borrowRecordRepository.DeleteRecord(Record.Id);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        private void RemoveReviewsForUser(int UserId)
        {
            var userReviews = _reviewRepository.GetAllReviews().Where(r => r.UserId == UserId);
            foreach(var Review in userReviews) {
                _reviewRepository.DeleteReview(Review.UserId, Review.TapeId);
            }
        }
        /// <summary>
        /// Compares if loan has lasted for a certain duration of days
        /// </summary>
        /// <param name="duration">duration in days of loan</param>
        /// <param name="LoanDate">loan date for tape</param>
        /// <param name="BorrowDate">borrow date for tape</param>
        /// <returns></returns>
        private bool MatchesDuration(int? duration, DateTime LoanDate, DateTime BorrowDate) =>
            !duration.HasValue || (LoanDate - BorrowDate).TotalDays >= duration.Value;
    }
}