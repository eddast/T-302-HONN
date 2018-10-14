using System.Collections.Generic;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.InputModels;
using VideotapesGalore.Repositories.Interfaces;

namespace VideotapesGalore.Repositories.Implementation
{
    public class TapeRepository : ITapeRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Tape"></param>
        /// <returns></returns>
        public int CreateTape(TapeInputModel Tape)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public void DeleteTape(int Id)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Tape"></param>
        public void EditTape(int Id, TapeInputModel Tape)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TapeDTO> GetAllTapes()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TapeDetailDTO GetTapeById(int Id)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TapeDTO> GetTapesBorrowedAtDate()
        {
            throw new System.NotImplementedException();
        }
    }
}