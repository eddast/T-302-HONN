using System;
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
        public static void ClassInitialize(TestContext testcontext) =>
            _userService = new UserService(_mockUserRepository.Object, _mockBorrowRecordRepository.Object, _mockTapeRepository.Object);

        [TestMethod]
        public void TestMethod1()
        {
            Console.WriteLine("TestMethod1() user service");
            Assert.AreEqual(1,2);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Console.WriteLine("TestMethod2() user service");
            Assert.AreEqual(1,2);
        }
    }
}
