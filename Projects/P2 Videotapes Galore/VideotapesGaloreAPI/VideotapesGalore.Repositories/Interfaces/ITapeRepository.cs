using System.Collections.Generic;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.InputModels;

namespace VideotapesGalore.Repositories.Interfaces
{
    public interface ITapeRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<TapeDTO> GetAllTapes();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        TapeDetailDTO GetTapeById(int Id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Tape"></param>
        /// <returns></returns>
        int CreateTape(TapeInputModel Tape);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Tape"></param>
        void EditTape(int Id, TapeInputModel Tape);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        void DeleteTape(int Id);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<TapeDTO> GetTapesBorrowedAtDate();
    }
}