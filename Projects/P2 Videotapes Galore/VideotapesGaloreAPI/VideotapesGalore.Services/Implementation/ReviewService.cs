using System;
using System.Collections.Generic;
using System.Linq;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.Exceptions;
using VideotapesGalore.Models.InputModels;
using VideotapesGalore.Repositories.Interfaces;
using VideotapesGalore.Services.Interfaces;

namespace VideotapesGalore.Services.Implementation
{
    public class ReviewService : IReviewService
    {
        /// <summary>Review repository</summary>
        private readonly IReviewRepository _reviewRepository;

        /// <summary>User repository</summary>
        private readonly IUserRepository _userRepository;

        /// <summary>Tape repository</summary>
        private readonly ITapeRepository _tapeRepository;

        /// <summary>
        /// Initialize repositories
        /// </summary>
        /// <param name="reviewRepository">Which implementation of review repository to use</param>
        /// <param name="userRepository">Which implementation of user repository to use</param>
        /// <param name="tapeRepository">Which implementation of tape repository to use</param>
        public ReviewService(IReviewRepository reviewRepository, IUserRepository userRepository, ITapeRepository tapeRepository)
        {
            this._reviewRepository = reviewRepository;
            this._userRepository = userRepository;
            this._tapeRepository = tapeRepository;
        }

        /// <summary>
        /// Gets all reviews in system
        /// </summary>
        /// <returns>List of all reviews</returns>
        public List<ReviewDTO> GetAllReviews() =>
            _reviewRepository.GetAllReviews();

        /// <summary>
        /// Gets all reviews in system by a given user
        /// </summary>
        /// <param name="UserId">Id associated with user within the system</param>
        /// <returns>List of reviews by user</returns>
        public List<ReviewDTO> GetUserReviewsById(int UserId)
        {
            ValidateUser(UserId);
            return(_reviewRepository.GetAllReviews().Where(t => t.UserId == UserId).ToList());
        }

        /// <summary>
        /// Gets all reviews for a given
        /// </summary>
        /// <param name="TapeId">Id associated with tape within the system</param>
        /// <returns>List of all reviews for tape</returns>
        public List<ReviewDTO> GetTapeReviewsById(int TapeId)
        {
            ValidateTape(TapeId);
            return(_reviewRepository.GetAllReviews().Where(t => t.TapeId == TapeId).ToList());
        }

        /// <summary>
        /// Gets a review for a given tape by a given user
        /// </summary>
        /// <param name="UserId">Id associated with user within the system</param>
        /// <param name="TapeId">Id associated with a tape within the system</param>
        /// <returns>The review for tape by user</returns>
        public ReviewDTO GetUserReviewForTape(int UserId, int TapeId)
        {
            ValidateUser(UserId); ValidateTape(TapeId);
            var review = _reviewRepository.GetAllReviews().FirstOrDefault(r => r.UserId == UserId && r.TapeId == TapeId);
            if(review == null) throw new ResourceNotFoundException($"No review was found by user with id {UserId} for tape with id {TapeId}.");
            return review;
        }

        /// <summary>
        /// Adds review to system by given user for a given tape
        /// </summary>
        /// <param name="UserId">Id associated with user within the system</param>
        /// <param name="TapeId">Id associated with tape within the system</param>
        /// <param name="Review">The review input model including tape rating</param>
        public void CreateUserReview(int UserId, int TapeId, ReviewInputModel Review)
        {
            ValidateUser(UserId); ValidateTape(TapeId);
            var reviewExists = _reviewRepository.GetAllReviews().FirstOrDefault(r => r.UserId == UserId && r.TapeId == TapeId);
            if(reviewExists != null) throw new InputFormatException($"Review already exists for user with id {UserId} for tape with id {TapeId}. Use PUT to edit reviews");
            _reviewRepository.CreateReview(UserId, TapeId, Review);
        }

        /// <summary>
        /// Updates review by user for a given tape
        /// </summary>
        /// <param name="UserId">Id associated with user within the system</param>
        /// <param name="TapeId">Id associated with tape within the system</param>
        /// <param name="Review">The review input model including tape rating</param>
        public void EditUserReview(int UserId, int TapeId, ReviewInputModel Review)
        {
            ValidateUser(UserId); ValidateTape(TapeId);
            var toUpdate = _reviewRepository.GetAllReviews().FirstOrDefault(r => r.UserId == UserId && r.TapeId == TapeId);
            if(toUpdate == null) throw new ResourceNotFoundException($"Cannot edit non-existing review: no review was found by user with id {UserId} for tape with id {TapeId}.");
            _reviewRepository.EditReview(UserId, TapeId, Review);
        }

        /// <summary>
        /// Deletes review from system for a given tape by a given user
        /// </summary>
        /// <param name="UserId">Id associated with user within the system</param>
        /// <param name="TapeId">Id associated with tape within the system</param>
        public void DeleteUserReview(int UserId, int TapeId)
        {
            ValidateUser(UserId); ValidateTape(TapeId);
            var toDelete = _reviewRepository.GetAllReviews().FirstOrDefault(r => r.UserId == UserId && r.TapeId == TapeId);
            if(toDelete == null) throw new ResourceNotFoundException($"Cannot delete non-existing review: no review was found by user with id {UserId} for tape with id {TapeId}.");
            _reviewRepository.DeleteReview(UserId, TapeId);
        }

        /// <summary>Validates user by id, throws error if no user exists with id</summary>
        /// <param name="UserId">user id to search user by</param>
        private void ValidateUser(int UserId)
        {
            var user = _userRepository.GetAllUsers().FirstOrDefault(u => u.Id == UserId);
            if (user == null) throw new ResourceNotFoundException($"User with id {UserId} not found.");
        }

        /// <summary>Validates user by id, throws error if no user exists with id</summary>
        /// <param name="UserId">user id to search user by</param>
        private void ValidateTape(int TapeId)
        {
            var tape = _tapeRepository.GetAllTapes().FirstOrDefault(t => t.Id == TapeId);
            if (tape == null) throw new ResourceNotFoundException($"Tape with id {TapeId} not found.");
        }
    }
}