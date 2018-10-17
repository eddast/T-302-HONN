using System;
using System.Collections.Generic;
using System.IO;
using VideotapesGalore.Services.Interfaces;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.InputModels;
using AutoMapper;
using VideotapesGalore.Models.Exceptions;
using VideotapesGalore.Repositories.Interfaces;
using System.Linq;

namespace VideotapesGalore.Services.Implementations
{
    /// <summary>
    /// Defines error logging action in system for global error handling
    /// </summary>
    public class TapeService : ITapeService
    {
        /// <summary>
        /// Tape repository
        /// </summary>
        private readonly ITapeRepository _tapeRepository;
        /// <summary>
        /// Borrow record repository
        /// </summary>
        private readonly IBorrowRecordRepository _borrowRecordRepository;
        /// <summary>
        /// User repository
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initialize repository
        /// </summary>
        /// <param name="tapeRepository">Which implementation of tape repository to use</param>
        /// <param name="borrowRecordRepository">Which implementation of borrow record repository to use</param>
        /// <param name="userRepository">Which implementation of user repository to use</param>
        public TapeService(ITapeRepository tapeRepository, IBorrowRecordRepository borrowRecordRepository, IUserRepository userRepository)
        {
            this._tapeRepository = tapeRepository;
            this._borrowRecordRepository = borrowRecordRepository;
            this._userRepository = userRepository;
        }
        /// <summary>
        /// Gets a list of all tapes in system
        /// </summary>
        /// <returns>List of tape dtos</returns>
        public List<TapeDTO> GetAllTapes() =>
            _tapeRepository.GetAllTapes();

        /// <summary>
        /// Returns tapes filtered by that they were being loaned to a user at a given date
        /// </summary>
        /// <param name="LoanDate">Date to use to output borrow records for</param>
        /// <returns>List of tape borrow record as report</returns>
        public List<TapeDTO> GetTapeReportAtDate(DateTime LoanDate)
        {
            List<TapeDTO> tapesOnLoan = new List<TapeDTO>();
            var allTapes = Mapper.Map<List<TapeDTO>>(_tapeRepository.GetAllTapes());
            var allTapeBorrows = _borrowRecordRepository.GetAllBorrowRecords();
            foreach (var tape in allTapes) {
                var tapeBorrows = allTapeBorrows.Where(t => t.TapeId == tape.Id);
                foreach(var tapeBorrow in tapeBorrows) {
                    if (MatchesLoanDate(LoanDate, tapeBorrow.BorrowDate, tapeBorrow.ReturnDate)) {
                        tapesOnLoan.Add(tape);
                        break;
                    }
                }
            }
            return tapesOnLoan;
        }
        
        /// <summary>
        /// Gets tape by id, throws exception if tape is not found by id
        /// </summary>
        /// <param name="Id">Id associated with tape in system</param>
        /// <returns>A tape detail dto<</returns>
        public TapeDetailDTO GetTapeById(int Id)
        {
            var tape = _tapeRepository.GetAllTapes().FirstOrDefault(t => t.Id == Id);
            if (tape == null) throw new ResourceNotFoundException($"Video tape with id {Id} was not found.");
            var borrowRecords = _borrowRecordRepository.GetAllBorrowRecords().Where(t => t.TapeId == Id);
            var tapeDetails = Mapper.Map<TapeDetailDTO>(tape);
            tapeDetails.History = borrowRecords;
            return tapeDetails;
        }

        /// <summary>
        /// Creates new tape
        /// </summary>
        /// <param name="Tape">new tape to add</param>
        /// <returns>the Id of new tape</returns>
        public int CreateTape(TapeInputModel Tape) =>
            _tapeRepository.CreateTape(Tape);

        /// <summary>
        /// Updates tape, throws resource not found exception if no tape is associated to Id
        /// </summary>
        /// <param name="Id">Id associated with tape in system to update</param>
        /// <param name="Tape">New information on tape to swap old information out for</param>
        public void EditTape(int Id, TapeInputModel Tape)
        {
            var oldTape = _tapeRepository.GetAllTapes().FirstOrDefault(t => t.Id == Id);
            if (oldTape == null) throw new ResourceNotFoundException($"Video tape with id {Id} was not found.");
            else _tapeRepository.EditTape(Id, Tape);
        }

        /// <summary>
        /// Deletes tape, throws resource not found exception if no tape is associated to Id
        /// </summary>
        /// <param name="Id">Id associated with tape in system to delete</param>
        public void DeleteTape(int Id)
        {
            var tape = _tapeRepository.GetAllTapes().FirstOrDefault(t => t.Id == Id);
            if (tape == null) throw new ResourceNotFoundException($"Video tape with id {Id} was not found.");
            else _tapeRepository.DeleteTape(Id);
        }
        /// <summary>
        /// Gets all tapes that user has on loan by user id
        /// </summary>
        /// <param name="UserId">Id of user to get all tapes on loan for</param>
        /// <returns>List of tape borrow records for given user</returns>
        public List<TapeBorrowRecordDTO> GetTapesForUserOnLoan(int UserId)
        {
            var User = _userRepository.GetAllUsers().FirstOrDefault(t => t.Id == UserId);
            if (User == null) throw new ResourceNotFoundException($"User with id {UserId} does not exist.");
            var UserRecords = _borrowRecordRepository.GetAllBorrowRecords().Where(t => t.UserId == UserId && (t.ReturnDate == null || t.ReturnDate == new DateTime(0))).ToList();
            List<TapeBorrowRecordDTO> Tapes = new List<TapeBorrowRecordDTO>();
            var AllTapes = _tapeRepository.GetAllTapes();
            foreach (var Record in UserRecords) {
                var Tape = AllTapes.FirstOrDefault(t => t.Id == Record.TapeId);
                if (Tape == null) throw new ResourceNotFoundException($"Video tape with id {Record.TapeId} does not exist.");
                var TapeBorrowRecord = Mapper.Map<TapeBorrowRecordDTO>(Tape);
                TapeBorrowRecord.BorrowDate = Record.BorrowDate;
                TapeBorrowRecord.ReturnDate = null;
                Tapes.Add(TapeBorrowRecord);
            }
            return Tapes;
        }
        /// <summary>
        /// Creates a new borrow record into database for today
        /// </summary>
        /// <param name="TapeId">Id of tape to loan</param>
        /// <param name="UserId">Id of user borrowing tape</param>
        /// <param name="BorrowRecord">Borrow record input model with dates</param>
        public void CreateBorrowRecord(int TapeId, int UserId, BorrowRecordInputModel BorrowRecord)
        {
            ValidateBorrowRecord(TapeId, UserId);
            var currentRecord = _borrowRecordRepository.GetCurrentBorrowRecord(TapeId);
            if (currentRecord != null) throw new InputFormatException("Tape is already on loan");
            if (BorrowRecord == null) {
                BorrowRecord = new BorrowRecordInputModel{BorrowDate = DateTime.Now};
            }
            var Record = Mapper.Map<BorrowRecordMinimalDTO>(BorrowRecord);
            Record.TapeId = TapeId;
            Record.UserId = UserId;
            _borrowRecordRepository.CreateBorrowRecord(Record);
        }
        /// <summary>
        /// Updates borrow record
        /// </summary>
        /// <param name="TapeId">Id of tape to update record for</param>
        /// <param name="UserId">Id of user to update record for</param>
        /// <param name="BorrowRecord">The input model for borrow record to update to</param>
        public void UpdateBorrowRecord(int TapeId, int UserId, BorrowRecordInputModel BorrowRecord)
        {
            ValidateBorrowRecord(TapeId, UserId);
            var prevRecord = _borrowRecordRepository.GetCurrentBorrowRecordForUser(UserId, TapeId);
            if (prevRecord == null) throw new ResourceNotFoundException($"User does not have the specified tape on loan");
            _borrowRecordRepository.EditBorrowRecord(prevRecord.Id, BorrowRecord);
        }
        /// <summary>
        /// Returns borrow record
        /// </summary>
        /// <param name="TapeId">Id of tape to return</param>
        /// <param name="UserId">Id of user returning tape</param>
        public void ReturnTape(int TapeId, int UserId)
        {
            ValidateBorrowRecord(TapeId, UserId);
            var prevRecord = _borrowRecordRepository.GetCurrentBorrowRecordForUser(UserId, TapeId);
            if (prevRecord == null) throw new ResourceNotFoundException($"User does not have the specified tape on loan");
            _borrowRecordRepository.ReturnTape(prevRecord.Id);
        }
        /// <summary>
        /// Validates borrow record, i.e. if both user and tape exists
        /// </summary>
        /// <param name="TapeId">Id of tape in borrow record</param>
        /// <param name="UserId">Id of user in borrow record</param>
        private void ValidateBorrowRecord(int TapeId, int UserId)
        {
            var Tape = _tapeRepository.GetAllTapes().FirstOrDefault(t => t.Id == TapeId);
            if (Tape == null) throw new ResourceNotFoundException($"Video tape with id {TapeId} does not exist.");
            var User = _userRepository.GetAllUsers().FirstOrDefault(t => t.Id == UserId);
            if (User == null) throw new ResourceNotFoundException($"User with id {UserId} does not exist.");
        }
        /// <summary>
        /// Compares loan date to borrow and return date of tape, returns true if it's in between
        /// </summary>
        /// <param name="LoanDate">loan date for tape</param>
        /// <param name="BorrowDate">date of borrow for tape</param>
        /// <param name="ReturnDate">return date for tape</param>
        /// <returns></returns>
        private bool MatchesLoanDate(DateTime LoanDate, DateTime BorrowDate, DateTime? ReturnDate)
        { 
            if(ReturnDate.HasValue) return DateTime.Compare(LoanDate, ReturnDate.Value) < 0 && DateTime.Compare(LoanDate, BorrowDate) >= 0;
            else return DateTime.Compare(LoanDate, BorrowDate) >= 0;
        }
    }
}