using System.Data.SqlClient;
using System.Data;
using TimetableBackend.Model;

namespace TimetableBackend.Service
{
    public class ChromosomeService
    {
        private readonly Helper _helper;
        private string _universityName;

        public ChromosomeService(string universityName)
        {
            _universityName=universityName;
        }


        public Chromosome GetCourseClaseesByUniversity()
        {
            SqlConnection con = _helper.Connection;
            try
            {
                Chromosome chromosome= new Chromosome();

                SqlCommand cmd = new SqlCommand("GetCourseClaseesByUniversity", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter name = new SqlParameter("@UniversityName", _universityName);
                cmd.Parameters.Add(name);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CourseClass courseClass = new CourseClass();
                    courseClass.Course.Id = (int)reader[0];
                    courseClass.Course.Name = reader.GetString(1);
                    courseClass.Course.Year= (int)reader[2];
                    courseClass.Professor.Id = (int)reader[3];
                    courseClass.Professor.Name = reader.GetString(4);
                    courseClass.Group.Id = (int)reader[5];
                    courseClass.Group.Name = reader.GetString(6);
                    chromosome.Genes.Add(courseClass);
                    //not ready yet, need more code
                    //its ready, not sure if it's correct
                }
                reader.Close();
                return chromosome;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Room> GetRoomsByUniversity()
        {
            SqlConnection con = _helper.Connection;
            try
            {
                List<Room> rooms = new List<Room>();

                SqlCommand cmd = new SqlCommand("GetRoomsByUni", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter name = new SqlParameter("@UniversityName", _universityName);
                cmd.Parameters.Add(name);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Room room = new Room();
                    room.Id = (int)reader[0];
                    room.Name = reader.GetString(1);
                    room.Capacity = (int)reader[2];
                    rooms.Add(room);
                    //not ready yet, need more code
                }
                reader.Close();
                return rooms;
            }
            finally
            {
                con.Close();
            }
        }


    }
}
