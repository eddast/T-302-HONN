using System;
using System.Collections.Generic;
using AutoMapper;
using VideotapesGalore.Models.DTOs;
using VideotapesGalore.Models.InputModels;
using VideotapesGalore.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using VideotapesGalore.Models.Entities;

namespace VideotapesGalore.Repositories.Implementation
{
    public class TapeRepository : ITapeRepository
    {
        private string _connectionString = "server=videotapes-galore-db.c5qvh3fggdj0.eu-west-2.rds.amazonaws.com;port=3306;database=videotapesGalore;user=eddast;password=Margret1337";
        // private string _connectionString;
        // public TapeRepository( string connectionString ) =>
        //     this._connectionString = connectionString;
        private MySqlConnection GetConnection() =>
            new MySqlConnection(this._connectionString);

        /// <summary>
        /// Gets all tapes from database
        /// </summary>
        /// <returns>List of all tapes</returns>
        public List<TapeDTO> GetAllTapes()  
        {  
            List<Tape> tapes = new List<Tape>();
            using (MySqlConnection conn = GetConnection())  
            {  
                conn.Open();  
                var GetAllTapesCmd = new MySqlCommand("select * from Tapes", conn);  
                using (var reader = GetAllTapesCmd.ExecuteReader())  
                {  
                    while (reader.Read())  
                    {  
                        tapes.Add(new Tape()  
                        {  
                            Id = Convert.ToInt32(reader["Id"]),  
                            Title = reader["title"].ToString(),   
                            Director = reader["director"].ToString(),  
                            Type = reader["type"].ToString(),  
                            ReleaseDate = DateTime.Now,
                            EIDR = reader["eidr"].ToString(),
                            CreatedDate = DateTime.Now,
                            LastModified = DateTime.Now
                        });  
                    }  
                }  
            }
            return Mapper.Map<List<TapeDTO>>(tapes);  
        } 
        /// <summary>
        /// Gets a tape by their Id from database
        /// </summary>
        /// <param name="Id">Id associated with tape in system</param>
        /// <returns>The tape by id (null if not found)</returns>
        public TapeDetailDTO GetTapeById(int Id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Creates new entity model of tape and adds to database
        /// </summary>
        /// <param name="Tape">Tape input model to create entity tape from</param>
        /// <returns>The id of the new video tape</returns>
        public int CreateTape(TapeInputModel Tape)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Updates tape by id
        /// </summary>
        /// <param name="Id">id of tape to update</param>
        /// <param name="Tape">new tape values to set to old tape</param>
        public void EditTape(int Id, TapeInputModel Tape)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Deletes tape from system
        /// </summary>
        /// <param name="Id">the id of the tape to delete from system</param>
        public void DeleteTape(int Id)
        {
            throw new NotImplementedException();
        }
    }
}