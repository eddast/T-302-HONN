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
        List<TapeDTO> GetTapeReportAtDate(DateTime LoanDate);
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
        /// <summary>
        /// Gets all tapes that user has on loan by user id
        /// </summary>
        /// <param name="UserId">Id of user to get all tapes on loan for</param>
        /// <returns>List of tape borrow records for given user</returns>
        List<TapeBorrowRecordDTO> GetTapesForUserOnLoan(int UserId);
        /// <summary>
        /// Creates a new borrow record into database for today
        /// </summary>
        /// <param name="TapeId">Id of tape to loan</param>
        /// <param name="UserId">Id of user borrowing tape</param>
        /// <param name="BorrowRecord">Borrow record input model with dates</param>
        void CreateBorrowRecord(int TapeId, int UserId, BorrowRecordInputModel BorrowRecord = null);
        /// <summary>
        /// Updates borrow record
        /// </summary>
        /// <param name="TapeId">Id of tape to update record for</param>
        /// <param name="UserId">Id of user to update record for</param>
        /// <param name="BorrowRecord">The input model for borrow record to update to</param>
        void UpdateBorrowRecord(int TapeId, int UserId, BorrowRecordInputModel BorrowRecord);
        /// <summary>
        /// Returns borrow record
        /// </summary>
        /// <param name="TapeId">Id of tape to return</param>
        /// <param name="UserId">Id of user returning tape</param>
        void ReturnTape(int TapeId, int UserId);
    }
}