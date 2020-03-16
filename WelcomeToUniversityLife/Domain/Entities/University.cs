using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class University
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int DocumentId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
