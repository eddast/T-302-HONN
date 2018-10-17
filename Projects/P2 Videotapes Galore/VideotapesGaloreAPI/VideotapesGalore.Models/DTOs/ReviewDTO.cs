using System;

namespace VideotapesGalore.Models.DTOs {
  /// <summary>
  /// Review data transfer object
  /// </summary>
  public class ReviewDTO {
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
  }
}