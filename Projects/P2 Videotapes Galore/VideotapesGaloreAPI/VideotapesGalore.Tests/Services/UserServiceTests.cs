using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        [TestMethod]
        public void TestMethod2()
        {
            Console.WriteLine("TestMethod2() user service");
            Assert.AreEqual(1,1);
        }
    }
}
