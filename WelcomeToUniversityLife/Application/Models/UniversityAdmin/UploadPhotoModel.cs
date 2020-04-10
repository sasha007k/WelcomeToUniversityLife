using System.ComponentModel.DataAnnotations;

namespace Application.Models.UniversityAdmin
{
    public class UploadPhotoModel
    {
        public int requestedUserId { get; set; }

        [Required] public int id { get; set; }
    }
}