﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DICs_API.Models
{
    public class Period
    {
        public int Id { get; set; }
        public int Months { get; set; }
        public string Name { get; set; }
        public int Removed { get; set; }
    }
}
