using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.User
{
    public class UserProfileModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public Dictionary<string, double> ZNOs { get; set; } = new Dictionary<string, double>();
        public ChangePasswordModel ChangePasswordModel { get; set; }
    }
}
