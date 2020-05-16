using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersAndAwards.DAL.Interfaces;
using UsersAndAwards.Entities;
using UsersAndAwards.ExtensionMethods;

namespace UsersAndAwards.DAL
{
    public class AwardDao : IAwardDao
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["UsersAndAwards"].ConnectionString;
        public void Add(Award award)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.AddAward";

                var idParameter = new SqlParameter().
                    CreateParameter<int>(DbType.Int32, ParameterDirection.Input, "@Id", award.Id);
                var titleParameter = new SqlParameter().
                    CreateParameter<string>(DbType.String, ParameterDirection.Input, "@Title", award.Title);
                command.Parameters.AddRange(new SqlParameter[] { idParameter, titleParameter });

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Award> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetAllAward";

                List<Award> awards = new List<Award>();
                connection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    awards.Add(new Award()
                    {
                        Id = (int)reader["Id"],
                        Title = reader["Title"] as string
                    });
                }
                return awards;
            }
        }

        public Award GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetAwardById";

                var idParameter = new SqlParameter().
                    CreateParameter<int>(DbType.Int32, ParameterDirection.Input, "@Id", id);
                command.Parameters.Add(idParameter);

                connection.Open();
                var reader = command.ExecuteReader();
                reader.Read();
                return new Award()
                {
                    Id = (int)reader["Id"],
                    Title = reader["Title"] as string
                };
            }
        }

        public IEnumerable<Award> GetUserAwards(int idUser)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetUserAwards";

                var idUserParameter = new SqlParameter().
                    CreateParameter<int>(DbType.Int32, ParameterDirection.Input, "@IdUser", idUser);
                command.Parameters.Add(idUserParameter);

                connection.Open();
                List<Award> awards = new List<Award>();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    awards.Add(GetById((int)reader["IdAward"]));
                }
                return awards;
            }
        }

        public void Remove(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.RemoveAward";

                var idParameter = new SqlParameter().
                    CreateParameter<int>(DbType.Int32, ParameterDirection.Input, "@Id", id);
                command.Parameters.Add(idParameter);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Reward(User user, Award award)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.Reward";

                var idUserParameter = new SqlParameter().
                    CreateParameter<int>(DbType.Int32, ParameterDirection.Input, "@IdUser", user.Id);
                var idAwardParameter = new SqlParameter().
                    CreateParameter<int>(DbType.Int32, ParameterDirection.Input, "@IdAward", award.Id);
                command.Parameters.AddRange(new SqlParameter[] { idUserParameter, idAwardParameter });

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UnReward(User user, Award award)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.UnReward";

                var idUserParameter = new SqlParameter().
                    CreateParameter<int>(DbType.Int32, ParameterDirection.Input, "@IdUser", user.Id);
                var idAwardParameter = new SqlParameter().
                    CreateParameter<int>(DbType.Int32, ParameterDirection.Input, "@IdAward", award.Id);
                command.Parameters.AddRange(new SqlParameter[] { idUserParameter, idAwardParameter });

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
