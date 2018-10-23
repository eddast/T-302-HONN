using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.Exceptions;
using VideotapesGalore.Repositories.Interfaces;
using VideotapesGalore.Services.Interfaces;

namespace VideotapesGalore.Services.Implementation
{
    public class RecommendationService : IRecommendationService
    {
        /// <summary>Review repository</summary>
        private readonly IReviewRepository _reviewRepository;
        /// <summary>Tape repository</summary>
        private readonly ITapeRepository _tapeRepository;
        /// <summary>User repository</summary>
        private readonly IUserRepository _userRepository;
        /// <summary>Tape repository</summary>
        private readonly IBorrowRecordRepository _borrowRecordRepository;

        /// <summary>
        /// Initialize repositories
        /// </summary>
        /// <param name="reviewRepository">Which implementation of review repository to use</param>
        /// <param name="tapeRepository">Which implementation of tape repository to use</param>
        /// <param name="borrowRecordRepository">Which implementation of borrow record repository to use</param>
        public RecommendationService(IReviewRepository reviewRepository, ITapeRepository tapeRepository, IUserRepository userRepository, IBorrowRecordRepository borrowRecordRepository)
        {
            this._reviewRepository = reviewRepository;
            this._tapeRepository = tapeRepository;
            this._borrowRecordRepository = borrowRecordRepository;
            this._userRepository = userRepository;
        }

        /// <summary>
        /// Bases recommendation first and formost on common borrows, i.e. if user requesting recommendation has had some
        /// of the same tapes on loan as another user, they will get recommendation of that common borrower's previously borrowed tapes (if user hasn't seen them)
        /// If that fails, recommend highest rated tape in system that user has not seen
        /// If that fails, recommend the newest relesed tape in system that user has not seen
        /// If that fails, no recommendation can be made for user
        /// </summary>
        /// <param name="UserId">Id of user requesting recommendation</param>
        /// <returns>Tape recommendation</returns>
        public TapeRecommendationDTO GetRecommendationForUser(int UserId)
        {
            // Check if user exists, throw error if not
            var user = _userRepository.GetAllUsers().FirstOrDefault(u => u.Id == UserId);
            if (user == null) throw new ResourceNotFoundException($"User with id {UserId} was not found.");

            // Extact user borrow records, user borrow records and all tapes
            var allRecords = _borrowRecordRepository.GetAllBorrowRecords();
            var userBorrowRecords = allRecords.Where(r => r.UserId == UserId);
            var allTapes = _tapeRepository.GetAllTapes();

            // Attempt to get recommendation from common borrows
            var recommendation = GetRecommendationOfTapeFromCommonBorrows(UserId, allRecords, userBorrowRecords, allTapes);
            
            // If that did not work, attempt to get recommendation for highest rated tapes
            if(recommendation == null) {
                recommendation = GetRecommendationFromReviews(userBorrowRecords, allRecords, allTapes);
            }

            // If that did not work, attempt to get recommendation for newest tape user has not seen
            if(recommendation == null) {
                recommendation = GetRecommendationForNewestTapes(userBorrowRecords, allRecords, allTapes);
            }

            // If all fails we send not found error back to user because they seem to have rented all videos in system
            if(recommendation == null) throw new ResourceNotFoundException("User has had all tapes on loan that are currently available in system");
            
            // Otherwise return the recommendation
            else return recommendation;
        }


        /// <summary>
        /// Gets recommendation based on common borrows in system (highest precedence of recommendation)
        /// For each previous tape user requesting recommendation has had on loan, check for users that borrowed that tape as well
        /// If that common borrower has had tapes on loan that the user requesting recommendation has not had on loan, provide it as recommendation
        /// If no tape recommendation results from procedure, return null
        /// </summary>
        /// <param name="UserId">Id of user requesting recommendation</param>
        /// <param name="Tapes">List of all tapes in system</param>
        /// <returns>Recommendation of a tape or null</returns>
        private TapeRecommendationDTO GetRecommendationOfTapeFromCommonBorrows(int UserId, List<BorrowRecordDTO> AllRecords, IEnumerable<BorrowRecordDTO> UserBorrowRecords, List<TapeDTO> Tapes)
        {
            /// String to clarify reason for recommendation
            string recommendationReason = "Users that have borrowed some of the same tapes as you also borrowed this tape";

            // Find common borrows in system by other users
            // e.g. users that have sometime borrowed same tape as current user
            foreach(var userBorrowRecord in UserBorrowRecords)
            {
                var commonBorrowRecords = AllRecords.Where(r => r.TapeId == userBorrowRecord.TapeId && r.UserId != UserId);
                foreach(var commonBorrowRecord in commonBorrowRecords)
                {
                    /// Extact all records by common borrower 
                    var recordsByCommonBorrower = AllRecords.Where(r => r.UserId == commonBorrowRecord.UserId);
                    foreach(var recordByCommonBorrower in recordsByCommonBorrower)
                    {
                        // If common borrower has had tape on loan that current user has not borrowed yet,
                        // Recommend that tape
                        if(TapeCanBeRecommended(recordByCommonBorrower.TapeId, UserBorrowRecords, AllRecords)) {
                            var recommendation = Mapper.Map<TapeRecommendationDTO>(Tapes.FirstOrDefault(t => t.Id == recordByCommonBorrower.TapeId));
                            recommendation.RecommendationReason = recommendationReason;
                            return recommendation;
                        }
                    }
                }
            }
            // If no recommendation was found by checking common borrows,
            // Method returns null
            return null;
        }


        /// <summary>
        /// Gets recommendation for user based on reviews
        /// e.g. gets mean value from all ratings for tapes in system and recommends highest to user
        /// If no ratings are present in system or if user has had all rated tapes on loan return null
        /// </summary>
        /// <param name="UserBorrowRecords">Borrow records for user requesting recommendation</param>
        /// <param name="Tapes">All tapes in system</param>
        /// <returns>Recommendation of a tape or null</returns>
        private TapeRecommendationDTO GetRecommendationFromReviews(IEnumerable<BorrowRecordDTO> UserBorrowRecords,  IEnumerable<BorrowRecordDTO> AllBorrowRecords, List<TapeDTO> Tapes)
        {
            /// String to clarify reason for recommendation
            string recommendationReason = "This tape is the highest rated tape in system of the available tapes that user has not seen";

            // Group highest ratest tapes together and order by descending order of rating
            var allReviews = _reviewRepository.GetAllReviews();
            var highestRatedTapes = allReviews.GroupBy(r => r.TapeId)
                .Select(r => new ReviewDTO { TapeId = r.First().TapeId, UserId = 0, Rating = (int)((double)r.Sum(re => re.Rating)/(double)r.Count())})
                .ToList().OrderByDescending(r => r.Rating);

            // Check that if for each tape in system user has not borrowed a high rated tape
            // If not, recommend tape to user
            foreach(var highRatedTape in highestRatedTapes)
            {
                if(TapeCanBeRecommended(highRatedTape.TapeId, UserBorrowRecords, AllBorrowRecords) && highRatedTape.Rating != 0) {
                    var recommendation = Mapper.Map<TapeRecommendationDTO>(Tapes.FirstOrDefault(t => t.Id == highRatedTape.TapeId));
                    recommendation.RecommendationReason = recommendationReason;
                    return recommendation;
                }
            }

            // If either no ratings are present or user has had all rated tapes on loan,
            // return null for procedure
            return null;
        }

        /// <summary>
        /// Gets recommendation for user based on reviews
        /// e.g. gets mean value from all ratings for tapes in system and recommends highest to user
        /// If no ratings are present in system or if user has had all rated tapes on loan return null
        /// </summary>
        /// <param name="UserBorrowRecords">Borrow records for user requesting recommendation</param>
        /// <param name="Tapes">All tapes in system</param>
        /// <returns>Recommendation of a tape or null</returns>
        private TapeRecommendationDTO GetRecommendationForNewestTapes(IEnumerable<BorrowRecordDTO> UserBorrowRecords, IEnumerable<BorrowRecordDTO> AllBorrowRecords, List<TapeDTO> Tapes)
        {
            /// String to clarify reason for recommendation
            string recommendationReason = "This tape is the newest release out of the available tapes that user has not seen";

            // Order tapes by it's newest release date
            var newestTapes = Tapes.OrderByDescending(t => t.ReleaseDate);

            // Check that if for each tape in system user has not borrowed a newly released tape
            // If not, recommend tape to user
            foreach(var newTape in newestTapes)
            {
                if(TapeCanBeRecommended(newTape.Id, UserBorrowRecords, AllBorrowRecords)) {
                    var recommendation = Mapper.Map<TapeRecommendationDTO>(newTape);
                    recommendation.RecommendationReason = recommendationReason;
                    return recommendation;
                }
            }

            // If either no video tapes are present or user has had all tapes on loan,
            // return null for procedure
            return null;
        }

        /// <summary>
        /// Check if tape can be recommended, i.e. if user has not had it on loan and that it's not currently on loan
        /// </summary>
        /// <param name="TapeId">Id of tape in system to check if recommendation can be made</param>
        /// <param name="AllBorrowRecords">All borrow records in system</param>
        /// <returns></returns>
        private bool TapeCanBeRecommended(int TapeId, IEnumerable<BorrowRecordDTO> UserBorrowRecords, IEnumerable<BorrowRecordDTO> AllBorrowRecords) =>
            TapeHasNotBeenLoanedByUser(TapeId, UserBorrowRecords) && TapeAvailable(TapeId, AllBorrowRecords);

        /// <summary>
        /// Returns false if tape in on loan (unavailable) true otherwise
        /// </summary>
        /// <param name="TapeId">Id of tape to check for availability</param>
        /// <param name="AllBorrowRecords">All borrow records in system</param>
        /// <returns></returns>
        private bool TapeAvailable(int TapeId, IEnumerable<BorrowRecordDTO> AllBorrowRecords)
        {
            var tapeBorrowRecords = AllBorrowRecords.Where(r => r.TapeId == TapeId);
            foreach (var record in tapeBorrowRecords) {
                if (record.ReturnDate == null || record.ReturnDate == new DateTime(0)) {
                    return false;
                }
            }
            return true;
        } 

        /// <summary>
        /// Check if tape has not been loaned by user before
        /// </summary>
        /// <param name="TapeId">Id of tape to check if user has loaned before</param>
        /// <param name="UserBorrowRecords">All borrow records in system</param>
        /// <returns></returns>
        private bool TapeHasNotBeenLoanedByUser(int TapeId, IEnumerable<BorrowRecordDTO> UserBorrowRecords) =>
            UserBorrowRecords.FirstOrDefault(t => t.TapeId == TapeId) == null;
    }
}