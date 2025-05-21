using System.Data.SqlClient;
using System.Data;
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
            SqlConnection con = _helper.Connection;
            try
            {
                List<User> result = new List<User>();
                SqlCommand cmd = new SqlCommand("GetAllUsers", con);

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    User User = new User();
                    User.Id = (int)reader[0];
                    User.Name = reader.GetString(1);
                    User.Email = reader.GetString(2);
                    User.Password= reader.GetString(3);
                    User.Role = reader.GetString(4);
                    result.Add(User);
                }
                reader.Close();
                return result;
            }
            finally
            {
                con.Close();
            }
        }

        public List<User> GetAllGorupsByUniversity(string uniName)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                List<User> result = new List<User>();
                SqlCommand cmd = new SqlCommand("GetAllUsersByUni", con);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter name = new SqlParameter("@name", uniName);

                cmd.Parameters.Add(name);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    User User = new User();
                    User.Id = (int)reader[0];
                    User.Name = reader.GetString(1);
                    User.Email = reader.GetString(2);
                    User.Password = reader.GetString(3);
                    User.Role = reader.GetString(4);
                    result.Add(User);
                }
                reader.Close();
                return result;
            }
            finally
            {
                con.Close();
            }
        }

        public void AddGorupInDatabase(User User)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("AddUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter id = new SqlParameter("@id", SqlDbType.Int);
                SqlParameter name = new SqlParameter("@name", User.Name);
                SqlParameter email = new SqlParameter("@email", User.Email);
                SqlParameter password = new SqlParameter("@password", User.Password);
                SqlParameter role = new SqlParameter("@role", User.Role);
                id.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(id);
                cmd.Parameters.Add(name);
                cmd.Parameters.Add(email);
                cmd.Parameters.Add(password);
                cmd.Parameters.Add(role);

                con.Open();
                cmd.ExecuteNonQuery();
                User.Id = (int)id.Value;
            }
            finally
            {
                con.Close();
            }
        }

        public void ModifyUserInDatabase(User User)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("ModifyUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter id = new SqlParameter("@id", User.Id);
                SqlParameter name = new SqlParameter("@name", User.Name);
                SqlParameter email = new SqlParameter("@email", User.Email);
                SqlParameter password = new SqlParameter("@password", User.Password);
                SqlParameter role = new SqlParameter("@role", User.Role);
                

                cmd.Parameters.Add(id);
                cmd.Parameters.Add(name);
                cmd.Parameters.Add(email);
                cmd.Parameters.Add(password);
                cmd.Parameters.Add(role);

                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void DeleteUserInDatabase(int UserId)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter id = new SqlParameter("@id", UserId);
                cmd.Parameters.Add(id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }
    }
}
