using System;
using System.Collections.Generic;

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

        public List<string> GetNotNullMarks()
        {
            var notNullSubjects = new List<string>();
            if (Math != 0) notNullSubjects.Add("Math");
            if (Geography != 0) notNullSubjects.Add("Geography");
            if (Ukrainian != 0) notNullSubjects.Add("Ukrainian");
            if (History != 0) notNullSubjects.Add("History");
            if (English != 0) notNullSubjects.Add("English");
            if (Spanish != 0) notNullSubjects.Add("Spanish");
            if (French != 0) notNullSubjects.Add("French");
            if (Germany != 0) notNullSubjects.Add("Germany");
            if (Biology != 0) notNullSubjects.Add("Biology");
            if (Physics != 0) notNullSubjects.Add("Physics");
            if (Chemistry != 0) notNullSubjects.Add("Chemistry");

            return notNullSubjects;
        }

        public double GetAverageMark()
        {
            var nouNullSubjects = GetNotNullMarks();
            double sum = 0;
            if (Math != 0) sum += Math;
            if (Geography != 0) sum += Geography;
            if (Ukrainian != 0) sum += Ukrainian;
            if (History != 0) sum += History;
            if (English != 0) sum += English;
            if (Spanish != 0) sum += Spanish;
            if (French != 0) sum += French;
            if (Germany != 0) sum += Germany;
            if (Biology != 0) sum += Biology;
            if (Physics != 0) sum += Physics;
            if (Chemistry != 0) sum += Chemistry;

            return sum / nouNullSubjects.Count;
        }

        public double GetMark(string subject)
        {
            switch (subject)
            {
                case "Math":
                    return Math;
                case "Geography":
                    return Geography;
                case "Ukrainian":
                    return Ukrainian;
                case "History":
                    return History;
                case "English":
                    return English;
                case "Spanish":
                    return Spanish;
                case "French":
                    return French;
                case "Germany":
                    return Germany;
                case "Biology":
                    return Biology;
                case "Physics":
                    return Physics;
                case "Chemistry":
                    return Chemistry;
            }

            return 0.0;
        }

        public void ClearMarks()
        {
            Math = 0.0;
            Geography = 0.0;
            Ukrainian = 0.0;
            History = 0.0;
            English = 0.0;
            Spanish = 0.0;
            French = 0.0;
            Germany = 0.0;
            Biology = 0.0;
            Physics = 0.0;
            Chemistry = 0.0;
        }
    }
}