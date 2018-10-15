using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.InputModels;
using VideotapesGalore.Models.Entities;
using VideotapesGalore.Repositories.DBContext;
using VideotapesGalore.Repositories.Interfaces;

namespace VideotapesGalore.Repositories.Implementation
{
    /// <summary>
    /// Gets user data from database and maps entity data to dto
    /// (e.g. in a format service layer understands)
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Database context to use (MySQL connection)
        /// </summary>
        private VideotapesGaloreDBContext _dbContext;

        /// <summary>
        /// Set database context
        /// </summary>
        /// <param name="dbContext">database context to use</param>
        public UserRepository(VideotapesGaloreDBContext dbContext) =>
            this._dbContext = dbContext;

        /// <summary>
        /// Gets all users from database
        /// </summary>
        /// <returns>List of all users</returns>
        public List<UserDTO> GetAllUsers() =>
            Mapper.Map<List<UserDTO>>(_dbContext.Users.ToList());

        /// <summary>
        /// Creates new entity model of user and adds to database
        /// </summary>
        /// <param name="User">User input model to create entity user from</param>
        /// <returns>The id of the new video user</returns>
        public int CreateUser(UserInputModel User)
        {
            _dbContext.Users.Add(Mapper.Map<User>(User));
            _dbContext.SaveChanges();
            return _dbContext.Users.ToList().OrderByDescending(u => u.CreatedAt).FirstOrDefault().Id;
        }

        /// <summary>
        /// Updates user by id
        /// </summary>
        /// <param name="Id">id of user to update</param>
        /// <param name="User">new user values to set to old tape</param>
        public void EditUser(int Id, UserInputModel User)
        {
            var updateModel = Mapper.Map<User>(User);
            var toUpdate = _dbContext.Users.FirstOrDefault(user => user.Id == Id);
            _dbContext.Attach(toUpdate);
            this.UpdateUser(ref toUpdate, updateModel);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Deletes user from system
        /// </summary>
        /// <param name="Id">the id of the user to delete from system</param>
        public void DeleteUser(int Id)
        {
            _dbContext.Users.Remove(_dbContext.Users.FirstOrDefault(user => user.Id == Id));
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Helper method, updates relevant values of tape to destination tape
        /// </summary>
        /// <param name="src">tape to be updated</param>
        /// <param name="dst">update model for tape</param>
        private void UpdateUser(ref User src, User dst)
        {
            src.Name = dst.Name;
            src.Email = dst.Email;
            src.Phone = dst.Phone;
            src.Address = dst.Address;
            src.LastModified = dst.LastModified;
        }
    }
}