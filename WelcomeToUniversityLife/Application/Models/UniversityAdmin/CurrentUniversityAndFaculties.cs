using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Models.UniversityAdmin
{
    public class CurrentUniversityAndFaculties
    {
        public University CurrentUniversity { get; set; }
        public List<Faculty> Faculties { get; set; } = new List<Faculty>();
    }
}
