using System.ComponentModel.DataAnnotations;

namespace Application.Models.UniversityAdmin
{
    public class AddFacultyModel
    {
        [Required] public string FacultyName { get; set; }

        public string Address { get; set; }
        public string Description { get; set; }
        public int UniversityId { get; set; }
    }
}