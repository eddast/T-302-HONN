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
        /// Returns report on tape borrow record at a given date
        /// </summary>
        /// <param name="LoanDate">Date to use to output borrow records for</param>
        /// <returns>List of tape borrow record as report</returns>
        public List<TapeBorrowRecordDTO> GetTapeReportAtDate(DateTime LoanDate)
        {
            throw new NotImplementedException();
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
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<TapeDTO> GetTapesForUserOnLoan(int UserId)
        {
            var UserRecords = _borrowRecordRepository.GetAllBorrowRecords().Where(t => t.UserId == UserId && t.ReturnDate == null).ToList();
            List<TapeDTO> Tapes = new List<TapeDTO>();
            var AllTapes = _tapeRepository.GetAllTapes();
            foreach (var Record in UserRecords) {
                var Tape = AllTapes.FirstOrDefault(t => t.Id == Record.TapeId);
                if (Tape == null) throw new ResourceNotFoundException($"Video tape with id {Record.TapeId} does not exist.");
                Tapes.Add(Tape);
            }
            return Tapes;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TapeId"></param>
        /// <param name="UserId"></param>
        /// <param name="BorrowRecord"></param>
        public void CreateBorrowRecord(int TapeId, int UserId, BorrowRecordInputModel BorrowRecord)
        {
            var Tape = _tapeRepository.GetAllTapes().FirstOrDefault(t => t.Id == TapeId);
            if (Tape == null) throw new ResourceNotFoundException($"Video tape with id {TapeId} does not exist.");
            var User = _userRepository.GetAllUsers().FirstOrDefault(t => t.Id == UserId);
            if (User == null) throw new ResourceNotFoundException($"User with id {UserId} does not exist.");

            var Record = Mapper.Map<BorrowRecordMinimalDTO>(BorrowRecord);
            Record.TapeId = TapeId;
            Record.UserId = UserId;
            _borrowRecordRepository.CreateBorrowRecord(Record);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TapeId"></param>
        /// <param name="UserId"></param>
        /// <param name="BorrowRecord"></param>
        public void UpdateBorrowRecord(int TapeId, int UserId, BorrowRecordInputModel BorrowRecord)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TapeId"></param>
        /// <param name="UserId"></param>
        public void RemoveBorrowRecord(int TapeId, int UserId)
        {
            throw new NotImplementedException();
        }
    }
}