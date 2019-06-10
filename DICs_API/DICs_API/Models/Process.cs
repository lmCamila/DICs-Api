﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DICs_API.Models
{
    public class Process
    {
        public int Id { get; set; }
        [Required]
        public Department Department { get; set; }
        [Required]
        public string Name { get; set; }
        public int Removed { get; set; }
    }
}