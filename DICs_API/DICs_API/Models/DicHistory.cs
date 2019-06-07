using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DICs_API.Models
{
    public class DicHistory
    {
        public int Id { get; set; }
        public int IdDic { get; set; }
        public int IdStatusDic { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
    }
}
