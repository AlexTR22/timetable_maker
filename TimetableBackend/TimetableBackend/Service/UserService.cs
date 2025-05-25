using System.Data;
using Microsoft.Data.SqlClient;
using TimetableBackend.Model;

namespace TimetableBackend.Service
{
    public class UserService
    {
        private readonly Helper _helper;

        public UserService(Helper helper)
        {
            _helper = helper ?? throw new ArgumentNullException(nameof(helper));
        }

        public List<User> GetAllUsers()
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("GetAllUsers", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            con.Open();
            using var reader = cmd.ExecuteReader();
            var result = new List<User>();

            while (reader.Read())
            {
                var user = new User
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Password = reader.GetString(3),
                    Role = reader.GetString(4)
                };
                result.Add(user);
            }

            return result;
        }

        public List<User> GetAllUsersByUniversity(string uniName)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("GetAllUsersByUni", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@name", uniName);

            con.Open();
            using var reader = cmd.ExecuteReader();
            var result = new List<User>();

            while (reader.Read())
            {
                var user = new User
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Password = reader.GetString(3),
                    Role = reader.GetString(4)
                };
                result.Add(user);
            }

            return result;
        }

        public bool AddUserInDatabase(User user)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("AddUser", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            var idParam = new SqlParameter("@id", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            cmd.Parameters.Add(idParam);
            cmd.Parameters.AddWithValue("@name", user.Name);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@role", user.Role);

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();

            user.Id = (int)idParam.Value;
            return rowsAffected > 0;
        }

        public bool ModifyUserInDatabase(User user)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("ModifyUser", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@id", user.Id);
            cmd.Parameters.AddWithValue("@name", user.Name);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@role", user.Role);

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool DeleteUserInDatabase(int userId)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("DeleteUser", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@id", userId);

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
}
