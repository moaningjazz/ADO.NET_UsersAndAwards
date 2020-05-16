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
    public class UserDao : IUserDao
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["UsersAndAwards"].ConnectionString;

        public void Add(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.AddUser";

                var idParameter = new SqlParameter().
                    CreateParameter<int>(DbType.Int32, ParameterDirection.Output, "@Id", user.Id); ;

                var nameParameter = new SqlParameter().
                    CreateParameter<string>(DbType.String, ParameterDirection.Input, "@Name", user.Name);

                var dateOfBirh = new SqlParameter().
                    CreateParameter<DateTime>(DbType.Date, ParameterDirection.Input, "@DateOfBirth", user.DateOfBirth);

                var age = new SqlParameter().
                    CreateParameter<int>(DbType.Int32, ParameterDirection.Input, "@Age", user.Age);

                command.Parameters.AddRange(new SqlParameter[] { idParameter, nameParameter, dateOfBirh, age});
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetAllUser";

                connection.Open();
                List<User> users = new List<User>();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new User()
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"] as string,
                        DateOfBirth = (DateTime)reader["DateOfBirth"]
                    });
                }
                return users;
            }
        }

        public User GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetUserById";

                var idParameter = new SqlParameter().
                    CreateParameter<int>(DbType.Int32, ParameterDirection.Input, "@Id", id);
                command.Parameters.Add(idParameter);

                connection.Open();
                var reader = command.ExecuteReader();
                reader.Read();
                return new User()
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"] as string,
                    DateOfBirth = (DateTime)reader["DateOfBirth"]
                };
            }
        }

        public void RemoveById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.RemoveUserById";

                var idParameter = new SqlParameter().
                    CreateParameter<int>(DbType.Int32, ParameterDirection.Input, "@Id", id);
                command.Parameters.Add(idParameter);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}