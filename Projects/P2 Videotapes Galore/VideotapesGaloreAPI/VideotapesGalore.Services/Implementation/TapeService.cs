using System;
using System.Collections.Generic;
using System.IO;
using VideotapesGalore.Services.Interfaces;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.InputModels;
using VideotapesGalore.Models.Exceptions;
using VideotapesGalore.Repositories.Interfaces;

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
        /// Initialize repository
        /// </summary>
        /// <param name="tapeRepository">Which implementation of tape repository to use</param>
        public TapeService(ITapeRepository tapeRepository)
        {
            this._tapeRepository = tapeRepository;
        }

        /// <summary>
        /// Gets a list of all tapes in system
        /// </summary>
        /// <returns>List of tape dtos</returns>
        public List<TapeDTO> GetAllTapes()
        {
            return _tapeRepository.GetAllTapes();
        }

        /// <summary>
        /// Gets tape by id, throws exception if tape is not found by id
        /// </summary>
        /// <param name="Id">Id associated with tape in system</param>
        /// <returns>A tape detail dto<</returns>
        public TapeDetailDTO GetTapeById(int Id)
        {
            var tape = _tapeRepository.GetTapeById(Id);
            if (tape == null) throw new ResourceNotFoundException($"Video tape with id {Id} was not found.");
            return tape;
        }

        /// <summary>
        /// Creates new tape
        /// </summary>
        /// <param name="Tape">new tape to add</param>
        /// <returns>the Id of new tape</returns>
        public int CreateTape(TapeInputModel Tape)
        {
            return _tapeRepository.CreateTape(Tape);
        }

        /// <summary>
        /// Updates tape, throws resource not found exception if no tape is associated to Id
        /// </summary>
        /// <param name="Id">Id associated with tape in system to update</param>
        /// <param name="Tape">New information on tape to swap old information out for</param>
        public void EditTape(int Id, TapeInputModel Tape)
        {
            var oldTape = _tapeRepository.GetTapeById(Id);
            if (oldTape == null) throw new ResourceNotFoundException($"Video tape with id {Id} was not found.");
            else _tapeRepository.EditTape(Id, Tape);
        }

        /// <summary>
        /// Deletes tape, throws resource not found exception if no tape is associated to Id
        /// </summary>
        /// <param name="Id">Id associated with tape in system to delete</param>
        public void DeleteTape(int Id)
        {
            var tape = _tapeRepository.GetTapeById(Id);
            if (tape == null) throw new ResourceNotFoundException($"Video tape with id {Id} was not found.");
            else _tapeRepository.DeleteTape(Id);
        }

        /// <summary>
        /// Returns report on tape borrow record at a given date
        /// </summary>
        /// <param name="LoanDate">Date to use to output borrow records for</param>
        /// <returns>List of tape borrow record as report</returns>
        public List<TapeBorrowRecordDTO> GetTapeReportAtDate(DateTime LoanDate)
        {
            throw new NotImplementedException();
        }
    }
}