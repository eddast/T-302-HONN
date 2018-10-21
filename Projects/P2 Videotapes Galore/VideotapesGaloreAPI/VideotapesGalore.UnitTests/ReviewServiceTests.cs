using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.Exceptions;
using VideotapesGalore.Models.InputModels;
using VideotapesGalore.Services.Implementation;
using VideotapesGalore.Services.Interfaces;

namespace VideotapesGalore.Tests
{
    /// <summary>
    /// Tests user service
    /// </summary>
    [TestClass]
    public class ReviewServiceTests : TestBase
    {
        /// <summary>
        /// Service to test
        /// </summary>
        private static IReviewService _reviewService;

        /// <summary>
        /// Setup mock service for each tests
        /// Depends on mock repositories created in TestBase
        /// </summary>
        [ClassInitialize]
        public static void ClassInitialize(TestContext testcontext) =>
            _reviewService = new ReviewService(_mockReviewRepository.Object, _mockUserRepository.Object, _mockTapeRepository.Object);

        /// <summary>
        /// Test if get all reviews returns list of all reviews
        /// </summary>
        [TestMethod]
        public void GetAllReviews_ReturnsListOfAllReviews()
        {
            var reviews = _reviewService.GetAllReviews();
            Assert.AreEqual(_reviewMockListSize,reviews.Count());
        }

        /// <summary>
        /// Verify that get user reviews by id returns correct values
        /// User with id 1 has reviewed tape with id 1 and tape with id 2
        /// Verify that returned list has reviews for user review has tapes with ids 1 and 2 only
        /// </summary>
        [TestMethod]
        public void GetUserReviewsById_ShouldReturnReviewForTape1and2ForUser1()
        {
            List<int> tapeIdsReviewedByUserWithId1 = new List<int>(){1, 2};
            var reviews = _reviewService.GetUserReviewsById(1);
            Assert.AreEqual(tapeIdsReviewedByUserWithId1.Count, reviews.Count());
            foreach(var tapeId in tapeIdsReviewedByUserWithId1) {
                Assert.IsNotNull(reviews.FirstOrDefault(t => t.TapeId == tapeId));
            }
        }

        /// <summary>
        /// Check if not found error is thrown if requested to fetch review by user that does not exist by id
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetUserReviewsById_ShouldThrowNotFound() =>
            _reviewService.GetUserReviewsById(_userMockListSize+1);

        /// <summary>
        /// Verify that get user reviews by id returns correct values
        /// Tape with id 2 has two reviews from users with ids 1 and 2
        /// Verify that returned list has reviews for tape from users with ids 1 and 2 only
        /// </summary>
        [TestMethod]
        public void GetTapeReviewsById_ShouldReturnReviewByUser1and2ForUser1()
        {
            List<int> userIdsThatReviewedTapeWithId2 = new List<int>(){1, 2};
            var reviews = _reviewService.GetTapeReviewsById(2);
            Assert.AreEqual(userIdsThatReviewedTapeWithId2.Count, reviews.Count());
            foreach(var userId in userIdsThatReviewedTapeWithId2) {
                Assert.IsNotNull(reviews.FirstOrDefault(t => t.UserId == userId));
            }
        }

        /// <summary>
        /// Check if not found error is thrown if requested to fetch review for tape that does not exist by id
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetTapeReviewsById_ShouldThrowNotFound() =>
            _reviewService.GetTapeReviewsById(_tapeMockListSize+1);

        /// <summary>
        /// Verify that get user reviews by id for tape by id returns correct values
        /// User with id 1 has reviewed tape with id 1
        /// Verify that returned value matches tape with id 1 for user with id 1
        /// </summary>
        [TestMethod]
        public void GetUserReviewForTape_ShouldReturnReviewByUser1and2ForUser1()
        {
            ReviewDTO expectedReview = new ReviewDTO() { UserId = 1, TapeId = 1 };
            var review = _reviewService.GetUserReviewForTape(expectedReview.UserId, expectedReview.TapeId);
            Assert.AreEqual(review.TapeId, expectedReview.TapeId);
            Assert.AreEqual(review.TapeId, expectedReview.UserId);
        }

        /// <summary>
        /// Check if not found error is thrown if requested to fetch review for user that does not exist by id
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetUserReviewForTape_ShouldThrowNotFoundForUserId() =>
            _reviewService.GetUserReviewForTape(_userMockListSize+1, _tapeMockListSize);

        /// <summary>
        /// Check if not found error is thrown if requested to fetch review for tape that does not exist by id
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetUserReviewForTape_ShouldThrowNotFoundForTapeId() =>
            _reviewService.GetUserReviewForTape(_userMockListSize, _tapeMockListSize+1);
        
        /// <summary>
        /// Check if not found error is thrown if requested to fetch review for tape and user that does not exist
        /// No review exists for user 3 for tape 4
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetUserReviewForTape_ShouldThrowNotFoundForReviewNotFound() =>
            _reviewService.GetUserReviewForTape(_userMockListSize, _tapeMockListSize);

        /// <summary>
        /// Check if create review method is called in review repository if review is being created from service
        /// (Given that ids correspond to valid resources in system and review does not exist already)
        /// User with id 3 has no review for tape with id 4
        /// </summary>
        [TestMethod]
        public void CreateUserReview_ShouldCallCreateReviewFromReviewRepository()
        {
            _reviewService.CreateUserReview(_userMockListSize, _tapeMockListSize, It.IsAny<ReviewInputModel>());
            _mockReviewRepository.Verify(mock => mock.CreateReview(_userMockListSize, _tapeMockListSize, It.IsAny<ReviewInputModel>()), Times.Once());
        }

        /// <summary>
        /// Check if not found error is thrown if requested to fetch review for user that does not exist by id
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void CreateUserReview_ShouldThrowNotFoundForUserId() =>
            _reviewService.CreateUserReview(_userMockListSize+1, _tapeMockListSize, It.IsAny<ReviewInputModel>());

        /// <summary>
        /// Check if not found error is thrown if requested to fetch review for tape that does not exist by id
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void CreateUserReview_ShouldThrowNotFoundForTapeId() =>
            _reviewService.CreateUserReview(_userMockListSize, _tapeMockListSize+1, It.IsAny<ReviewInputModel>());

        /// <summary>
        /// Check if error is thrown if review exists for user for a given tape (illegal)
        /// User with id 1 has reviewed tape 1
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InputFormatException))]
        public void CreateUserReview_ShouldThrowInputFormatExceptionIfReviewExists() =>
            _reviewService.CreateUserReview(1, 1, It.IsAny<ReviewInputModel>());

        /// <summary>
        /// Check if edit review method is called in review repository if existing review is being edited from service
        /// (Given that ids correspond to valid resources in system and review does not exist already)
        /// User with id 1 has reviewed tape with id 1
        /// </summary>
        [TestMethod]
        public void EditUserReview_ShouldCallEditReviewFromReviewRepository()
        {
            ReviewDTO newReview = new ReviewDTO() { UserId = 1, TapeId = 1 };
            _reviewService.EditUserReview(newReview.UserId, newReview.TapeId, It.IsAny<ReviewInputModel>());
            _mockReviewRepository.Verify(mock => mock.EditReview(newReview.UserId, newReview.TapeId, It.IsAny<ReviewInputModel>()), Times.Once());
        }

        /// <summary>
        /// Check if not found error is thrown if requested to edit review for user that does not exist by id
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void EditUserReview_ShouldThrowNotFoundForUserId() =>
            _reviewService.EditUserReview(_userMockListSize+1, _tapeMockListSize, It.IsAny<ReviewInputModel>());

        /// <summary>
        /// Check if not found error is thrown if requested to edit review for tape that does not exist by id
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void EditUserReview_ShouldThrowNotFoundForTapeId() =>
            _reviewService.EditUserReview(_userMockListSize, _tapeMockListSize+1, It.IsAny<ReviewInputModel>());

        /// <summary>
        /// Check if not found error is thrown if requested to edit review that does not exist in system
        /// No review exists for user with id 3 for tape with id 4
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void EditUserReview_ShouldThrowNotFoundForNonexistingReview() =>
            _reviewService.EditUserReview(_userMockListSize, _tapeMockListSize, It.IsAny<ReviewInputModel>());

        /// <summary>
        /// Check if delete review method is called in review repository if existing review is being deleted from service
        /// (Given that ids correspond to valid resources in system and review exist)
        /// User with id 1 has reviewed tape with id 1
        /// </summary>
        [TestMethod]
        public void DeleteUserReview_ShouldCallEditReviewFromReviewRepository()
        {
            ReviewDTO reviewToDelete = new ReviewDTO() { UserId = 1, TapeId = 1 };
            _reviewService.DeleteUserReview(reviewToDelete.UserId, reviewToDelete.TapeId);
            _mockReviewRepository.Verify(mock => mock.DeleteReview(reviewToDelete.UserId, reviewToDelete.TapeId), Times.Once());
        }

        /// <summary>
        /// Check if not found error is thrown if requested to delete review for user that does not exist by id
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteUserReview_ShouldThrowNotFoundForUserId() =>
            _reviewService.DeleteUserReview(_userMockListSize+1, _tapeMockListSize);

        /// <summary>
        /// Check if not found error is thrown if requested to delete review for tape that does not exist by id
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteUserReview_ShouldThrowNotFoundForTapeId() =>
            _reviewService.DeleteUserReview(_userMockListSize, _tapeMockListSize+1);

        /// <summary>
        /// Check if not found error is thrown if requested to delete review that does not exist in system
        /// No review exists for user with id 3 for tape with id 4
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteUserReview_ShouldThrowNotFoundForNonexistingReview() =>
            _reviewService.DeleteUserReview(_userMockListSize, _tapeMockListSize);
    }
}
