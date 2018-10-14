using System;

namespace VideotapesGalore.Models.DTOs {
  /// <summary>
  /// Video tape data transfer object with details on previous borrows (e.g. borrow history)
  /// </summary>
  public class TapeDetailDTO {
    /// <summary>
    /// Video tape unique ID
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Video tape title
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// Director of video tape
    /// </summary>
    public string Director { get; set; }
    /// <summary>
    /// Video tape type (VHS or Betamax)
    /// </summary>
    public string Type { get; set; }
    /// <summary>
    /// Video tape release date
    /// </summary>
    public DateTime ReleaseDate { get; set; }
    /// <summary>
    /// Video tape's EIDR number
    /// </summary>
    public string EIDR { get; set; }
    public BorrowRecordDTO[] History { get; set; }
  }
}