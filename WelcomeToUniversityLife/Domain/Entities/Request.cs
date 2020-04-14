namespace Domain.Entities
{
    public class Request
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public int SpecialityId { get; set; }
    }
}