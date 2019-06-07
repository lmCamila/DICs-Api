using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DICs_API.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
        public int IdDepartment { get; set; }
        public int IdProcess { get; set; }
        public int IsLeaderDepartment { get; set; }
        public int IsLeaderProcess { get; set; }
        public int Removed { get; set; }
    }
}
