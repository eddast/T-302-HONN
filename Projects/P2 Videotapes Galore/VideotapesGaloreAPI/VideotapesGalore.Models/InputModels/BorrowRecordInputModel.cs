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
    [Display(Name = "Borrow Date")]
    [DataType(DataType.DateTime, ErrorMessage = "Borrow date must be a valid date")]
    [Required(ErrorMessage = "Borrow date is required")]
    public DateTime? BorrowDate { get; set; }
    /// <summary>
    /// Date of return for borrow
    /// </summary>
    [Display(Name = "Return Date")]
    [DataType(DataType.DateTime, ErrorMessage = "Return date must be a valid date")]
    public DateTime? ReturnDate { get; set; }
  }
}