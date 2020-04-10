namespace Domain.Entities
{
    public class ZNO
    {
        public int Id { get; set; }
        public double Math { get; set; }
        public double Geography { get; set; }
        public double Ukrainian { get; set; }
        public double History { get; set; }
        public double English { get; set; }
        public double Spanish { get; set; }
        public double French { get; set; }
        public double Germany { get; set; }
        public double Biology { get; set; }
        public double Physics { get; set; }
        public double Chemistry { get; set; }
        public User User { get; set; }

        public void SetMark(string subject, double mark)
        {
            switch (subject)
            {
                case "Math":
                    Math = mark;
                    break;
                case "Geography":
                    Geography = mark;
                    break;
                case "Ukrainian":
                    Ukrainian = mark;
                    break;
                case "History":
                    History = mark;
                    break;
                case "English":
                    English = mark;
                    break;
                case "Spanish":
                    Spanish = mark;
                    break;
                case "French":
                    French = mark;
                    break;
                case "Germany":
                    Germany = mark;
                    break;
                case "Biology":
                    Biology = mark;
                    break;
                case "Physics":
                    Physics = mark;
                    break;
                case "Chemistry":
                    Chemistry = mark;
                    break;
            }
        }
    }
}