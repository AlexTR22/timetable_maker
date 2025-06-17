using System.Data;
using Microsoft.Data.SqlClient;
using TimetableBackend.Model;

namespace TimetableBackend.Service
{
    public class ChromosomeService
    {
        private readonly Helper _helper;
        private readonly int _collegeId;
        private readonly bool _semester;
        private readonly int _year;

        public ChromosomeService(Helper helper, int collegeId, bool semester,int year)
        {
            _helper = helper ?? throw new ArgumentNullException(nameof(helper));
            _collegeId = collegeId;
            _semester = semester;
            _year = year;
        }

        public Chromosome GetSubjectClassesByUniversity()
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("GetSubjectClassesByUniversity", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@CollegeId", _collegeId);
            cmd.Parameters.AddWithValue("@Year", 1);  // momentan fix, modifici după ce dorești
            cmd.Parameters.AddWithValue("@Semester", false);

            con.Open();

            using var reader = cmd.ExecuteReader();
            var chromosome = new Chromosome();

            while (reader.Read())
            {
                var subjectClass = new SubjectClass
                {
                    Subject = new Subject
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Year = reader.GetInt32(2)
                    },
                    Professor = new Professor
                    {
                        Id = reader.GetInt32(3),
                        Name = reader.GetString(4)
                    },
                    Group = new Group
                    {
                        Id = reader.GetInt32(5),
                        Name = reader.GetString(6)
                    }
                };

                chromosome.Genes.Add(subjectClass);
            }

            return chromosome;
        }

        public List<Room> GetRoomsByCollege(int id)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("GetRoomsByCollege", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@CollegeId", id);

            con.Open();

            using var reader = cmd.ExecuteReader();
            var rooms = new List<Room>();

            while (reader.Read())
            {
                var room = new Room
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Capacity = reader.GetInt32(2)
                };

                rooms.Add(room);
            }

            return rooms;
        }

    }
}
