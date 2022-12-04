using System.ComponentModel.DataAnnotations;

namespace MRA.Domain.Services.Authentication
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
