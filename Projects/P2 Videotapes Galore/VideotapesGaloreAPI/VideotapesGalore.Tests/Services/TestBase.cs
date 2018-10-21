using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VideotapesGalore.Repositories.Interfaces;

namespace VideotapesGalore.Tests.Services
{
    /// <summary>
    /// Initializes repositories and mock functionality of repositories before all tests are run
    /// Derived classes can then access the mocked repositories as they wish
    /// </summary>
    [TestClass]
    public class TestBase
    {
        /// <summary>
        /// Mock repository object to substitute for tape repository
        /// </summary>
        public static Mock<ITapeRepository> _mockTapeRepository = new Mock<ITapeRepository>();
        /// <summary>
        /// Mock repository object to substitute for user repository
        /// </summary>
        public static Mock<IUserRepository> _mockUserRepository = new Mock<IUserRepository>();
        /// <summary>
        /// Mock repository object to substitute for borrow record repository
        /// </summary>
        public static Mock<IBorrowRecordRepository> _mockBorrowRecordRepository = new Mock<IBorrowRecordRepository>();
        /// <summary>
        /// Mock repository object to substitute for review repository
        /// </summary>
        public static Mock<IReviewRepository> _mockReviewRepository = new Mock<IReviewRepository>();

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext tc)
        {
            SetupTapeRepository();
            SetupUserRepository();
            SetupBorrowRecordRepository();
            SetupReviewRepository();
            Console.WriteLine("AssemblyInitialize() in base");
        }

        /// <summary>
        /// Build mock functionality for tape repository
        /// </summary>
        private static void SetupTapeRepository()
        {
            // TODO
        }

        /// <summary>
        /// Build mock functionality for user repository
        /// </summary>
        private static void SetupUserRepository()
        {
            // TODO
        }

        /// <summary>
        /// Build mock functionality for borrow record repository
        /// </summary>
        private static void SetupBorrowRecordRepository()
        {
            // TODO
        }

        /// <summary>
        /// Build mock functionality for review repository
        /// </summary>
        private static void SetupReviewRepository()
        {
            // TODO
        }
    }
}