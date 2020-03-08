using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User: IdentityUser<int>, IEntityBase
    {
        public string LastName { get; set; }

        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public int UserInfoID { get; set; }    
    }
}