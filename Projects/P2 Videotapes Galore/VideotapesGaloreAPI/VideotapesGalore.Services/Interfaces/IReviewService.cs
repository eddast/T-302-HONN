using System.Collections.Generic;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.InputModels;

namespace VideotapesGalore.Services.Interfaces
{
    public interface IReviewService
    {
        /// <summary>
        /// Gets all reviews in system
        /// </summary>
        /// <returns>List of all reviews</returns>
        List<ReviewDTO> GetAllReviews();

        /// <summary>
        /// Gets all reviews in system by a given user
        /// </summary>
        /// <param name="UserId">Id associated with user within the system</param>
        /// <returns>List of reviews by user</returns>
        List<ReviewDTO> GetUserReviewsById(int UserId);

        /// <summary>
        /// Gets all reviews for a given
        /// </summary>
        /// <param name="TapeId">Id associated with tape within the system</param>
        /// <returns>List of all reviews for tape</returns>
        List<ReviewDTO> GetTapeReviewsById(int TapeId);

        /// <summary>
        /// Gets a review for a given tape by a given user
        /// </summary>
        /// <param name="UserId">Id associated with user within the system</param>
        /// <param name="TapeId">Id associated with a tape within the system</param>
        /// <returns>The review for tape by user</returns>
        ReviewDTO GetUserReviewForTape(int UserId, int TapeId);

        /// <summary>
        /// Adds review to system by given user for a given tape
        /// </summary>
        /// <param name="UserId">Id associated with user within the system</param>
        /// <param name="TapeId">Id associated with tape within the system</param>
        /// <param name="Review">The review input model including tape rating</param>
        void CreateUserReview(int UserId, int TapeId, ReviewInputModel Review);

        /// <summary>
        /// Updates review by user for a given tape
        /// </summary>
        /// <param name="UserId">Id associated with user within the system</param>
        /// <param name="TapeId">Id associated with tape within the system</param>
        /// <param name="Review">The review input model including tape rating</param>
        void EditUserReview(int UserId, int TapeId, ReviewInputModel Review);

        /// <summary>
        /// Deletes review from system for a given tape by a given user
        /// </summary>
        /// <param name="UserId">Id associated with user within the system</param>
        /// <param name="TapeId">Id associated with tape within the system</param>
        void DeleteUserReview(int UserId, int TapeId);
    }
}