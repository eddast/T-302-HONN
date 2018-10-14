using System.Collections.Generic;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.InputModels;

namespace VideotapesGalore.Services.Interfaces
{
    public interface IReviewService
    {
        List<ReviewDTO> GetAllReviews();
        List<ReviewDTO> GetUserReviewsById(int UserId);
        List<ReviewDTO> GetTapeReviewsById(int TapeId);
        ReviewDTO GetUserReviewForTape(int UserId, int TapeId);
        void CreateUserReview(int UserId, int TapeId, ReviewInputModel Review);
        void EditUserReview(int UserId, int TapeId, ReviewInputModel Review);
        void DeleteUserReview(int UserId, int TapeId);
    }
}