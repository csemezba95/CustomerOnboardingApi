using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Customer
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;


    [Range(100000000000, 999999999999, ErrorMessage = "IC Number must be a 12-digit number.")]
    public long ICNumber { get; set; }

    [Required]
    [StringLength(11, MinimumLength = 11)]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "Mobile number must be exactly 11 digits.")]
    public string MobileNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Range(100000, 999999, ErrorMessage = "PIN must be a 6-digit number.")]
    public int? Pin { get; set; }  // Nullable

    public bool? IsVerified { get; set; } = null;  // Nullable

    public bool? FingerprintEnabled { get; set; } = null;  // Nullable
}
