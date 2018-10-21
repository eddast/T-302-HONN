using System;
using System.Collections.Generic;

namespace VideotapesGalore.Models.DTOs {
  /// <summary>
  /// Record for user along with user's borrow history for video tapes
  /// </summary>
  public class UserDetailDTO {
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
    /// History of user's borrows
    /// </summary>
    public IEnumerable<BorrowRecordDTO> History { get; set; }
  }
}