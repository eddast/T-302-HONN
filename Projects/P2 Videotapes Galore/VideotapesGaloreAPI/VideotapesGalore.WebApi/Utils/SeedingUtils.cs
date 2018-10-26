using System;
using System.Linq;
using System.Collections.Generic;
using VideotapesGalore.Models.Exceptions;
using VideotapesGalore.Models.InputModels;
using VideotapesGalore.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace VideotapesGalore.WebApi.Utils
{
    /// <summary>
    /// Functions to aid seeding of content in API from initialization file
    /// </summary>
    public static class SeedingUtils
    {
        /// <summary>
        /// Transform user JSON model from file to user input model
        /// </summary>
        /// <param name="userJSON">user json format from initialization file</param>
        /// <returns>user input model from user json object</returns>
        public static UserInputModel ConvertJsonToUserInputModel(dynamic userJSON)
        {
            return new UserInputModel {
                Name = $"{userJSON.first_name} {userJSON.last_name}",
                Email = userJSON.email,
                Phone = userJSON.phone,
                Address = userJSON.address,
            };
        }

        /// <summary>
        /// Transform borrow record JSON model from file to user input model
        /// </summary>
        /// <param name="borrowRecordJSON">borrow record json format from initialization file</param>
        /// <returns>borrow record input model from borrow record json object</returns>
        public static BorrowRecordInputModel ConvertJSONToBorrowRecordInputModel(dynamic borrowRecordJSON)
        {
            return new BorrowRecordInputModel {
                BorrowDate = Convert.ChangeType(borrowRecordJSON.borrow_date, typeof(DateTime)),
                ReturnDate = borrowRecordJSON.return_date != null ? Convert.ChangeType(borrowRecordJSON.return_date, typeof(DateTime)) : null
            };
        }

        /// <summary>
        /// Transform tape JSON model from file to tape input model
        /// </summary>
        /// <param name="tapeJSON">tape json format from initialization file</param>
        /// <returns>tape input model from tape json object</returns>
        public static TapeInputModel ConvertJSONToTapeInputModel(dynamic tapeJSON)
        {
            return new TapeInputModel {
                Title = tapeJSON.title,
                Director = $"{tapeJSON.director_first_name} {tapeJSON.director_last_name}",
                Type = tapeJSON.type,
                ReleaseDate = tapeJSON.release_date,
                EIDR = tapeJSON.eidr
            };
        }

        /// <summary>
        /// Creates borrow record from tapes for user
        /// </summary>
        /// <param name="userJSON">json object for user</param>
        /// <param name="tapeService">tape service with tape functionalities</param>
        public static void CreateTapesForUser(dynamic userJSON, ITapeService tapeService)
        {
            // Create all borrows associated with user after user was added
            if(userJSON.tapes != null) {
                foreach(var borrowRecord in userJSON.tapes) {
                    // Generate input model from json for borrow record
                    BorrowRecordInputModel record = ConvertJSONToBorrowRecordInputModel(borrowRecord);
                    // Check if borrow record input model is valid
                    var results = new List<ValidationResult>();
                    var context = new ValidationContext(record, null, null);
                    if (!Validator.TryValidateObject(record, context, results)) {
                        IEnumerable<string> errorList = results.Select(x => x.ErrorMessage);
                        throw new InputFormatException("Tapes borrow for user in initialization file improperly formatted.", errorList);
                    }
                    // Otherwise add to database
                    tapeService.CreateBorrowRecord((int) borrowRecord.id, (int) userJSON.id, record);
                }
            }
        }
    }
}