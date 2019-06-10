using Dapper;
using DICs_API.Models;
using DICs_API.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
namespace DICs_API.Repositories
{
    public class UsersRepository : AbstractRepository<Users>
    {
        public UsersRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public override Users Get(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                var query = db.Query<Users, Department, Process, Users>(@"SELECT u.*, d.*, p.* FROM users u 
                                  INNER JOIN DEPARTMENT d ON u.ID_DEPARTMENT = d.id
							      INNER JOIN PROCESS p ON u.ID_PROCESS = p.id
					  		WHERE u.id = @Id AND u.REMOVED = 0"
                , (u, d, p) =>
                {
                    u.Department = d;
                    u.Process = p;
                    return u;
                }, new { Id = id }, splitOn: "id, id, id").AsList();
                return query[0];
            }
        }

        public override IEnumerable<Users> GetAll()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                var query = db.Query<Users, Department, Process, Users>(@"SELECT u.*, d.*, p.* FROM users u 
                                  INNER JOIN DEPARTMENT d ON u.ID_DEPARTMENT = d.id
							      INNER JOIN PROCESS p ON u.ID_PROCESS = p.id
					  		WHERE u.REMOVED = 0"
                , (u, d, p) =>
                {
                    u.Department = d;
                    u.Process = p;
                    return u;
                }, null, splitOn: "id, id, id").AsList();
                return (List<Users>)query;
            }
        }

        public override bool Insert(Users item)
        {
            throw new Exception("Utilize o método que recebe como parâmetro o UsersUpload");
        }

        public bool Insert(UsersUpload item)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                int result = db.Execute(@"INSERT INTO USERS 
                                            (NAME, AVATAR, EMAIL, PWD, ID_DEPARTMENT, ID_PROCESS, IS_LEADER_DEPARTMENT, IS_LEADER_PROCESS, REMOVED)
		                                    VALUES(@Name, @Avatar, @Email, @IdDepartment, @IdProcess, @IsLeaderDepartment, @IsLeaderProcess, 0))",
                                            new {
                                                Name = item.Name,
                                                Avatar = item.Avatar,
                                                Email = item.Email,
                                                IdDepartment = item.Department,
                                                IdProcess = item.Process,
                                                IsLeaderDepartment = item.IsLeaderDepartment,
                                                IsLeaderProcess = item.IsLeaderProcess
                                            }
                );
                return (result > 0);

            }
        }

        public override bool Update(Users item)
        {
            throw new Exception("Utilize o método que recebe como parâmetro o UsersUpload");
        }

        public bool Update(UsersUpload item)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                int result = db.Execute(@"UPDATE USERS SET NAME = @Name,
		                                     AVATAR = @Avatar,
				                             EMAIL = @Email,
				                             ID_DEPARTMENT = @IdDepartment,
				                             ID_PROCESS = @IdProcess,
				                             IS_LEADER_DEPARTMENT = @IsLeaderDepartment,
				                             IS_LEADER_PROCESS = @IsLeaderProcess
				                             WHERE ID = @Id",
                                             new
                                             {
                                                 Name = item.Name,
                                                 Avatar = item.Avatar,
                                                 Email = item.Email,
                                                 IdDepartment = item.Department,
                                                 IdProcess = item.Process,
                                                 IsLeaderDepartment = item.IsLeaderDepartment,
                                                 IsLeaderProcess = item.IsLeaderProcess,
                                                 Id = item.Id
                                             }
                );
                return (result > 0);
            }
        }

        public override bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                int result = db.Execute(@"UPDATE USERS SET REMOVED = 1 WHERE ID = @Id", new { Id = id });
                return (result > 0);
            }
        }

        public override Users GetLastInserted()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                var query = db.Query<Users, Department, Process, Users>(@"SELECT u.*, d.*, p.* FROM users u 
                                  INNER JOIN DEPARTMENT d ON u.ID_DEPARTMENT = d.id
							      INNER JOIN PROCESS p ON u.ID_PROCESS = p.id
					  		WHERE u.id = IDENT_CURRENT('USERS') AND u.REMOVED = 0"
                , (u, d, p) =>
                {
                    u.Department = d;
                    u.Process = p;
                    return u;
                }, null, splitOn: "id, id, id").AsList();
                return query[0];
            }
        }
    }
}
