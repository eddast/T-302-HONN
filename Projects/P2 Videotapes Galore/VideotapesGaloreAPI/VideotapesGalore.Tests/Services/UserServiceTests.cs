using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideotapesGalore.Models.Exceptions;
using VideotapesGalore.Services.Implementation;
using VideotapesGalore.Services.Interfaces;

namespace VideotapesGalore.Tests.Services
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
        public static void ClassInitialize(TestContext testcontext)
        {
            Console.WriteLine("ClassInitialize() in UserService");
            _userService = new UserService(_mockUserRepository.Object, _mockBorrowRecordRepository.Object, _mockTapeRepository.Object, _mockReviewRepository.Object);
        }

        /// <summary>
        /// Test if get all users returns list of all users
        /// </summary>
        [TestMethod]
        public void GetAllUsers_ReturnsListOfAllUsers()
        {
            Console.WriteLine("GetAllUsers() user service");
            var users = _userService.GetAllUsers();
            Assert.AreEqual(_userMockListSize,users.Count());
        }

        // TESTS FOR GetUsersReportAtDateForDuration

        
        /// <summary>
        /// Check if right user is fetched by id
        /// </summary>
        /* [TestMethod]
        public void GetUserById_ShouldReturnCorrectUser()
        {
            var user = _userService.GetUserById(_userMockListSize);
            Assert.AreEqual(user.Id,_userMockListSize);
        }*/

        /// <summary>
        /// Check if not found error is thrown if user by id is not found
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetUserById_ShouldThrowNotFound() =>
            _userService.GetUserById(_userMockListSize+1);
    }
}
