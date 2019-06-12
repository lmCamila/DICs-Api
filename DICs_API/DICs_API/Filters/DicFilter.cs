using DICs_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DICs_API.Filters
{
    public class DicFilter
    {
        public string User { get; set; }
        public string Department { get; set; }
        public string Process { get; set; }
        public string Period { get; set; }
        public string Status { get; set; }
        public string Finished { get; set; }
    }

    public static class DicFilterExtensions
    {
        public static IQueryable<DIC>Filter(this IQueryable<DIC> query, DicFilter filter)
        {
            if(filter != null)
            {
                if (!string.IsNullOrEmpty(filter.User))
                {
                    query = query.Where(d => d.User.Name.ToLower().Contains(d.User.Name.ToLower()));
                }
                if (!string.IsNullOrEmpty(filter.Department))
                {
                    query = query.Where(d => d.User.Department.Name.ToLower().Contains(d.User.Department.Name.ToLower()));
                }
                if (!string.IsNullOrEmpty(filter.Process))
                {
                    query = query.Where(d => d.User.Process.Name.ToLower().Contains(d.User.Process.Name.ToLower()));
                }
                if (!string.IsNullOrEmpty(filter.Period))
                {
                    query = query.Where(d => d.Period.Name.ToLower().Contains(d.Period.Name.ToLower()));
                }
                if (!string.IsNullOrEmpty(filter.Status))
                {
                    query = query.Where(d => d.Status.Name.ToLower().Contains(d.Status.Name.ToLower()));
                }
            }
            return query;
        }
    }
}
