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
    public class ConfigurationRepository : AbstractRepository<Configuration>
    {
        public ConfigurationRepository(IConfiguration configuration) : base(configuration) { }
        public override bool Delete(int id)
        {
            throw new Exception("Não é permitida a exclusão.");
        }

        public override Configuration Get(int id)
        {
            using(IDbConnection db = new SqlConnection(ConnectionString))
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                if(db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                string query = "SELECT * FROM CONFIGURATION WHERE ID = @Id";
                var configuration = db.QueryFirst<Configuration>(query, new { Id = id });
                db.Close();
                return configuration;
            }
        }

        public override IEnumerable<Configuration> GetAll()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                IEnumerable<Configuration> configurations = db.Query<Configuration>("SELECT * FROM CONFIGURATION");
                return (List<Configuration>)configurations;
            }
        }

        public override Configuration GetLastInserted()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                var configuration = db.QueryFirst<Configuration>("SELECT * FROM CONFIGURATION WHERE ID = IDENT_CURRENT('CONFIGURATION')");
                db.Close();
                return configuration;
            }
        }

        public override bool Insert(Configuration item)
        {
            using(IDbConnection db = new SqlConnection(ConnectionString))
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                if(db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                string query = "INSERT INTO CONFIGURATION(ID_PERIOD) VALUES (@Period)";
                var result = db.Execute(query, new { Period = item.Period });
                db.Close();
                return result > 0;
            }
        }

        public override bool Update(Configuration item)
        {
            using(IDbConnection db = new SqlConnection(ConnectionString))
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                string query = "UPDATE CONFIGURATION SET ID_PERIOD = @Period";
                var result = db.Execute(query, new { Period = item.Period });
                db.Close();
                return result > 0;
            }
        }
    }
}
