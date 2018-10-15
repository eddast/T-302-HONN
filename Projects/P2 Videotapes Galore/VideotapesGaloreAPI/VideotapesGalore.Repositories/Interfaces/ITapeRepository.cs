using System.Collections.Generic;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.InputModels;

namespace VideotapesGalore.Repositories.Interfaces
{
    /// <summary>
    /// Gets tape data from database and maps entity data to dto
    /// (e.g. in a format service layer understands)
    /// </summary>
    public interface ITapeRepository
    {
        /// <summary>
        /// Gets all tapes from database
        /// </summary>
        /// <returns>List of all tapes</returns>
        List<TapeDTO> GetAllTapes();
        /// <summary>
        /// Creates new entity model of tape and adds to database
        /// </summary>
        /// <param name="Tape">Tape input model to create entity tape from</param>
        /// <returns>The id of the new video tape</returns>
        int CreateTape(TapeInputModel Tape);
        /// <summary>
        /// Updates tape by id
        /// </summary>
        /// <param name="Id">id of tape to update</param>
        /// <param name="Tape">new tape values to set to old tape</param>
        void EditTape(int Id, TapeInputModel Tape);
        /// <summary>
        /// Deletes tape from system
        /// </summary>
        /// <param name="Id">the id of the tape to delete from system</param>
        void DeleteTape(int Id);
    }
}