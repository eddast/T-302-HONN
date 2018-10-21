using System;
using System.Collections.Generic;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.InputModels;

namespace VideotapesGalore.Services.Interfaces
{
    /// <summary>
    /// Gets users data from repository in appropriate form
    /// Conducts any buisness logic necessary on data
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets a list of all users in system
        /// </summary>
        /// <returns>List of users dtos</returns>
        List<UserDTO> GetAllUsers();
        /// <summary>
        /// Returns report on users with borrowed tapes on given loan date (if provided, otherwise on this day)
        /// for at least the amount of days corresponding to loan duration (if provided, otherwise any duration)
        /// </summary>
        /// <param name="LoanDate">Date to base record of users borrowing tapes on</param>
        /// <param name="LoanDuration">Duration in days to base record of users borrowing tapes for</param>
        /// <returns>List of user borrow record as report</returns>
        List<UserDTO> GetUsersReportAtDateForDuration(DateTime? LoanDate, int? LoanDuration);
        /// <summary>
        /// Gets user by id with his or her borrow history, throws exception if user is not found by id
        /// </summary>
        /// <param name="Id">Id associated with user in system</param>
        /// <returns>A user detail dto with borrow history<</returns>
        UserDetailDTO GetUserById(int Id);
        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="User">new user to add</param>
        /// <returns>the Id of new user</returns>
        int CreateUser(UserInputModel User);
        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="Id">Id associated with user in system to update</param>
        /// <param name="User">New information on user to swap old information out for</param>
        void EditUser(int Id, UserInputModel User);
        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="Id">Id associated with user in system to delete</param>
        void DeleteUser(int Id);
    }
}