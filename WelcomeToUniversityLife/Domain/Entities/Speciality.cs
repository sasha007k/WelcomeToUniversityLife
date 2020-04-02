using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Speciality
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FreeSpaces { get; set; }
        public int PaidSpaces { get; set; }
        public string RequiredZNO1 { get; set; }
        public string RequiredZNO2 { get; set; }
        public string RequiredZNO3 { get; set; }
        public int FacultyId { get; set; }
    }
}
