using System;
using System.Diagnostics;
using System.Linq;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VideotapesGalore.Models.DTOs;
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
        protected static Mock<ITapeRepository> _mockTapeRepository = new Mock<ITapeRepository>();
        /// <summary>
        /// Mock repository object to substitute for user repository
        /// </summary>
        protected static Mock<IUserRepository> _mockUserRepository = new Mock<IUserRepository>();
        /// <summary>
        /// Mock repository object to substitute for borrow record repository
        /// </summary>
        protected static Mock<IBorrowRecordRepository> _mockBorrowRecordRepository = new Mock<IBorrowRecordRepository>();
        /// <summary>
        /// Mock repository object to substitute for review repository
        /// </summary>
        protected static Mock<IReviewRepository> _mockReviewRepository = new Mock<IReviewRepository>();

        /// <summary>
        /// Size of mocked list of tapes for tests
        /// </summary>
        protected static int _tapeMockListSize = 4;
        /// <summary>
        /// Size of mocked list of users for tests
        /// </summary>
        protected static int _userMockListSize = 3;
        /// <summary>
        /// Size of mocked list of borrow records for tests
        /// </summary>
        protected static int _borrowRecordMockListSize = 5;
        /// <summary>
        /// Size of mocked list of borrow records for tests
        /// </summary>
        protected static int _reviewMockListSize = 4;

        /// <summary>
        /// Initializes mocks for repositories for all derived test classes to use
        /// </summary>
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext tc)
        {
            // Reset mapper and add need mappings for testing
            AutoMapper.Mapper.Reset();
            AutoMapper.Mapper.Initialize(cfg => {
                cfg.CreateMap<UserDTO, UserDetailDTO>();
            });

            SetupTapeRepository();
            SetupUserRepository();
            SetupBorrowRecordRepository();
            SetupReviewRepository();
        }

        /// <summary>
        /// Build minimal mock functionality for tape repository for services to function
        /// All tapes method returns five tapes with ids 1-4
        /// </summary>
        private static void SetupTapeRepository() =>
            _mockTapeRepository.Setup(method => method.GetAllTapes())
                .Returns(FizzWare.NBuilder.Builder<TapeDTO>
                    .CreateListOfSize(_tapeMockListSize)
                    .TheFirst(1).With(t => t.Id = 1)
                    .TheNext(1).With(t => t.Id = 2)
                    .TheNext(1).With(t => t.Id = 3)
                    .TheNext(1).With(t => t.Id = 4)
                .Build().ToList());

        /// <summary>
        /// Build minimal mock functionality for user repository for services to function
        /// All users method returns three users with ids 1-3
        /// </summary>
        private static void SetupUserRepository() =>
            _mockUserRepository.Setup(method => method.GetAllUsers())
                .Returns(FizzWare.NBuilder.Builder<UserDTO>
                    .CreateListOfSize(_userMockListSize)
                    .TheFirst(1).With(u => u.Id = 1)
                    .TheNext(1).With(u => u.Id = 2)
                    .TheNext(1).With(u => u.Id = 3)
                .Build().ToList());

        /// <summary>
        /// Build mock functionality for borrow record repository
        /// User with id 1 had tape with id 1 on loan in 2016 but returned it, currently has tapes with ids 1 and 2 on loan
        /// User with id 2 had tape with id 2 on loan in 2015 but returned it, currently has tapes with id 3
        /// User 3 does not have any borrow records associated with them
        /// Tape 4 does not have any borrow record associated with it
        /// </summary>
        /// 
        private static void SetupBorrowRecordRepository() =>
            _mockBorrowRecordRepository.Setup(method => method.GetAllBorrowRecords())
                .Returns(FizzWare.NBuilder.Builder<BorrowRecordDTO>
                    .CreateListOfSize(_borrowRecordMockListSize)
                    // Borrows for user with id 1
                    .TheFirst(1).With(r => r.UserId = 1).With(r => r.TapeId = 1).With(r => r.BorrowDate = new DateTime(2016, 1, 1)).With(r => r.ReturnDate = new DateTime(2016, 12, 30))
                    .TheNext(1).With(r => r.UserId = 1).With(r => r.TapeId = 1).With(r => r.BorrowDate = new DateTime(2018, 1, 1)).With(r => r.ReturnDate = null)
                    .TheNext(1).With(r => r.UserId = 1).With(r => r.TapeId = 2).With(r => r.BorrowDate = new DateTime(2018, 10, 1)).With(r => r.ReturnDate = null)
                    // Borrows for user with id 2
                    .TheNext(1).With(r => r.UserId = 2).With(r => r.TapeId = 2).With(r => r.BorrowDate = new DateTime(2015, 8, 10)).With(r => r.ReturnDate = new DateTime(2016, 1, 10))
                    .TheNext(1).With(r => r.UserId = 2).With(r => r.TapeId = 3).With(r => r.BorrowDate = new DateTime(2018, 10, 1)).With(r => r.ReturnDate = null)
                .Build().ToList());

        /// <summary>
        /// Build mock functionality for review repository
        /// Tape with id 1 has one review from user 1
        /// Tape with id 2 has two reviews from users with ids 1 and 2
        /// Tape with id 3 has one review from user with id 2
        /// Tape with id 4 has no reviews and user with id 3 has no reviews
        /// </summary>
        private static void SetupReviewRepository() =>
            _mockReviewRepository.Setup(method => method.GetAllReviews())
                .Returns(FizzWare.NBuilder.Builder<ReviewDTO>
                    .CreateListOfSize(_reviewMockListSize)
                    .TheFirst(1).With(r => r.TapeId = 1).With(r => r.UserId = 1)
                    .TheNext(1).With(r => r.TapeId = 2).With(r => r.UserId = 1)
                    .TheNext(1).With(r => r.TapeId = 2).With(r => r.UserId = 2)
                    .TheNext(1).With(r => r.TapeId = 3).With(r => r.UserId = 2)
                .Build().ToList());
    }
}