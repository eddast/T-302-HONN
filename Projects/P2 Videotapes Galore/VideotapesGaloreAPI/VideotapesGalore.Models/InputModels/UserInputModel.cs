using System;
using System.ComponentModel.DataAnnotations;

namespace VideotapesGalore.Models.InputModels {
  /// <summary>
  /// User data transfer object
  /// </summary>
  public class UserInputModel {
    /// <summary>
    /// User name
    /// </summary>
    [Display(Name = "User name")]
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(150, ErrorMessage = "User name too long - maximum length is 150 characters")]
    public string Name { get; set; }
    /// <summary>
    /// User email
    /// </summary>
    [Display(Name = "User email")]
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email must be a valid email address")]
    [MaxLength(150, ErrorMessage = "Email too long - maximum length is 150 characters")]
    public string Email { get; set; }
    /// <summary>
    /// User phone number
    /// </summary>
    [Display(Name = "User phone number")]
    [Required(ErrorMessage = "Phone number is required")]
    [MaxLength(15, ErrorMessage = "Phone number too long - maximum length is 15 characters")]
    public string Phone { get; set; }
    /// <summary>
    /// User address
    /// </summary>
    [Display(Name = "User address")]
    [Required(ErrorMessage = "Address is required")]
    [MaxLength(150, ErrorMessage = "Address too long - maximum length is 150 characters")]
    public string Address { get; set; }
  }
}