using VideotapesGalore.Models.DTOs;

namespace VideotapesGalore.Services.Interfaces
{
    public interface IRecommendationService
    {
        /// <summary>
        /// Provides recommendation to user
        /// </summary>
        /// <param name="UserId">Id of user to provide recommendation of tape to</param>
        /// <returns></returns>
         TapeRecommendationDTO GetRecommendationForUser(int UserId);
    }
}