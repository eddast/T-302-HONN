using System.Collections.Generic;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.InputModels;
using VideotapesGalore.Repositories.Interfaces;

namespace VideotapesGalore.Repositories.Implementation
{
    public class ReviewRepository : IReviewRepository
    {
        public void CreateUserReview(int UserId, int TapeId, ReviewInputModel Review)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteUserReview(int UserId, int TapeId)
        {
            throw new System.NotImplementedException();
        }

        public void EditUserReview(int UserId, int TapeId, ReviewInputModel Review)
        {
            throw new System.NotImplementedException();
        }

        public List<ReviewDTO> GetAllReviews()
        {
            throw new System.NotImplementedException();
        }

        public List<ReviewDTO> GetTapeReviewsById(int TapeId)
        {
            throw new System.NotImplementedException();
        }

        public ReviewDTO GetUserReviewForTape(int UserId, int TapeId)
        {
            throw new System.NotImplementedException();
        }

        public List<ReviewDTO> GetUserReviewsById(int UserId)
        {
            throw new System.NotImplementedException();
        }
    }
}