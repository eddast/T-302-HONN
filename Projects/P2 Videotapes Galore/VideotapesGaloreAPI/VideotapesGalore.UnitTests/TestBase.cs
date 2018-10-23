using System;
using System.Diagnostics;
using System.Linq;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.InputModels;
using VideotapesGalore.Repositories.Interfaces;

namespace VideotapesGalore.Tests
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
        protected static int _tapeMockListSize = 5;
        /// <summary>
        /// Size of mocked list of users for tests
        /// </summary>
        protected static int _userMockListSize = 3;
        /// <summary>
        /// Size of mocked list of borrow records for tests
        /// </summary>
        protected static int _borrowRecordMockListSize = 7;
        /// <summary>
        /// Size of mocked list of borrow records for tests
        /// </summary>
        protected static int _reviewMockListSize = 6;

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
                cfg.CreateMap<TapeDTO, TapeDetailDTO>();
                cfg.CreateMap<TapeDTO, TapeBorrowRecordDTO>();
                cfg.CreateMap<TapeDTO, TapeRecommendationDTO>();
                cfg.CreateMap<TapeRecommendationDTO, TapeDTO>();
                cfg.CreateMap<BorrowRecordInputModel, BorrowRecordDTO>();
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
        private static void SetupTapeRepository()
        {
            _mockTapeRepository.Setup(mock => mock.CreateTape(It.IsAny<TapeInputModel>())).Returns(_tapeMockListSize+1);
            _mockTapeRepository.Setup(mock => mock.EditTape(It.IsAny<int>(), It.IsAny<TapeInputModel>()));
            _mockTapeRepository.Setup(mock => mock.DeleteTape(It.IsAny<int>()));
            _mockTapeRepository.Setup(method => method.GetAllTapes())
                .Returns(FizzWare.NBuilder.Builder<TapeDTO>
                    .CreateListOfSize(_tapeMockListSize)
                    .TheFirst(1).With(t => t.Id = 1)
                    .TheNext(1).With(t => t.Id = 2)
                    .TheNext(1).With(t => t.Id = 3)
                    .TheNext(1).With(t => t.Id = 4)
                    .TheNext(1).With(t => t.Id = 5)
                .Build().ToList());
        }

        /// <summary>
        /// Build minimal mock functionality for user repository for services to function
        /// All users method returns three users with ids 1-3
        /// </summary>
        private static void SetupUserRepository()
        {
            _mockUserRepository.Setup(mock => mock.CreateUser(It.IsAny<UserInputModel>())).Returns(_userMockListSize+1);
            _mockUserRepository.Setup(mock => mock.EditUser(It.IsAny<int>(), It.IsAny<UserInputModel>()));
            _mockUserRepository.Setup(mock => mock.DeleteUser(It.IsAny<int>()));
            _mockUserRepository.Setup(method => method.GetAllUsers())
                .Returns(FizzWare.NBuilder.Builder<UserDTO>
                    .CreateListOfSize(_userMockListSize)
                    .TheFirst(1).With(u => u.Id = 1)
                    .TheNext(1).With(u => u.Id = 2)
                    .TheNext(1).With(u => u.Id = 3)
                .Build().ToList());
        }

        /// <summary>
        /// Build mock functionality for borrow record repository
        /// User with id 1 had tape with id 1 on loan two years ago but returned it, currently has tapes with ids 1 and 2 on loan since January this year
        /// User with id 2 had tape with ids 2, 4 and 5 on loan two years ago but returned it, currently has tape with id 3 since 1 and half years ago
        /// User 3 does not have any borrow records associated with them
        /// </summary>
        private static void SetupBorrowRecordRepository()
        {
            _mockBorrowRecordRepository.Setup(mock => mock.CreateBorrowRecord(It.IsAny<BorrowRecordDTO>())).Returns(_borrowRecordMockListSize+1);
            _mockBorrowRecordRepository.Setup(mock => mock.EditBorrowRecord(It.IsAny<int>(), It.IsAny<BorrowRecordInputModel>()));
            _mockBorrowRecordRepository.Setup(mock => mock.DeleteRecord(It.IsAny<int>()));
            _mockBorrowRecordRepository.Setup(method => method.GetAllBorrowRecords())
                .Returns(FizzWare.NBuilder.Builder<BorrowRecordDTO>
                    .CreateListOfSize(_borrowRecordMockListSize)
                    // Borrows for user with id 1
                    .TheFirst(1).With(r => r.UserId = 1).With(r => r.TapeId = 1).With(r => r.BorrowDate = DateTime.Now.AddYears(-2)).With(r => r.ReturnDate = DateTime.Now.AddYears(-1))
                    .TheNext(1).With(r => r.UserId = 1).With(r => r.TapeId = 1).With(r => r.BorrowDate = new DateTime(DateTime.Today.Year, 1, 1)).With(r => r.ReturnDate = null)
                    .TheNext(1).With(r => r.UserId = 1).With(r => r.TapeId = 2).With(r => r.BorrowDate = new DateTime(DateTime.Today.Year, 10, 1)).With(r => r.ReturnDate = null)
                    // Borrows for user with id 2
                    .TheNext(1).With(r => r.UserId = 2).With(r => r.TapeId = 2).With(r => r.BorrowDate = DateTime.Now.AddYears(-2)).With(r => r.ReturnDate = DateTime.Now.AddYears(-1))
                    .TheNext(1).With(r => r.UserId = 2).With(r => r.TapeId = 4).With(r => r.BorrowDate = DateTime.Now.AddYears(-2)).With(r => r.ReturnDate = DateTime.Now.AddYears(-1))
                    .TheNext(1).With(r => r.UserId = 2).With(r => r.TapeId = 5).With(r => r.BorrowDate = DateTime.Now.AddYears(-2)).With(r => r.ReturnDate = DateTime.Now.AddYears(-1))
                    .TheNext(1).With(r => r.UserId = 2).With(r => r.TapeId = 3).With(r => r.BorrowDate = DateTime.Now.AddYears(-2).AddMonths(6)).With(r => r.ReturnDate = null)
                .Build().ToList());
        }

        /// <summary>
        /// Build mock functionality for review repository
        /// Tape with id 1 has one review from user 1
        /// Tape with id 2 has two reviews from users with ids 1 and 2
        /// Tape with id 3 has one review from user with id 2
        /// Tape with id 4 has one review from user with id 2
        /// </summary>
        private static void SetupReviewRepository() =>
            _mockReviewRepository.Setup(method => method.GetAllReviews())
                .Returns(FizzWare.NBuilder.Builder<ReviewDTO>
                    .CreateListOfSize(_reviewMockListSize)
                    .TheFirst(1).With(r => r.TapeId = 1).With(r => r.UserId = 1)
                    .TheNext(1).With(r => r.TapeId = 2).With(r => r.UserId = 1)
                    .TheNext(1).With(r => r.TapeId = 2).With(r => r.UserId = 2)
                    .TheNext(1).With(r => r.TapeId = 3).With(r => r.UserId = 2)
                    .TheNext(1).With(r => r.TapeId = 4).With(r => r.UserId = 2).With(r => r.Rating = 4)
                    .TheNext(1).With(r => r.TapeId = 5).With(r => r.UserId = 2).With(r => r.Rating = 5)
                .Build().ToList());
    }
}