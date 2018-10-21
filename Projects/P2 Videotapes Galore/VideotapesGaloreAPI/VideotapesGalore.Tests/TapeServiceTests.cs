using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.Exceptions;
using VideotapesGalore.Models.InputModels;
using VideotapesGalore.Services.Implementation;
using VideotapesGalore.Services.Implementations;
using VideotapesGalore.Services.Interfaces;

namespace VideotapesGalore.Tests
{
    /// <summary>
    /// Tests tape service
    /// </summary>
    [TestClass]
    public class TapeServiceTests : TestBase
    {
        /// <summary>
        /// Service to test
        /// </summary>
        private static ITapeService _tapeService;

        /// <summary>
        /// Setup mock service for each tests
        /// Depends on mock repositories created in TestBase
        /// </summary>
        [ClassInitialize]
        public static void ClassInitialize(TestContext testcontext) =>
            _tapeService = new TapeService(_mockTapeRepository.Object, _mockBorrowRecordRepository.Object, _mockUserRepository.Object, _mockReviewRepository.Object);

        /// <summary>
        /// Test if get all tapes returns list of all tapes
        /// </summary>
        [TestMethod]
        public void GetAllTapes_ReturnsListOfAllTapes()
        {
            var tapes = _tapeService.GetAllTapes();
            Assert.AreEqual(_tapeMockListSize,tapes.Count());
        }

        /// <summary>
        /// Test if report function in regards to loan date parameter functions correctly
        /// Tapes with ids 1 and 2 were the only ones on loan just short of two years ago
        /// Test if both tapes with ids 1 and 2 are returned from procedure and only them
        /// </summary>
        [TestMethod]
        public void GetTapeReportAtDate_ReturnsTapeWithId1And2FromTwoYearsAgo()
        {
            List<int> tapeIdsForTapesOnLoanTwoYearsAgo = new List<int>(){1, 2};
            var tapes = _tapeService.GetTapeReportAtDate(DateTime.Now.AddYears(-2).AddDays(1));
            Assert.AreEqual(tapeIdsForTapesOnLoanTwoYearsAgo.Count, tapes.Count());
            foreach(var tapeId in tapeIdsForTapesOnLoanTwoYearsAgo) {
                Assert.IsNotNull(tapes.FirstOrDefault(t => t.Id == tapeId));
            }
        }

        /// <summary>
        /// Check if right tape is fetched by id
        /// </summary>
        [TestMethod]
        public void GetTapeById_ShouldReturnCorrectTape()
        {
            var tape = _tapeService.GetTapeById(_tapeMockListSize);
            Assert.AreEqual(tape.Id,_tapeMockListSize);
        }

        /// <summary>
        /// Check if not found error is thrown if requested to fetch tape by id that does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetTapeById_ShouldThrowNotFound() =>
            _tapeService.GetTapeById(_tapeMockListSize+1);

        /// <summary>
        /// Check if create tape method is called in tape repository if tape is being created from service
        /// </summary>
        [TestMethod]
        public void CreateTape_ShouldCallCreateTapeFromTapeRepository()
        {
            _tapeService.CreateTape(It.IsAny<TapeInputModel>());
            _mockTapeRepository.Verify(mock => mock.CreateTape(It.IsAny<TapeInputModel>()), Times.Once());
        }
        
        /// <summary>
        /// Check if edit tape method is called in tape repository if valid id is passed into edit tape
        /// </summary>
        [TestMethod]
        public void EditTape_ShouldCallEditTapeFromRepository()
        {
            _tapeService.EditTape(_tapeMockListSize, It.IsAny<TapeInputModel>());
            _mockTapeRepository.Verify(mock => mock.EditTape(_tapeMockListSize, It.IsAny<TapeInputModel>()), Times.Once());
        }

        /// <summary>
        /// Check if not found error is thrown if requested to edit tape by id that does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void EditTape_ShouldThrowNotFound() =>
            _tapeService.EditTape(_tapeMockListSize+1, It.IsAny<TapeInputModel>());

        /// <summary>
        /// Check if edit tape method is called in tape repository if valid id is passed into delete tape
        /// </summary>
        [TestMethod]
        public void DeleteTape_ShouldCallDeleteTapeFromTapeRepository()
        {
            _tapeService.DeleteTape(_tapeMockListSize);
            _mockTapeRepository.Verify(mock => mock.DeleteTape(_tapeMockListSize), Times.Once());
        }

        /// <summary>
        /// Check if not found error is thrown if requested to delete tape by id that does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteTape_ShouldThrowNotFound() =>
            _tapeService.DeleteTape(_tapeMockListSize+1);

        /// <summary>
        /// Test if tapes currently on loan are fetched correctly for user
        /// User with id 1 currently has tapes with ids 1 and 2 on loan
        /// Test if both those tapes are returned from procedure and only them
        /// </summary>
        [TestMethod]
        public void GetTapesForUserOnLoan_ReturnsAllTapesCurrentlyOnLoan()
        {
            List<int> tapeIdsForTapesUserWithId1CurrentlyHasOnLoan = new List<int>(){ 1, 2 };
            var tapes = _tapeService.GetTapesForUserOnLoan(1);
            Assert.AreEqual(tapeIdsForTapesUserWithId1CurrentlyHasOnLoan.Count, tapes.Count());
            foreach(var tapeId in tapeIdsForTapesUserWithId1CurrentlyHasOnLoan) {
                Assert.IsNotNull(tapes.FirstOrDefault(t => t.Id == tapeId));
            }
        }

        /// <summary>
        /// Check if not found error is thrown if requested to get tapes currently on loan
        /// for user by user id that does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetTapesForUserOnLoan_ShouldThrowNotFound() =>
            _tapeService.GetTapesForUserOnLoan(_userMockListSize+1);

        /// <summary>
        /// Check if not found error is thrown if requested to create borrow record
        /// for user by user id that does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void CreateBorrowRecord_ShouldThrowNotFoundForUserId() =>
            _tapeService.CreateBorrowRecord(_tapeMockListSize, _userMockListSize+1, It.IsAny<BorrowRecordInputModel>());

        /// <summary>
        /// Check if create borrow record method is called in borrow record repository
        /// if valid user and tape ids are passed into create borrow record
        /// </summary>
        [TestMethod]
        public void CreateBorrowRecord_ShouldCallCreateBorrowRecordInBorrowRecordRepository()
        {
            _tapeService.CreateBorrowRecord(_tapeMockListSize, _userMockListSize, It.IsAny<BorrowRecordInputModel>());
            _mockBorrowRecordRepository.Verify(mock => mock.CreateBorrowRecord(It.IsAny<BorrowRecordDTO>()), Times.Once());
        }
        
        /// <summary>
        /// Check if not found error is thrown if requested to create borrow record
        /// for user by user id that does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void CreateBorrowRecord_ShouldThrowNotFoundForTapeId() =>
            _tapeService.CreateBorrowRecord(_tapeMockListSize+1, _userMockListSize, It.IsAny<BorrowRecordInputModel>());

        /// <summary>
        /// Check if update borrow record method is called in borrow record repository
        /// User 1 is borrowing tape 1 - attempt to edit that borrow record
        /// </summary>
        [TestMethod]
        public void UpdateBorrowRecord_ShouldCallEditBorrowRecordInBorrowRecordRepository()
        {
            _tapeService.UpdateBorrowRecord(1, 1, It.IsAny<BorrowRecordInputModel>());
            _mockBorrowRecordRepository.Verify(mock => mock.EditBorrowRecord(It.IsAny<int>(), It.IsAny<BorrowRecordInputModel>()), Times.Once());
        }

        /// <summary>
        /// Check if not found error is thrown if requested to update borrow record
        /// for user by user id that does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void UpdateBorrowRecord_ShouldThrowNotFoundForUserId() =>
            _tapeService.UpdateBorrowRecord(_tapeMockListSize, _userMockListSize+1, It.IsAny<BorrowRecordInputModel>());

        /// <summary>
        /// Check if not found error is thrown if requested to update borrow record
        /// for tape by tape id that does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void UpdateBorrowRecord_ShouldThrowNotFoundForTapeId() =>
            _tapeService.UpdateBorrowRecord(_tapeMockListSize+1, _userMockListSize, It.IsAny<BorrowRecordInputModel>());
        
        /// <summary>
        /// Check if not found error is thrown if requested to update borrow record
        /// for user and tape which no borrow record exists for
        /// User 3 has no borrow record for tape 4
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void UpdateBorrowRecord_ShouldThrowNotFoundForNonexistantBorrowRecord() =>
            _tapeService.UpdateBorrowRecord(_tapeMockListSize, _userMockListSize, It.IsAny<BorrowRecordInputModel>());

        /// <summary>
        /// Check if update borrow record method is called in borrow record repository
        /// If borrow record, tape and user exists in system
        /// User 1 is borrowing tape 1 - attempt to edit that borrow record
        /// </summary>
        [TestMethod]
        public void ReturnTape_ShouldCallReturnTapeInBorrowRecordRepository()
        {
            _tapeService.ReturnTape(1, 1);
            _mockBorrowRecordRepository.Verify(mock => mock.ReturnTape(It.IsAny<int>()), Times.Once());
        }

        /// <summary>
        /// Check if not found error is thrown if requested to return tape for borrow record
        /// for user by user id that does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void ReturnTape_ShouldThrowNotFoundForUserId() =>
            _tapeService.ReturnTape(_tapeMockListSize, _userMockListSize+1);

        /// <summary>
        /// Check if not found error is thrown if requested to return tape for borrow record
        /// for tape by tape id that does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void ReturnTape_ShouldThrowNotFoundForTapeId() =>
            _tapeService.ReturnTape(_tapeMockListSize+1, _userMockListSize);

        /// <summary>
        /// Check if not found error is thrown if requested to return tape if there's no record of
        /// user borrowing the specified tape
        /// User 3 has no borrow record for tape 4
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void ReturnTape_ShouldThrowNotFoundForNonexistantBorrowRecord() =>
            _tapeService.ReturnTape(_tapeMockListSize, _userMockListSize);
    }
}
