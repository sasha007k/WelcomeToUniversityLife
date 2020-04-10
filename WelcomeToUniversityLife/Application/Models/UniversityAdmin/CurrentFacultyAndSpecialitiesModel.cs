using System.Collections.Generic;
using Domain.Entities;

namespace Application.Models.UniversityAdmin
{
    public class CurrentFacultyAndSpecialitiesModel
    {
        public int? FacultyAdminId { get; set; }
        public Faculty CurrentFaculty { get; set; }
        public List<Speciality> Specialities { get; set; } = new List<Speciality>();
    }
}