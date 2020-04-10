using System.ComponentModel.DataAnnotations;

namespace Application.Models.Authentication
{
    public class RegisterModel
    {
        [Required] [EmailAddress] public string Email { get; set; }

        [Required] public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords mismatch")]
        public string ConfirmPassword { get; set; }
    }
}