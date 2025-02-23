using System.Data.SqlClient;
using System.Data;
using TimetableBackend.Model;

namespace TimetableBackend.Service
{
    public class RoomService
    {
        private readonly Helper _helper;


        public List<Professor> GetAllRooms()
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllRooms", con);
                List<Professor> result = new List<Professor>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Professor professor = new Professor();
                    professor.Id = (int)reader[0];
                    professor.Name = reader.GetString(1);
                    result.Add(professor);
                }
                reader.Close();
                //ProfessorList = result;
                return result;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Professor> GetAllRoomsByUniversity(string uniName)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllRoomsByUni", con);
                List<Professor> result = new List<Professor>();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter name = new SqlParameter("@name", uniName);
                cmd.Parameters.Add(name);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Professor professor = new Professor();
                    professor.Id = (int)reader[0];
                    professor.Name = reader.GetString(1);
                    result.Add(professor);
                }
                reader.Close();
                //ProfessorList = result;
                return result;
            }
            finally
            {
                con.Close();
            }
        }

        public void AddRoomInDatabase(Room room)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("AddRoom", con);
                List<Professor> result = new List<Professor>();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter id = new SqlParameter("@id", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                SqlParameter name = new SqlParameter("@name", room.Name);
                cmd.Parameters.Add(id);
                cmd.Parameters.Add(name);
                con.Open();
                cmd.ExecuteNonQuery();
                room.Id = (int)id.Value;
            }
            finally
            {
                con.Close();
            }
        }

        public void ModifyRoomInDatabase(Room room)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("ModifyRoom", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter id = new SqlParameter("@id", room.Id);
                SqlParameter name = new SqlParameter("@name", room.Name);
                cmd.Parameters.Add(id);
                cmd.Parameters.Add(name);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void DeleteRoomInDatabase(int roomId)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteRoom", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter id = new SqlParameter("@id", roomId);
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
