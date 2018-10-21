using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VideotapesGalore.Models.Exceptions;
using VideotapesGalore.Services.Implementation;
using VideotapesGalore.Services.Interfaces;

namespace VideotapesGalore.Tests
{
    /// <summary>
    /// Tests user service
    /// </summary>
    [TestClass]
    public class UserServiceTests : TestBase
    {
        /// <summary>
        /// Service to test
        /// </summary>
        private static IUserService _userService;

        /// <summary>
        /// Setup mock service for each tests
        /// Depends on mock repositories created in TestBase
        /// </summary>
        [ClassInitialize]
        public static void ClassInitialize(TestContext testcontext) =>
            _userService = new UserService(_mockUserRepository.Object, _mockBorrowRecordRepository.Object, _mockTapeRepository.Object, _mockReviewRepository.Object);

        /// <summary>
        /// Test if get all users returns list of all users
        /// </summary>
        [TestMethod]
        public void GetAllUsers_ReturnsListOfAllUsers()
        {
            var users = _userService.GetAllUsers();
            Assert.AreEqual(_userMockListSize,users.Count());
        }

        /// <summary>
        /// Test if report function regards loan date parameter correctly
        /// Users with id 1 and users with id 2 had tapes on loan on the date a day short of two years ago
        /// Test if both users with ids 1 and 2 are returned from procedure and only them
        /// </summary>
        [TestMethod]
        public void GetUsersReportAtDateForDuration_TestLoanDate_ShouldReturnUsersWithIds1and2Only()
        {
            List<int> userIdsWithTapesOnLoanOnGivenDate = new List<int>(){1, 2};
            var users = _userService.GetUsersReportAtDateForDuration(DateTime.Now.AddYears(-2).AddDays(1), null);
            Assert.AreEqual(userIdsWithTapesOnLoanOnGivenDate.Count, users.Count());
            foreach(var userId in userIdsWithTapesOnLoanOnGivenDate) {
                Assert.IsNotNull(users.FirstOrDefault(u => u.Id == userId));
            }
        }

        /// <summary>
        /// Test if report function regards duration parameter correctly
        /// User with id 2 is the only one that has had a tape for over a year
        /// Test if only user with id 2 is returned from procedure
        /// </summary>
        [TestMethod]
        public void GetUsersReportAtDateForDuration_TestDuration_ShouldReturnUserWithId3Only()
        {
            List<int> usersIdsWithTapesForOverYear = new List<int>(){ 2 };
            var users = _userService.GetUsersReportAtDateForDuration( null, 365 );
            Assert.AreEqual(usersIdsWithTapesForOverYear.Count, users.Count());
            foreach(var userId in usersIdsWithTapesForOverYear) {
                Assert.IsNotNull(users.FirstOrDefault(u => u.Id == userId));
            }
        }

        /// <summary>
        /// Check if right user is fetched by id
        /// </summary>
        [TestMethod]
        public void GetUserById_ShouldReturnCorrectUser()
        {
            var user = _userService.GetUserById(_userMockListSize);
            Assert.AreEqual(user.Id,_userMockListSize);
        }

        /// <summary>
        /// Check if not found error is thrown if requested to fetch user by id that does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetUserById_ShouldThrowNotFound() =>
            _userService.GetUserById(_userMockListSize+1);

        /// <summary>
        /// Check if create user method is called in user repository if user is being created from service
        /// </summary>
        [TestMethod]
        public void CreateUser_ShouldCallCreateUserFromUserRepository()
        {
            _userService.CreateUser(null);
            _mockUserRepository.Verify(mock => mock.CreateUser(null), Times.Once());
        }
        
        /// <summary>
        /// Check if edit user method is called in user repository if valid id is passed into edit user
        /// </summary>
        [TestMethod]
        public void EditUser_ShouldCallEditUserFromRepository()
        {
            _userService.EditUser(_userMockListSize, null);
            _mockUserRepository.Verify(mock => mock.EditUser(_userMockListSize, null), Times.Once());
        }

        /// <summary>
        /// Check if not found error is thrown if requested to edit user by id that does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void EditUser_ShouldThrowNotFound() =>
            _userService.EditUser(_userMockListSize+1, null);

        /// <summary>
        /// Check if edit user method is called in user repository if valid id is passed into delete user
        /// </summary>
        [TestMethod]
        public void DeleteUser_ShouldCallDeleteUserFromRepository()
        {
            _userService.DeleteUser(_userMockListSize);
            _mockUserRepository.Verify(mock => mock.DeleteUser(_userMockListSize), Times.Once());
        }

        /// <summary>
        /// Check if not found error is thrown if requested to delete user by id that does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteUser_ShouldThrowNotFound() =>
            _userService.DeleteUser(_userMockListSize+1);
    }
}
