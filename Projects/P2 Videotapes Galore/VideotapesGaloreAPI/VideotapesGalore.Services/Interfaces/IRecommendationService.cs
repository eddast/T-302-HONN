using VideotapesGalore.Models.DTOs;

namespace VideotapesGalore.Services.Interfaces
{
    public interface IRecommendationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
         TapeRecommendationDTO GetRecommendationForUser(int UserId);
    }
}