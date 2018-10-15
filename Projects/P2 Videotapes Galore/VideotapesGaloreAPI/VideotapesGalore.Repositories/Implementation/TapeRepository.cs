using System;
using System.Collections.Generic;
using AutoMapper;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.InputModels;
using VideotapesGalore.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using VideotapesGalore.Models.Entities;
using VideotapesGalore.Repositories.DBContext;
using System.Linq;

namespace VideotapesGalore.Repositories.Implementation
{
    /// <summary>
    /// Gets tape data from database and maps entity data to dto
    /// (e.g. in a format service layer understands)
    /// </summary>
    public class TapeRepository : ITapeRepository
    {
        /// <summary>
        /// Database context to use (MySQL connection)
        /// </summary>
        private VideotapesGaloreDBContext _dbContext;

        /// <summary>
        /// Set database context
        /// </summary>
        /// <param name="dbContext">database context to use</param>
        public TapeRepository(VideotapesGaloreDBContext dbContext) =>
            this._dbContext = dbContext;

        /// <summary>
        /// Gets all tapes from database
        /// </summary>
        /// <returns>List of all tapes</returns>
        public List<TapeDTO> GetAllTapes() =>
            Mapper.Map<List<TapeDTO>>(_dbContext.Tapes.ToList());

        /// <summary>
        /// Creates new entity model of tape and adds to database
        /// </summary>
        /// <param name="Tape">Tape input model to create entity tape from</param>
        /// <returns>The id of the new video tape</returns>
        public int CreateTape(TapeInputModel Tape)
        {
            _dbContext.Tapes.Add(Mapper.Map<Tape>(Tape));
            _dbContext.SaveChanges();
            return _dbContext.Tapes.ToList().OrderByDescending(t => t.CreatedAt).FirstOrDefault().Id;
        }

        /// <summary>
        /// Updates tape by id
        /// </summary>
        /// <param name="Id">id of tape to update</param>
        /// <param name="Tape">new tape values to set to old tape</param>
        public void EditTape(int Id, TapeInputModel Tape)
        {
            var updateModel = Mapper.Map<Tape>(Tape);
            var toUpdate = _dbContext.Tapes.FirstOrDefault(tape => tape.Id == Id);
            _dbContext.Attach(toUpdate);
            this.UpdateTape(ref toUpdate, updateModel);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Deletes tape from system
        /// </summary>
        /// <param name="Id">the id of the tape to delete from system</param>
        public void DeleteTape(int Id)
        {
            _dbContext.Tapes.Remove(_dbContext.Tapes.FirstOrDefault(tape => tape.Id == Id));
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Helper method, updates relevant values of tape to destination tape
        /// </summary>
        /// <param name="src">tape to be updated</param>
        /// <param name="dst">update model for tape</param>
        private void UpdateTape(ref Tape src, Tape dst)
        {
            src.Title = dst.Title;
            src.Director = dst.Director;
            src.Type = dst.Type;
            src.ReleaseDate = dst.ReleaseDate;
            src.EIDR = dst.EIDR;
            src.LastModified = dst.LastModified;
        }
    }
}