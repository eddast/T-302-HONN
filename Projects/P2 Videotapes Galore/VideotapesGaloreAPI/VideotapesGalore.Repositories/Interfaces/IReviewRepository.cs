using System.Collections.Generic;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.InputModels;

namespace VideotapesGalore.Repositories.Interfaces
{
    /// <summary>
    /// Gets reviews data from database and maps entity data to dto
    /// (e.g. in a format service layer understands)
    /// </summary>
    public interface IReviewRepository
    {
        /// <summary>
        /// Gets all reviews from database
        /// </summary>
        /// <returns>List of all reviews from database</returns>
        List<ReviewDTO> GetAllReviews();
        /// <summary>
        /// Creates new entity model of review and adds to database
        /// </summary>
        /// <param name="UserId">Id of user associated with review</param>
        /// <param name="TapeId">Id of tape associated with review</param>
        /// <param name="Review">Review input model to create entity borrow record from</param>
        void CreateReview(int UserId, int TapeId, ReviewInputModel Review);
        /// <summary>
        /// Updates review by review id
        /// </summary>
        /// <param name="UserId">the id of the user to delete review by from system</param>
        /// <param name="TapeId">the id of the tape to delete review for from system</param>
        /// <param name="Review">new review values to set to old review</param>
        void EditReview(int UserId, int TapeId, ReviewInputModel Review);
        /// <summary>
        /// Deletes review from system
        /// </summary>
        /// <param name="UserId">the id of the user to delete review by from system</param>
        /// <param name="TapeId">the id of the tape to delete review for from system</param>
        void DeleteReview(int UserId, int TapeId);
    }
}