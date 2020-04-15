using Domain.Entities;
using System.Collections.Generic;

namespace Application.Models.SpecialityModels
{
    public class SpecialityRating
    {
        public Speciality Speciality { get; set; }

        public List<RequestsInfo> Requests { get; set; }
    }
}
