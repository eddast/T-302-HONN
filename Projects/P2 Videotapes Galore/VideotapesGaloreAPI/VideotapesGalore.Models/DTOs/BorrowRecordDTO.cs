using System;

namespace VideotapesGalore.Models.DTOs
{
    public class BorrowRecordDTO
    {
        /// <summary>
        /// User ID of user that borrowed tape
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Video tape ID of video tape being borrowed
        /// </summary>
        public int TapeId { get; set; }
        /// <summary>
        /// Date when tape was borrowed
        /// </summary>
        public DateTime BorrowDate { get; set; }
        /// <summary>
        /// Date of return for borrow
        /// </summary>
        public DateTime? ReturnDate { get; set; }
    }
}