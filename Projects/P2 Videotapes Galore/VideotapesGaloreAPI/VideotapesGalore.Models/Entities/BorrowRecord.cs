using System;

namespace VideotapesGalore.Models.Entities {
  /// <summary>
  /// Borrow record entity model
  /// </summary>
  public class BorrowRecord {
    /// <summary>
    /// Borrow record unique ID
    /// </summary>
    public int Id { get; set; }
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
    /// <summary>
    /// Date of creation of model
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Date when model was last modified
    /// </summary>
    public DateTime LastModified { get; set; }
  }
}