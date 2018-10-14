using System;
using System.Collections.Generic;
using System.IO;
using VideotapesGalore.Services.Interfaces;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.InputModels;

namespace VideotapesGalore.Services.Implementations
{
    /// <summary>
    /// Defines error logging action in system for global error handling
    /// </summary>
    public class TapeService : ITapeService
    {
        public int CreateTape(TapeInputModel Tape)
        {
            throw new NotImplementedException();
        }

        public void DeleteTape(int Id)
        {
            throw new NotImplementedException();
        }

        public void EditTape(int Id, TapeInputModel Tape)
        {
            throw new NotImplementedException();
        }

        public List<TapeDTO> GetAllTapes()
        {
            throw new NotImplementedException();
        }

        public TapeDetailDTO GetTapeById(int Id)
        {
            throw new NotImplementedException();
        }

        public List<TapeBorrowRecordDTO> GetTapeReportAtDate(DateTime LoanDate)
        {
            throw new NotImplementedException();
        }
    }
}