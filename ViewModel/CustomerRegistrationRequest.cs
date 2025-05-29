using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace CustomerOnboardingApi.ViewModel
{
    public class CustomerRegistrationRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        [DefaultValue("Mezba Uddin")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "IC Number is required.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "IC Number must be a 11-digit number.")]
        [DefaultValue(88021456631)]
        public long? ICNumber { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [StringLength(15)]
        [DefaultValue("+8801920519595")]
        [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Invalid mobile number.")]
        public string MobileNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(100)]
        [DefaultValue("csemezba@gmail.com")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]{3,}@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Email must have at least 3 characters before '@'.")]
        public string Email { get; set; } = string.Empty;
    }
}
