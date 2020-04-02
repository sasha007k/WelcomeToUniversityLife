using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Enum
{
    public enum ZNOs
    {
        Math,
        Geography,
        Ukrainian,
        History,
        English,
        Spanish,
        French,
        Germany,
        Biology,
        Physics,
        Chemistry
    }

    public static class AllZNO
    {
        public static string GetZNOName(ZNOs zno)
        {
            return zno switch
            {
                ZNOs.Math => "Math",
                ZNOs.Geography => "Geography",
                ZNOs.Ukrainian => "Ukrainian",
                ZNOs.History => "History",
                ZNOs.English => "English",
                ZNOs.Spanish => "Spanish",
                ZNOs.French => "French",
                ZNOs.Germany => "Germany",
                ZNOs.Biology => "Biology",
                ZNOs.Physics => "Physics",
                ZNOs.Chemistry => "Chemistry",
                _ => string.Empty
            };
        }
    }
}
