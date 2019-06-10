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
        public Status StatusDic { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
    }
    public class DicHistoryUpload
    {
        public int Id { get; set; }
        public int  IdDic { get; set; }
        public int IdStatus { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
    }
    public class DicHistoryConfig
    {
        public DIC Dic { get; set; }
        public List<DicHistory> History { get; set; }
    }
    public static class DicHistoryConfigExtensions
    {
        public static DicHistoryConfig ToDicHistoryConfig( this IEnumerable<DicHistory> history, DIC dic)
        {
            return new DicHistoryConfig
            {
                Dic = dic,
                History = history.ToList()
            };
        }
    }
}
