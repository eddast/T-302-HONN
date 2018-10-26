using System.ComponentModel.DataAnnotations;
using System;

namespace VideotapesGalore.Models.InputModels {
  /// <summary>
  /// Video tape input model
  /// </summary>
  public class TapeInputModel {
    /// <summary>
    /// Video tape title
    /// </summary>
    [Display(Name = "Video tape title")]
    [Required(ErrorMessage = "Video tape title is required")]
    [MaxLength(150, ErrorMessage = "Video tape title too long - maximum length is 150 charachters")]
    public string Title { get; set; }
    /// <summary>
    /// Director of video tape
    /// </summary>
    [Display(Name = "Director")]
    [Required(ErrorMessage = "Director for video tape is required")]
    [MaxLength(150, ErrorMessage = "Director name too long - maximum length is 150 charachters")]
    public string Director { get; set; }
    /// <summary>
    /// Video tape type (VHS or Betamax)
    /// </summary>
    [Display(Name = "Video tape type")]
    [Required(ErrorMessage = "Video tape type is required")]
    [RegularExpression("VHS|Betamax", ErrorMessage = "Invalid type: should be either 'VHS' or 'Betamax'")]
    public string Type { get; set; }
    /// <summary>
    /// Video tape release date
    /// </summary>
    [Display(Name = "Release Date")]
    [DataType(DataType.DateTime, ErrorMessage = "Release date must be a valid date")]
    [Required(ErrorMessage = "Release date is required")]
    public DateTime? ReleaseDate { get; set; }
    /// <summary>
    /// Video tape's EIDR number
    /// </summary>
    [Display(Name = "EIDR number")]
    [Required(ErrorMessage = "EIDR number is required")]
    [StringLength(34, ErrorMessage = "Invalid EIDR: must be 34 characters")]
    [MinLength(34, ErrorMessage = "Invalid EIDR: must be 34 characters")]
    public string EIDR { get; set; }
  }
}