using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
        [Required]
        public Department Department { get; set; }
        public Process Process { get; set; }
        public byte IsLeaderDepartment { get; set; }
        public byte IsLeaderProcess { get; set; }
        public byte Removed { get; set; }
    }

    public class UsersUpload
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Avatar { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int Department { get; set; }
        public int Process { get; set; }
        public int IsLeaderDepartment { get; set; }
        public int IsLeaderProcess { get; set; }
        public int Removed { get; set; }
    }
    public class UserDics
    {
        public Users User { get; set; }
        public List<DIC> Dics { get; set; }
    }
    public static class UserDicsExtensions
    {
        public static UserDics ToUserDics(this IEnumerable<DIC> dics, Users user)
        {
            return new UserDics
            {
                User = user,
                Dics = dics.ToList()
            };
        }
    }
}
