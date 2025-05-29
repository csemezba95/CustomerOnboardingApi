using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class OtpVerifyRequest
{
    [Required(ErrorMessage = "OTP is required.")]
    [Range(1000, 9999, ErrorMessage = "OTP must be a 4-digit number.")]
    [DefaultValue(7809)]
    public int Otp { get; set; }

}
