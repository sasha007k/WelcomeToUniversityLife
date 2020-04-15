﻿using System.Collections.Generic;

namespace Domain.Entities
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int DocumentId { get; set; }
        public int UniversityId { get; set; }
        public University University { get; set; }
        public ICollection<Speciality> Specialities { get; set; } = 
            new List<Speciality>();
    }
}