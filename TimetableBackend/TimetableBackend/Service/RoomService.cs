using System.Data;
using Microsoft.Data.SqlClient;         
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

        /* ------------------------------------------------------------------ */
        /*  READ ALL ROOMS                                                    */
        /* ------------------------------------------------------------------ */
        public List<Room> GetAllRooms()
        {
            var result = new List<Room>();

            using var con = _helper.Connection;
            using var cmd = new SqlCommand("GetAllRooms", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            con.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new Room
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Capacity = reader.GetInt32(2),
                    CollegeId = reader.GetInt32(3),

                });
            }

            return result;
        }

        /* ------------------------------------------------------------------ */
        /*  READ ROOMS BY COLLEGE                                             */
        /* ------------------------------------------------------------------ */
        public List<Room> GetAllRoomsByCollege(int collegeId)
        {
            var result = new List<Room>();

            using var con = _helper.Connection;
            using var cmd = new SqlCommand("GetRoomsByCollege", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters
               .Add("@CollegeId", SqlDbType.Int)
               .Value = collegeId;

            con.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new Room
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Capacity = reader.GetInt32(2),
                    CollegeId= reader.GetInt32(3),
                });
            }

            return result;
        }

        /* ------------------------------------------------------------------ */
        /*  CREATE                                                            */
        /* ------------------------------------------------------------------ */
        public bool AddRoomInDatabase(Room room)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("AddRoom", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            var idParam = new SqlParameter("@Id", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(idParam);

            cmd.Parameters
               .Add("@Name", SqlDbType.NVarChar, 100)
               .Value = room.Name;

            cmd.Parameters
               .Add("@Capacity", SqlDbType.Int)
               .Value = room.Capacity;
            cmd.Parameters
               .Add("@CollegeId", SqlDbType.Int)
               .Value = room.CollegeId;
            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            room.Id = (int)idParam.Value;
            return rowsAffected > 0;
        }

        /* ------------------------------------------------------------------ */
        /*  UPDATE                                                            */
        /* ------------------------------------------------------------------ */
        public bool ModifyRoomInDatabase(Room room)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("ModifyRoom", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = room.Id;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = room.Name;
            cmd.Parameters.Add("@Capacity", SqlDbType.Int).Value = room.Capacity;
            cmd.Parameters.Add("@CollegeId", SqlDbType.Int).Value = room.CollegeId;

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        /* ------------------------------------------------------------------ */
        /*  DELETE                                                            */
        /* ------------------------------------------------------------------ */
        public bool DeleteRoomInDatabase(int roomId)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("DeleteRoom", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = roomId;

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
}
