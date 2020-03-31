using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Models.SiteAdmin
{
    public class AddUniversityModel
    {
        public string UniversityName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords mismatch")]
        public string ConfirmPassword { get; set; }
    
    }
}
