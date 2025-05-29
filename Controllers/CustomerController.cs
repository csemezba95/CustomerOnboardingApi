using CustomerOnboardingApi.Data;
using CustomerOnboardingApi.Models;
using CustomerOnboardingApi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CustomerController(ApplicationDbContext context)
    {
        _context = context;
    }
   
    [HttpPost("register")]
    [SwaggerOperation(
        Summary = "Register a new customer",
        Description = "Registers a new customer only if the provided IC Number is unique. If the IC Number already exists in the system, the API returns a warning message indicating that an account is already registered and prompts the user to log in instead. If the IC Number is not found, the customer is registered successfully, and a One-Time Password (OTP) is sent to both the provided mobile number and email address for verification. The IC Number must be exactly 12 digits in length. After successful registration, a 4-digit OTP is sent to the customer's mobile number and email address associated with the provided IC Number. The user can then proceed to verify the OTP and complete the login process.",
        Tags = new[] { "New Customer Registration" }
    )]
    [SwaggerResponse(200, "Customer registered and OTP sent", typeof(OtpResponse))]
    [SwaggerResponse(400, "Validation failed or duplicate ICNumber")]
    public IActionResult Register([FromBody] CustomerRegistrationRequest customer)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(new
            {
                Title = "Validation Failed",
                Errors = errors
            });
        }

        if (_context.Customers.Any(c => c.ICNumber == customer.ICNumber))
        {
            return BadRequest(new
            {
                Title = "Account already exists.",
                Message = "There is an account registered with the IC number. Please login to continue."
            });
        }

        var newCustomer = new Customer
        {
            Name = customer.Name,
            ICNumber = (long)customer.ICNumber,
            MobileNumber = customer.MobileNumber,
            Email = customer.Email,
            IsVerified = false
        };

        _context.Customers.Add(newCustomer);
        _context.SaveChanges();

        var otp = new Random().Next(1000, 9999);

        _context.OtpVerifications.Add(new OtpVerification
        {
            MobileNumber = customer.MobileNumber,
            Email = customer.Email,
            OtpCode = otp,
            SentAt = DateTime.UtcNow
        });

        _context.SaveChanges();

        return Ok(new OtpResponse
        {
            Message = "Customer registered successfully. OTP sent.",
            Otp = otp
        });
    }


    [HttpPost("verify-otp")]
    [SwaggerOperation(
      Summary = "Verify OTP for a customer",
      Description = "After a customer receives the 4-digit numeric OTP, they must verify it within 2 minutes, as the OTP will automatically expire after that time. If already verified, the system will return a message indicating that.",
      OperationId = "VerifyOtp",
      Tags = new[] { "Verify OTP" }
  )]
    [SwaggerResponse(StatusCodes.Status200OK, "OTP verified")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid or expired OTP")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Customer not found")]
    public IActionResult VerifyOtp([FromBody] OtpVerifyRequest request)
    {
        var otpRecord = _context.OtpVerifications
            .OrderByDescending(o => o.SentAt)
            .FirstOrDefault(o => o.OtpCode == request.Otp);

        if (otpRecord == null || otpRecord.SentAt.AddMinutes(2) < DateTime.UtcNow)
        {
            return BadRequest(new { Message = "Invalid or expired OTP." });
        }

        var customer = _context.Customers.FirstOrDefault(c =>
            c.MobileNumber == otpRecord.MobileNumber && c.Email == otpRecord.Email);

        if (customer == null)
        {
            return NotFound(new { Message = "Customer not found." });
        }

        if (customer.IsVerified == true)
        {
            return Ok(new
            {
                Message = "Customer already verified.",
                CustomerId = customer.Id
            });
        }

        customer.IsVerified = true;
        _context.SaveChanges();

        return Ok(new
        {
            Message = "OTP verified.",
            CustomerId = customer.Id
        });
    }


    [HttpPost("set-pin/{id}")]
    [SwaggerOperation(
    Summary = "Set customer PIN",
    Description = "This API endpoint allows a verified customer to set their personal 6-digit PIN. Before calling this endpoint, the customer must have completed the OTP verification process, confirming their identity. The PIN must be exactly 6 digits and numeric, providing a secure method for future authentication. If the customer is not verified, the request will be denied to ensure only authenticated users can set their PIN. Use user IDs such as 1 or 2 because these users already exist in the database.",
    Tags = new[] { "Set PIN" }
)]
    [SwaggerResponse(StatusCodes.Status200OK, "PIN set successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Customer not verified or PIN invalid")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Customer not found")]
    public IActionResult SetPin(int id, [FromBody] SetPinRequest request)
    {
        var customer = _context.Customers.Find(id);
        if (customer == null)
            return NotFound(new { Message = "Customer not found." });

        if (customer.IsVerified == false)
            return BadRequest(new { Message = "Customer must be verified before setting PIN." });

        if (request.Pin < 100000 || request.Pin > 999999)
            return BadRequest(new { Message = "PIN must be a 6-digit number." });

        customer.Pin = request.Pin;
        _context.SaveChanges();

        return Ok(new { Message = "PIN set successfully." });
    }


    [HttpPost("enable-fingerprint/{id}")]
    [SwaggerOperation(
        Summary = "Enable fingerprint authentication for a verified customer",
        Description = "This API endpoint enables fingerprint authentication for a customer who has already completed the OTP verification and set their PIN. The customer must be verified (`IsVerified = true`) before fingerprint can be enabled. Use user IDs such as 1 or 2 if those users already exist in the database.",
        Tags = new[] { "Enable Fingerprint" }
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Fingerprint enabled successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Customer is not verified")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Customer not found")]
    public IActionResult EnableFingerprint(int id)
    {
        var customer = _context.Customers.Find(id);
        if (customer == null)
            return NotFound(new { Message = "Customer not found." });

        if (customer.IsVerified != true)
            return BadRequest(new { Message = "Customer must be verified before enabling fingerprint." });

        customer.FingerprintEnabled = true;
        _context.SaveChanges();

        return Ok(new { Message = "Fingerprint enabled successfully." });
    }


}
