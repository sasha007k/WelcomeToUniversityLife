using System.ComponentModel.DataAnnotations;

namespace Application.Models.User
{
    public class ChangePasswordModel
    {
        [Required]
        [Display(Name = "Old password")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Confirm password")]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords mismatch")]
        public string ConfirmNewPassword { get; set; }
    }
}