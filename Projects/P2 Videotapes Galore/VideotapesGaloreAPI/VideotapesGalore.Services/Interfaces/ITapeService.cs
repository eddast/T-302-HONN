using System;
using System.Collections.Generic;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.InputModels;

namespace VideotapesGalore.Services.Interfaces
{
    /// <summary>
    /// Gets video tape data from repository in appropriate form
    /// Conducts any buisness logic necessary on data
    /// </summary>
    public interface ITapeService
    {
        /// <summary>
        /// Gets a list of all tapes in system
        /// </summary>
        /// <returns>List of tape dtos</returns>
        List<TapeDTO> GetAllTapes();
        /// <summary>
        /// Returns report on tape borrow record at a given date
        /// </summary>
        /// <param name="LoanDate">Date to use to output borrow records for</param>
        /// <returns>List of tape borrow record as report</returns>
        List<TapeBorrowRecordDTO> GetTapeReportAtDate(DateTime LoanDate);
        /// <summary>
        /// Gets tape by id, throws exception if tape is not found by id
        /// </summary>
        /// <param name="Id">Id associated with tape in system</param>
        /// <returns>A tape detail dto<</returns>
        TapeDetailDTO GetTapeById(int Id);
        /// <summary>
        /// Creates new tape
        /// </summary>
        /// <param name="Tape">new tape to add</param>
        /// <returns>the Id of new tape</returns>
        int CreateTape(TapeInputModel Tape);
        /// <summary>
        /// Updates tape
        /// </summary>
        /// <param name="Id">Id associated with tape in system to update</param>
        /// <param name="Tape">New information on tape to swap old information out for</param>
        void EditTape(int Id, TapeInputModel Tape);
        /// <summary>
        /// Deletes tape
        /// </summary>
        /// <param name="Id">Id associated with tape in system to delete</param>
        void DeleteTape(int Id);
    }
}