using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CustomerOnboardingApi.ViewModel
{
    public class SendOtpByIcRequest
    {

        [Required]
        [Range(10000000000, 99999999999, ErrorMessage = "IC Number must be a 11-digit number.")]
        [DefaultValue(88021456631)]
        public long ICNumber { get; set; }
    }
}
