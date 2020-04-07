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
            {"Math", 0 },
            {"Geography",0 } ,
            {"Ukrainian",0 } ,
            {"History",0 } ,
            {"English",0 } ,
            {"Spanish",0 } ,
            {"French",0 } ,
            {"Germany",0 } ,
            {"Biology",0 } ,
            {"Physics",0 } ,
            {"Chemistry",0 } ,

        };
        public ChangePasswordModel ChangePasswordModel { get; set; }
        
    }
}
