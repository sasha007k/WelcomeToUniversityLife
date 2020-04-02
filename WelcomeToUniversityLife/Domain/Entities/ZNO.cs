using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
