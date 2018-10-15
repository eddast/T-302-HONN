using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.Entities;
using VideotapesGalore.Models.InputModels;
using VideotapesGalore.Repositories.DBContext;
using VideotapesGalore.Repositories.Interfaces;

namespace VideotapesGalore.Repositories.Implementation
{
    /// <summary>
    /// Gets reviews data from database and maps entity data to dto
    /// (e.g. in a format service layer understands)
    /// </summary>
    public class ReviewRepository : IReviewRepository
    {
        /// <summary>
        /// Database context to use (MySQL connection)
        /// </summary>
        private VideotapesGaloreDBContext _dbContext;

        /// <summary>
        /// Set database context
        /// </summary>
        /// <param name="dbContext">database context to use</param>
        public ReviewRepository(VideotapesGaloreDBContext dbContext) =>
            this._dbContext = dbContext;

        /// <summary>
        /// Gets all reviews from database
        /// </summary>
        /// <returns>List of all reviews from database</returns>
        public List<ReviewDTO> GetAllReviews() =>
            Mapper.Map<List<ReviewDTO>>(_dbContext.Reviews.ToList());

        /// <summary>
        /// Creates new entity model of review and adds to database
        /// </summary>
        /// <param name="UserId">Id of user associated with review</param>
        /// <param name="TapeId">Id of tape associated with review</param>
        /// <param name="Review">Review input model to create entity borrow record from</param>
        public void CreateReview(int UserId, int TapeId, ReviewInputModel Review)
        {
            var newReview = Mapper.Map<Review>(Review);
            newReview.TapeId = TapeId;
            newReview.UserId = UserId;
            _dbContext.Reviews.Add(newReview);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Updates review by review id
        /// </summary>
        /// <param name="Id">id of review to update</param>
        /// <param name="Review">new review values to set to old review</param>
        public void EditReview(int Id, ReviewInputModel Review)
        {
            var updateModel = Mapper.Map<Review>(Review);
            var toUpdate = _dbContext.Reviews.FirstOrDefault(review => review.Id == Id);
            _dbContext.Attach(toUpdate);
            toUpdate.Rating = updateModel.Rating;
            toUpdate.LastModified = updateModel.LastModified;
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Deletes review from system
        /// </summary>
        /// <param name="Id">the id of the review to delete from system</param>
        public void DeleteReview(int Id)
        {
            _dbContext.Reviews.Remove(_dbContext.Reviews.FirstOrDefault(review => review.Id == Id));
            _dbContext.SaveChanges();
        }
    }
}