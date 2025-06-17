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
                    Role = reader.GetString(4),
                    UniversityId = reader.GetInt32(5),
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

            cmd.Parameters.AddWithValue("@Name", uniName);

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
                    Role = reader.GetString(4),
                    UniversityId = reader.GetInt32(5),
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
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Role", user.Role);
            cmd.Parameters.AddWithValue("@UniversityId", user.UniversityId);

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

            cmd.Parameters.AddWithValue("@Id", user.Id);
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Role", user.Role);
            cmd.Parameters.AddWithValue("@UniversityId", user.UniversityId);

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

            cmd.Parameters.AddWithValue("@Id", userId);

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public User ValidateUser(string username, string password, string email)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("dbo.ValidateUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.AddWithValue("@Email", email);

            con.Open();
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new User
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Password = reader.GetString(3),
                    Role = reader.GetString(4),
                    UniversityId = reader.GetInt32(5),
                };
            }
            else
            {
                return null; // User invalid
            }
        }
    }
}
