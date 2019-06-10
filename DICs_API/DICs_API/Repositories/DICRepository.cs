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
    public class DICRepository : AbstractRepository<DIC>
    {
        public DICRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public override bool Delete(int id)
        {
            throw new Exception("Não é possível excluir um DIC.");
        }

        public override DIC Get(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                if(db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                var query = db.Query<DIC, Users, Status, Period, Department, Process, DIC>(@"SELECT d.*, u.*, s.*, p.*, dep.*, pro.* 
                          FROM DIC d INNER JOIN USERS u ON d.ID_USER = U.ID 
			                        INNER JOIN STATUS s ON d.ID_STATUS = s.ID 
                                    INNER JOIN PERIOD p ON d.ID_PERIOD = p.ID
			                        INNER JOIN DEPARTMENT dep ON u.ID_DEPARTMENT = dep.ID
			                        INNER JOIN PROCESS pro ON u.ID_PROCESS = pro.ID
                         WHERE d.ID = @IdDic"
                , (d, u, s, p, dep, pro) =>
                {
                    d.User = u;
                    d.User.Department = dep;
                    d.User.Process = pro;
                    d.Status = s;
                    d.Period = p;
                    return d;
                }, new { IdDic = id }, splitOn: "id, id, id, id").AsList();
                return query[0];
            }
        }

        public override IEnumerable<DIC> GetAll()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                var query = db.Query<DIC, Users, Status, Period, Department, Process, DIC>(@"SELECT d.*, u.*, s.*, p.*, dep.*, pro.* 
                          FROM DIC d INNER JOIN USERS u ON d.ID_USER = U.ID 
			                        INNER JOIN STATUS s ON d.ID_STATUS = s.ID 
                                    INNER JOIN PERIOD p ON d.ID_PERIOD = p.ID
			                        INNER JOIN DEPARTMENT dep ON u.ID_DEPARTMENT = dep.ID
			                        INNER JOIN PROCESS pro ON u.ID_PROCESS = pro.ID"
                , (d, u, s, p, dep, pro) =>
                {
                    d.User = u;
                    d.User.Department = dep;
                    d.User.Process = pro;
                    d.Status = s;
                    d.Period = p;
                    return d;
                }, null, splitOn: "id, id, id, id").AsList();
                return (List<DIC>)query;
            }
        }

        public override DIC GetLastInserted()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                var query = db.Query<DIC, Users, Status, Period, Department, Process, DIC>(@"
                            SELECT d.*, u.*, s.*, p.*, dep.*, pro.* 
                            FROM DIC d INNER JOIN USERS u ON d.ID_USER = U.ID 
			                     INNER JOIN STATUS s ON d.ID_STATUS = s.ID 
                                 INNER JOIN PERIOD p ON d.ID_PERIOD = p.ID
			                     INNER JOIN DEPARTMENT dep ON u.ID_DEPARTMENT = dep.ID
			                     INNER JOIN PROCESS pro ON u.ID_PROCESS = pro.ID
                            WHERE d.ID = IDENT_CURRENT('DIC')"
                , (d, u, s, p, dep, pro) =>
                {
                    d.User = u;
                    d.User.Department = dep;
                    d.User.Process = pro;
                    d.Status = s;
                    d.Period = p;
                    return d;
                }, null, splitOn: "id, id, id, id").AsList();
                return query[0];
            }
        }

        public override bool Insert(DIC item)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                if(db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                int result = db.Execute(@"INSERT INTO DIC(ID_USER, DESCRIPTION, START_DATE, ID_STATUS, ID_PERIOD)
                                            VALUES(@IdUsuario, @Descricao, GETDATE(), @IdStatus, @IdPeriod)", item);
                return (result > 0);
            }
        }

        public override bool Update(DIC item)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                int result = db.Execute(@"UPDATE DIC SET 	
                                        DESCRIPTION = @Descricao,
				                        FINISHED_DATE = @Data_conclu,
				                        ID_STATUS = @IdStatus 
                                        WHERE ID = @Id ", item);
                return (result > 0);
            }
        }
    }
}
