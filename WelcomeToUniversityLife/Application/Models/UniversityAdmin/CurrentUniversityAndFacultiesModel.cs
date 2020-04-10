using System.Collections.Generic;
using Domain.Entities;

namespace Application.Models.UniversityAdmin
{
    public class CurrentUniversityAndFacultiesModel
    {
        public University CurrentUniversity { get; set; }
        public List<Faculty> Faculties { get; set; } = new List<Faculty>();
    }
}