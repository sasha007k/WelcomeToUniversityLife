namespace Application.Models.User
{
    public class ZnoModel
    {
        public string Name { get; set; }
        public string Mark { get; set; }
    }

    public class AddMarksModel
    {
        public ZnoModel FirstZnoModel { get; set; }
        public ZnoModel SecondZnoModel { get; set; }
        public ZnoModel ThirdZnoModel { get; set; }
        public ZnoModel FourZnoModel { get; set; }
    }
}