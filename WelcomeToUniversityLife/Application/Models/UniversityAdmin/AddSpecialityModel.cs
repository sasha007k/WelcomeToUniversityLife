using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Models.UniversityAdmin
{
    public class AddSpecialityModel
    {
        [Required]
        public string SpecialityName { get; set; }
        public string Description { get; set; }
        [Required]
        public int FreeSpaces { get; set; }
        [Required]
        public int PaidSpaces { get; set; }
        public List<string> ZNO { get; set; }
        public int FacultyId { get; set; }
    }
}
