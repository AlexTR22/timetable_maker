using System.Data.SqlClient;
using System.Data;
using TimetableBackend.Model;
using System.Globalization;

namespace TimetableBackend.Service
{
    public class ChromosomeService
    {
        private readonly Helper _helper;
        private string _collegeName;
        private bool _semester;


        public ChromosomeService(Helper helper, string collegeName, bool semester)
        {
            _helper=helper ?? throw new ArgumentNullException(nameof(helper));
            _collegeName = collegeName;
            _semester = semester;
        }
       

        public Chromosome GetSubjectClaseesByUniversity()
        {
            SqlConnection con = _helper.Connection;
            try
            {
                Chromosome chromosome= new Chromosome();

                SqlCommand cmd = new SqlCommand("GetSubjectClassesByUniversity", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter name = new SqlParameter("@collegeName", _collegeName);
                //aici o sa trebuiasca sa iau dupa toti anii dar schimb mai tarziu, fac doar pentru un ad doar de test
                SqlParameter year = new SqlParameter("@year", 1);
                SqlParameter semester = new SqlParameter("@semester", _semester);
                cmd.Parameters.Add(name);
                cmd.Parameters.Add(year);
                cmd.Parameters.Add(semester);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SubjectClass SubjectClass = new SubjectClass();
                    SubjectClass.Subject.Id = (int)reader[0];
                    SubjectClass.Subject.Name = reader.GetString(1);
                    SubjectClass.Subject.Year= (int)reader[2];
                    SubjectClass.Professor.Id = (int)reader[3];
                    SubjectClass.Professor.Name = reader.GetString(4);
                    SubjectClass.Group.Id = (int)reader[5];
                    SubjectClass.Group.Name = reader.GetString(6);
                    chromosome.Genes.Add(SubjectClass);
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

        public List<Room> GetRoomsByCollege()
        {
            SqlConnection con = _helper.Connection;
            try
            {
                List<Room> rooms = new List<Room>();

                SqlCommand cmd = new SqlCommand("GetRoomsByUniversity", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter name = new SqlParameter("@UniversityName", _collegeName);
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
