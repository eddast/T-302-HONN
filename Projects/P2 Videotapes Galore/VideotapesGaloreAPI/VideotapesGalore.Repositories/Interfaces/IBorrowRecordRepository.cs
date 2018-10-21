using System.Collections.Generic;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.Entities;
using VideotapesGalore.Models.InputModels;

namespace VideotapesGalore.Repositories.Interfaces
{
    /// <summary>
    /// Gets borrow record data from database and maps entity data to dto
    /// (e.g. in a format service layer understands)
    /// </summary>
    public interface IBorrowRecordRepository
    {
        /// <summary>
        /// Gets all borrow records from database
        /// </summary>
        /// <returns>List of all borrow records</returns>
        List<BorrowRecordDTO> GetAllBorrowRecords();
        /// <summary>
        /// Creates new entity model of borrow records and adds to database
        /// </summary>
        /// <param name="BorrowRecord">Borrow record input model to create entity borrow record from</param>
        /// <returns>The id of the new borrow record</returns>
        int CreateBorrowRecord(BorrowRecordDTO BorrowRecord);
        /// <summary>
        /// Updates borrow record by id
        /// </summary>
        /// <param name="Id">id of borrow record to update</param>
        /// <param name="BorrowRecord">new borrow record values to set to old borrow record</param>
        void EditBorrowRecord(int Id, BorrowRecordInputModel BorrowRecord);
        /// <summary>
        /// Deletes borrow record from system
        /// </summary>
        /// <param name="Id">the id of the borrow record to delete from system</param>
        void ReturnTape(int Id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TapeId"></param>
        List<BorrowRecord> GetBorrowRecordsForTape(int TapeId);
        /// <summary>
        /// Finds the newest borrow record for given user for given tape
        /// </summary>
<<<<<<< HEAD
        /// <param name="UserId"></param>
        /// <param name="TapeId"></param>
        List<BorrowRecord> GetBorrowRecordsForUser(int UserId, int TapeId);
=======
        /// <param name="UserId">Id of user to get newest borrow record of tape for</param>
        /// <param name="TapeId">Id of tape to get newest borrow record for by user</param>
        BorrowRecord GetCurrentBorrowRecordForUser(int UserId, int TapeId);
>>>>>>> 731f6f93afba9ba0c971d160273657dc51779f6b
        /// <summary>
        /// Deletes record from data source
        /// Not accessible by routes, only called when user or tape is deleted
        /// Causing cascading delete effect on borrow records
        /// </summary>
        /// <param name="Id">Id of borrow record to delete</param>
        void DeleteRecord(int Id);
    }
}