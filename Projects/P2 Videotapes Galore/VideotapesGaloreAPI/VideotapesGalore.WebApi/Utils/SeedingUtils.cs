using System;
using System.Linq;
using System.Collections.Generic;
using VideotapesGalore.Models.Exceptions;
using VideotapesGalore.Models.InputModels;
using VideotapesGalore.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace VideotapesGalore.WebApi.Utils
{
    public static class SeedingUtils
    {
        public static UserInputModel ConvertJsonToUserInputModel(dynamic userJSON)
        {
            return new UserInputModel {
                Name = $"{userJSON.first_name} {userJSON.last_name}",
                Email = userJSON.email,
                Phone = userJSON.phone,
                Address = userJSON.address,
            };
        }

        public static BorrowRecordInputModel ConvertJSONToBorrowRecordInputModel(dynamic borrowRecord)
        {
            return new BorrowRecordInputModel {
                BorrowDate = Convert.ChangeType(borrowRecord.borrow_date, typeof(DateTime)),
                ReturnDate = borrowRecord.return_date != null ? Convert.ChangeType(borrowRecord.return_date, typeof(DateTime)) : null
            };
        }

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