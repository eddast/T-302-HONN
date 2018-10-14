using System;
using System.ComponentModel.DataAnnotations;

namespace VideotapesGalore.Models.InputModels {
  /// <summary>
  /// Borrow record data transfer object
  /// </summary>
  public class BorrowRecordInputModel {
    /// <summary>
    /// Date when tape was borrowed
    /// </summary>
    [Display(Name = "Release Date")]
    [DataType(DataType.DateTime, ErrorMessage = "Release date must be a valid date")]
    [Required(ErrorMessage = "Release date is required")]
    public DateTime? BorrowDate { get; set; }
    /// <summary>
    /// Date of return for borrow
    /// </summary>
    [Display(Name = "Release Date")]
    [DataType(DataType.DateTime, ErrorMessage = "Release date must be a valid date")]
    [Required(ErrorMessage = "Release date is required")]
    public DateTime? ReturnDate { get; set; }
  }
}