using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DICs_API.Models
{
    public class Process
    {
        public int Id { get; set; }
        public int IdDepartment { get; set; }
        public string Name { get; set; }
        public int Removed { get; set; }
    }
}
