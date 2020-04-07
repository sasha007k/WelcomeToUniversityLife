﻿using System;
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
        public AddMarksModel MarksModel {get;set;}
        public ChangePasswordModel ChangePasswordModel { get; set; }
        
    }
}
