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
        BorrowRecord GetCurrentBorrowRecord(int TapeId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="TapeId"></param>
        BorrowRecord GetCurrentBorrowRecordForUser(int UserId, int TapeId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        void DeleteRecord(int Id);
    }
}