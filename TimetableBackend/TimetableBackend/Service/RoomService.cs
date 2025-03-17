using System.Data.SqlClient;
using System.Data;
using TimetableBackend.Model;

namespace TimetableBackend.Service
{
    public class RoomService
    {
        private readonly Helper _helper;
        public RoomService(Helper helper)
        {
            _helper = helper ?? throw new ArgumentNullException(nameof(helper));
        }

        public List<Room> GetAllRooms()
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllRooms", con);
                List<Room> result = new List<Room>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Room room = new Room();
                    room.Id = (int)reader[0];
                    room.Name = reader.GetString(1);
                    room.Capacity = (int)reader[2];
                    result.Add(room);
                }
                reader.Close();
                return result;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Room> GetAllRoomsByUniversity(string uniName)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllRoomsByUni", con);
                List<Room> result = new List<Room>();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter name = new SqlParameter("@name", uniName);
                cmd.Parameters.Add(name);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Room room = new Room();
                    room.Id = (int)reader[0];
                    room.Name = reader.GetString(1);
                    room.Capacity = (int)reader[2];
                    result.Add(room);
                }
                reader.Close();
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
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter id = new SqlParameter("@id", SqlDbType.Int);
                SqlParameter name = new SqlParameter("@name", room.Name);
                SqlParameter capacity = new SqlParameter("@capacity", room.Name);

                id.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(id);
                cmd.Parameters.Add(name);
                cmd.Parameters.Add(capacity);

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
                SqlParameter capacity = new SqlParameter("@capacity", room.Name);
                cmd.Parameters.Add(id);
                cmd.Parameters.Add(name);
                cmd.Parameters.Add(capacity);
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
