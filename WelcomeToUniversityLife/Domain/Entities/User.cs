using Microsoft.AspNetCore.Identity;
using System;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User: IdentityUser<int>
    {
     public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public int UserInfoID { get; set; }
    
    }
}