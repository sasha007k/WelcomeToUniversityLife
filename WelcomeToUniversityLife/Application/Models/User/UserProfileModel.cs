using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

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
        public Dictionary<string, double> ZNOs { get; set; } = new Dictionary<string, double>()
        { 
            { "Math", 2 },
            {"Geography",3 } ,
            {" Ukrainian",3 } ,
            {"History",3 } ,
            {"English",3 } ,
            {"Spanish",3 } ,
            {"French",3 } ,
            {"Germany",3 } ,
            {"Biology",3 } ,
            {"Physics",3 } ,
             {"Chemistry",3 } ,

        };
        public ChangePasswordModel ChangePasswordModel { get; set; }
        
    }
}
