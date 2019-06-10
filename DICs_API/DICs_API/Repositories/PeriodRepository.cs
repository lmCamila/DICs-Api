using Dapper;
using DICs_API.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DICs_API.Repositories
{
    public class PeriodRepository : AbstractRepository<Period>
    {
        public PeriodRepository(IConfiguration configuration) : base(configuration) { }

        public override bool Delete(int id)
        {
            using(IDbConnection db = new SqlConnection(ConnectionString))
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                if(db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                string query = "UPDATE PERIOD SET REMOVED = 1 WHERE ID = @Id";
                var result = db.Execute(query, new { Id = id });
                db.Close();
                return (result > 0);
            }
        }

        public override Period Get(int id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Period> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Period GetLastInserted()
        {
            throw new NotImplementedException();
        }

        public override bool Insert(Period item)
        {
            throw new NotImplementedException();
        }

        public override bool Update(Period item)
        {
            throw new NotImplementedException();
        }
    }
}
