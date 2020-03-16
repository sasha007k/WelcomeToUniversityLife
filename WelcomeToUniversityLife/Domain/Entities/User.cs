using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User: IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public ZNO ZNO { get; set; }
        public int? ZNOId { get; set; }
        public ICollection<Document> Documents { get; set; } = new List<Document>();
    }
}