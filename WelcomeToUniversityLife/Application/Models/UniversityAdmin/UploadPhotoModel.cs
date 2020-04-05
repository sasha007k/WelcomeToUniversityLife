using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Models.UniversityAdmin
{
   public class UploadPhotoModel
    {
        public int requestedUserId { get; set; }

        [Required]
        public int id { get; set; }
    }
}
