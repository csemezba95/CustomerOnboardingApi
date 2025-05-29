using CustomerOnboardingApi.Data;
using CustomerOnboardingApi.Models;
using CustomerOnboardingApi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("send-resend-otp")]
    [SwaggerOperation(
      Summary = "Send OTP to customer's email and mobile by IC Number",
      Description = "Generates and sends a 4-digit OTP to a customer's registered mobile and email if the customer exists and no valid OTP (within 2 minutes) has been previously sent. Prevents resending within 2 minutes for security.",
      OperationId = "SendOtpByIC",
      Tags = new[] { "OTP Send Or Resend" }
  )]
    [SwaggerResponse(StatusCodes.Status200OK, "OTP sent successfully", typeof(OtpResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Customer not found")]
    public IActionResult SendOtp([FromBody] SendOtpByIcRequest request)
    {
        // Find customer by ICNumber
        var customer = _context.Customers.FirstOrDefault(c => c.ICNumber == request.ICNumber);

        if (customer == null)
        {
            return BadRequest(new
            {
                Message = "Customer does not exist. Please register first."
            });
        }

        // Check if an OTP was already sent within the last 2 minutes
        var existingOtp = _context.OtpVerifications
            .OrderByDescending(o => o.SentAt)
            .FirstOrDefault(o => o.MobileNumber == customer.MobileNumber && o.Email == customer.Email);

        if (existingOtp != null && existingOtp.SentAt.AddMinutes(2) > DateTime.UtcNow)
        {
            var remainingTime = existingOtp.SentAt.AddMinutes(2) - DateTime.UtcNow;

            int totalSeconds = (int)remainingTime.TotalSeconds;
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;

            string formattedTime;

            if (minutes > 0)
            {
                formattedTime = $"{minutes} minute(s) and {seconds} second(s)";
            }
            else
            {
                formattedTime = $"{seconds} second(s)";
            }

            return Ok(new OtpResponse
            {
                Message = $"OTP already sent recently. Please wait {formattedTime} before requesting a new one.",
                Otp = existingOtp.OtpCode
            });
        }


        // Generate 4-digit OTP
        var otp = new Random().Next(1000, 9999);

        // Save OTP record
        var otpRecord = new OtpVerification
        {
            MobileNumber = customer.MobileNumber,
            Email = customer.Email,
            OtpCode = otp,
            SentAt = DateTime.UtcNow
        };

        _context.OtpVerifications.Add(otpRecord);
        _context.SaveChanges();

        return Ok(new OtpResponse
        {
            Message = "OTP sent to registered mobile and email.",
            Otp = otp
        });
    }




}
