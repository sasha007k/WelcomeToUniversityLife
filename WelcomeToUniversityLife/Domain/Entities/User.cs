using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public ZNO ZNO { get; set; }
        public int? ZNOId { get; set; }
        public int NumberOfApplications { get; set; }
        public University University { get; set; }
        public ICollection<Document> Documents { get; set; } = new List<Document>();
    }
}