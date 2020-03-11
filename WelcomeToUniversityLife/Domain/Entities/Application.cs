using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Application
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SpecialityId { get; set; }
    }
}
