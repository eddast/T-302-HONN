using System;
using System.ComponentModel.DataAnnotations;

namespace VideotapesGalore.Models.InputModels {
  /// <summary>
  /// Review input model for user to provide rating
  /// </summary>
  public class ReviewInputModel {
    /// <summary>
    /// User name
    /// </summary>
    [Display(Name = "User rating")]
    [Required(ErrorMessage = "Rating is required")]
    [Range(1, 5, ErrorMessage = "Rating must be a numeric value between 1-5")]
    public int? Rating { get; set; }
  }
}