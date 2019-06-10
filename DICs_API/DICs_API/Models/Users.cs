using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DICs_API.Models
{
    public class Users
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Avatar { get; set; }
        [Required]
        public string Email { get; set; }
        public string Pwd { get; set; }
        [Required]
        public Department Department { get; set; }
        public Process Process { get; set; }
        public int IsLeaderDepartment { get; set; }
        public int IsLeaderProcess { get; set; }
        public int Removed { get; set; }
    }
}
