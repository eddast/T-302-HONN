using System;

namespace VideotapesGalore.Models.DTOs {
  /// <summary>
  /// User data transfer object
  /// </summary>
  public class UserDTO {
    /// <summary>
    /// User unique ID
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// User name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// User email
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// User phone number
    /// </summary>
    public string Phone { get; set; }
    /// <summary>
    /// User address
    /// </summary>
    public string Address { get; set; }
  }
}