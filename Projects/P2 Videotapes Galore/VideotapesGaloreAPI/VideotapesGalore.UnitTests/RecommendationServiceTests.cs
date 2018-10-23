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


/// User with id 1 had tape with id 1 on loan two years ago but returned it, currently has tapes with ids 1 and 2 on loan since January this year
/// User with id 2 had tape with ids 2 and 4 on loan two years ago but returned it, currently has tape with id 3 since 1 and half years ago

namespace VideotapesGalore.Tests
{
    /// <summary>
    /// Tests user service
    /// </summary>
    [TestClass]
    public class RecommendationServiceTests : TestBase
    {
        /// <summary>
        /// Service to test
        /// </summary>
        private static IRecommendationService _recommendationService;

        /// <summary>
        /// Setup mock service for each tests
        /// Depends on mock repositories created in TestBase
        /// </summary>
        [ClassInitialize]
        public static void ClassInitialize(TestContext testcontext) =>
            _recommendationService = new RecommendationService(_mockReviewRepository.Object, _mockTapeRepository.Object, _mockUserRepository.Object, _mockBorrowRecordRepository.Object);

        /// <summary>
        /// Test if recommendation logic functions correctly in terms of suggesting common borrows
        /// User 1 and user 2 have both borrowed tape 2, so for user 1 recommendation should return tape 4 (tape 2 and 3 is on loan so that leaves tape 4 to be recommended)
        /// </summary>
        [TestMethod]
        public void GetRecommendation_ReturnsCommonBorrowersTape()
        {
            var recommendation = _recommendationService.GetRecommendationForUser(1);
            Assert.AreEqual(recommendation.Id, 4);
            Assert.AreEqual(recommendation.RecommendationReason, "Users that have borrowed some of the same tapes as you also borrowed this tape");
        }

        /// <summary>
        /// Test if recommendation logic functions correctly in terms of suggesting tapes based on rating
        /// User with id 3 has no common borrow records with anyone so his recommendation should be based on reviews in system
        /// Tapes with ids 4 and 5 are only ones not on loan (and user with id 3 has not borrowed either yet),
        /// and tape with id 5 has higher average rating than tape with id 4, so tape with 5 five should be returned for user
        /// </summary>
        [TestMethod]
        public void GetRecommendation_ReturnsBasedOnReviews()
        {
            var recommendation = _recommendationService.GetRecommendationForUser(3);
            Assert.AreEqual(recommendation.Id, 5);
            Assert.AreEqual(recommendation.RecommendationReason, "This tape is the highest rated tape in system of the available tapes that user has not seen");
        }

        /// <summary>
        /// Test if recommendation logic functions correctly when no recommendation can be given to user
        /// User 1 has borrowed all tapes that are not on loan so none are available for recommendation
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetRecommendation_ReturnsErrorWhenNoRecommendationCanBeProvided() =>
            _recommendationService.GetRecommendationForUser(2);

    }
}
