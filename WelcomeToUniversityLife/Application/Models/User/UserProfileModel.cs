using System;

namespace Application.Models.User
{
    public class UserProfileModel
    {
        public int id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }

        public DocsModel Docs { get; set; }
        public AddMarksModel MarksModel { get; set; }
        public ChangePasswordModel ChangePasswordModel { get; set; }
    }
}