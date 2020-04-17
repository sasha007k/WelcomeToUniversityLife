using Domain.Entities;
using System.Collections.Generic;

namespace Application.Models.UniversityAdmin
{
    public class CurrentFacultyAndSpecialitiesModel
    {
        public int? FacultyAdminId { get; set; }
        public Faculty CurrentFaculty { get; set; }
        public List<Speciality> Specialities { get; set; } = new List<Speciality>();
    }
}