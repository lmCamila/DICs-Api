using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DICs_API.Models
{
    public class DIC
    {
        public int id { get; set; }
        public int IdUser { get; set; }
        public int IdStatus { get; set; }
        public int IdPeriod { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime FinishedDate { get; set; }
    }
}
