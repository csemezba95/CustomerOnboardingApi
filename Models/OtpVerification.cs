using System.ComponentModel.DataAnnotations;

namespace CustomerOnboardingApi.Models
{
    public class OtpVerification
    {
        public int Id { get; set; }

        [Required]
        [StringLength(11, ErrorMessage = "Mobile number must be 11 digits.")]
        public string MobileNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Range(1000, 9999, ErrorMessage = "OTP must be a 4-digit number.")]
        public int OtpCode { get; set; }

        public DateTime SentAt { get; set; } = DateTime.UtcNow; // Default to current UTC timestamp
    }
}
