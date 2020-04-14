using System.Collections.Generic;

namespace Domain.Entities
{
    public class Speciality
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FreeSpaces { get; set; }
        public int PaidSpaces { get; set; }
        public string RequiredZNO1 { get; set; }
        public string RequiredZNO2 { get; set; }
        public string RequiredZNO3 { get; set; }
        public string RequiredZNO4 { get; set; }
        public int FacultyId { get; set; }

        public List<string> GetRequiredZNO()
        {
            var requiredZNO = new List<string>
            {
                RequiredZNO1,
                RequiredZNO2
            };

            if (!string.IsNullOrWhiteSpace(RequiredZNO3)) requiredZNO.Add(RequiredZNO3);
            if (!string.IsNullOrWhiteSpace(RequiredZNO4)) requiredZNO.Add(RequiredZNO4);

            return requiredZNO;
        }
    }
}