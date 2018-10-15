using System;

namespace VideotapesGalore.Models.Entities {
  /// <summary>
  /// Review entity model
  /// </summary>
  public class Review {
    /// <summary>
    /// Review unique ID
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// User ID of user that conducted review
    /// </summary>
    public int UserId { get; set; }
    /// <summary>
    /// Video tape ID of video tape being reviewed
    /// </summary>
    public int TapeId { get; set; }
    /// <summary>
    /// User provided rating for tape
    /// </summary>
    public int Rating { get; set; }
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