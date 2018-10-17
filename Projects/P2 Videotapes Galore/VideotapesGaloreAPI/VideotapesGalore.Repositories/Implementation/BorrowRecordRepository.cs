using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.InputModels;
using VideotapesGalore.Models.Entities;
using VideotapesGalore.Repositories.DBContext;
using VideotapesGalore.Repositories.Interfaces;
using VideotapesGalore.Models.Exceptions;

namespace VideotapesGalore.Repositories.Implementation
{
    /// <summary>
    /// Gets borrow record data from database and maps entity data to dto
    /// (e.g. in a format service layer understands)
    /// </summary>
    public class BorrowRecordRepository : IBorrowRecordRepository
    {
        /// <summary>
        /// Database context to use (MySQL connection)
        /// </summary>
        private VideotapesGaloreDBContext _dbContext;

        /// <summary>
        /// Set database context
        /// </summary>
        /// <param name="dbContext">database context to use</param>
        public BorrowRecordRepository(VideotapesGaloreDBContext dbContext) =>
            this._dbContext = dbContext;

        /// <summary>
        /// Gets all borrow records from database
        /// </summary>
        /// <returns>List of all borrow records</returns>
        public List<BorrowRecordDTO> GetAllBorrowRecords() =>
            Mapper.Map<List<BorrowRecordDTO>>(_dbContext.BorrowRecords.ToList());

        /// <summary>
        /// Creates new entity model of borrow records and adds to database
        /// </summary>
        /// <param name="BorrowRecord">Borrow record input model to create entity borrow record from</param>
        /// <returns>The id of the new borrow record</returns>
        public int CreateBorrowRecord(BorrowRecordMinimalDTO BorrowRecord)
        {
            var BorrowRecords = _dbContext.BorrowRecords.Where(record => record.TapeId == BorrowRecord.TapeId);
            BorrowRecord toUpdate = null;
            if (BorrowRecords != null) {
                foreach (var Record in BorrowRecords) {
                    if (Record.ReturnDate == null || Record.ReturnDate == new DateTime(0)) {
                        toUpdate = Record;
                        break;
                    }
                }
            }
            if (toUpdate != null) throw new InputFormatException($"Tape with id {BorrowRecord.TapeId} already on loan");
            _dbContext.BorrowRecords.Add(Mapper.Map<BorrowRecord>(BorrowRecord));
            _dbContext.SaveChanges();
            return _dbContext.BorrowRecords.ToList().OrderByDescending(r => r.CreatedAt).FirstOrDefault().Id;
        }

        /// <summary>
        /// Updates borrow record by id
        /// </summary>
        /// <param name="Id">id of borrow record to update</param>
        /// <param name="BorrowRecord">new borrow record values to set to old borrow record</param>
        public void EditBorrowRecord(int TapeId, int UserId, BorrowRecordInputModel BorrowRecord)
        {
            var updateModel = Mapper.Map<BorrowRecord>(BorrowRecord);
            var BorrowRecords = _dbContext.BorrowRecords.Where(record => record.UserId == UserId && record.TapeId == TapeId);
            if (BorrowRecords == null) throw new ResourceNotFoundException($"No borrow record found for user with id {UserId} and tape with id {TapeId}");
            BorrowRecord toUpdate = null;
            foreach (var Record in BorrowRecords) {
                if (Record.ReturnDate == null || Record.ReturnDate == new DateTime(0)) {
                    toUpdate = Record;
                    break;
                }
            }
            if (toUpdate == null) throw new ResourceNotFoundException($"No borrow record found for user with id {UserId} and tape with id {TapeId}");
            _dbContext.Attach(toUpdate);
            this.UpdateBorrowRecord(ref toUpdate, updateModel);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Deletes borrow record from system
        /// </summary>
        /// <param name="Id">the id of the borrow record to delete from system</param>
        public void ReturnTape(int TapeId, int UserId)
        {
            var BorrowRecords = _dbContext.BorrowRecords.Where(record => record.UserId == UserId && record.TapeId == TapeId);
            if (BorrowRecords == null) throw new ResourceNotFoundException($"No borrow record found for user with id {UserId} and tape with id {TapeId}");
            BorrowRecord toUpdate = null;
            foreach (var Record in BorrowRecords) {
                if (Record.ReturnDate == null || Record.ReturnDate == new DateTime(0)) {
                    toUpdate = Record;
                    break;
                }
            }
            if (toUpdate == null || (toUpdate.ReturnDate != null && toUpdate.ReturnDate != new DateTime(0))) {
                throw new InputFormatException("For all matching records of borrows for this tape to this user, tape has already been returned");
            }
            _dbContext.Attach(toUpdate);
            toUpdate.ReturnDate = DateTime.Now;
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Helper method, updates relevant values of review to destination review
        /// </summary>
        /// <param name="src">tape to be updated</param>
        /// <param name="dst">update model for tape</param>
        private void UpdateBorrowRecord(ref BorrowRecord src, BorrowRecord dst)
        {
            src.BorrowDate = dst.BorrowDate;
            src.ReturnDate = dst.ReturnDate;
            src.LastModified = dst.LastModified;
        }
    }
}