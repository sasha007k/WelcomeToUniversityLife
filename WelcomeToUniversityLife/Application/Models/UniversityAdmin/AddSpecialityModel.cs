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
        public string RequiredZNO1 { get; set; }
        public string RequiredZNO2 { get; set; }
        public string RequiredZNO3 { get; set; }
        public int FacultyId { get; set; }
    }
}
