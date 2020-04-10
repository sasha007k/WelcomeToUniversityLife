namespace Domain.Entities
{
    public class University
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int DocumentId { get; set; }
        public string LocationLink { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}