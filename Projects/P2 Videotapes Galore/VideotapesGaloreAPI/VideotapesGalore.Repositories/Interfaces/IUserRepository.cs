using System.Collections.Generic;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.InputModels;

namespace VideotapesGalore.Repositories.Interfaces
{
    /// <summary>
    /// Gets user data from database and maps entity data to dto
    /// (e.g. in a format service layer understands)
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gets all users from database
        /// </summary>
        /// <returns>List of all users</returns>
        List<UserDTO> GetAllUsers();
        /// <summary>
        /// Creates new entity model of user and adds to database
        /// </summary>
        /// <param name="User">User input model to create entity user from</param>
        /// <returns>The id of the new video user</returns>
        int CreateUser(UserInputModel User);
        /// <summary>
        /// Updates user by id
        /// </summary>
        /// <param name="Id">id of user to update</param>
        /// <param name="User">new user values to set to old tape</param>
        void EditUser(int Id, UserInputModel User);
        /// <summary>
        /// Deletes user from system
        /// </summary>
        /// <param name="Id">the id of the user to delete from system</param>
        void DeleteUser(int Id);
    }
}