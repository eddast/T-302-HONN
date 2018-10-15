using System;

namespace VideotapesGalore.Models.Entities {
  /// <summary>
  /// User entity model
  /// </summary>
  public class User {
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