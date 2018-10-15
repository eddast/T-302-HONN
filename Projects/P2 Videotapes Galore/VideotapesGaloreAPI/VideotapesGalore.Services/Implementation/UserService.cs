using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Tape repository
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initialize repository
        /// </summary>
        /// <param name="userRepository">Which implementation of user repository to use</param>
        public UserService(IUserRepository userRepository) =>
            this._userRepository = userRepository;

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
        public List<UserAndBorrowedTapesDTO> GetAllUsersAndBorrows(DateTime? LoanDate, int? LoanDuration)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets user by id with his or her borrow history, throws exception if user is not found by id
        /// </summary>
        /// <param name="Id">Id associated with user in system</param>
        /// <returns>A user detail dto with borrow history<</returns>
        public UserBorrowRecordDTO GetUserById(int Id)
        {
            var user = _userRepository.GetAllUsers().FirstOrDefault(u => u.Id == Id);
            if (user == null) throw new ResourceNotFoundException($"User with id {Id} was not found.");
            throw new NotImplementedException();
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
            else _userRepository.DeleteUser(Id);
        }
    }
}