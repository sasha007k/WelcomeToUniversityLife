﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.User
{
  public  class Zno
    {
      public  string Name { get; set; }
       public string Mark { get; set; }
    }

   public class AddMarksModel
    {
        public Zno FirstZno { get; set; }

    }
}
