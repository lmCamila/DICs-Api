using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DICs_API.Models
{
    public class DIC
    {
        public int id { get; set; }
        [Required]
        public Users User { get; set; }
        [Required]
        public Status Status { get; set; }
        [Required]
        public Period Period { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public DateTime FinishedDate { get; set; }
    }
}
