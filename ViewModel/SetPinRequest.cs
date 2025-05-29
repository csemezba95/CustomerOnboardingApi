using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class SetPinRequest
{
    [Required(ErrorMessage = "PIN is required.")]
    [Range(100000, 999999, ErrorMessage = "PIN must be a 6-digit number.")]
    public int Pin { get; set; }
}
