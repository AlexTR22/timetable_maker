using System.Data;
using Microsoft.Data.SqlClient;
using TimetableBackend.Model;

namespace TimetableBackend.Service
{
    public class SubjectService
    {
        private readonly Helper _helper;

        public SubjectService(Helper helper)
        {
            _helper = helper ?? throw new ArgumentNullException(nameof(helper));
        }

        public List<Subject> GetAllSubjects()
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("GetAllSubjects", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            con.Open();
            using var reader = cmd.ExecuteReader();
            var result = new List<Subject>();

            while (reader.Read())
            {
                var subject = new Subject
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Year = reader.GetInt32(2),
                    Semester = reader.GetBoolean(3),
                    IdProfessor = reader.GetInt32(4),
                    IdCollege = reader.GetInt32(5),
                };
                result.Add(subject);
            }

            return result;
        }

        public List<Subject> GetAllSubjectsByCollege(string collegeName)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("GetAllSubjectsByCollege", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Name", collegeName);

            con.Open();
            using var reader = cmd.ExecuteReader();
            var result = new List<Subject>();

            while (reader.Read())
            {
                var subject = new Subject
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Year = reader.GetInt32(2),
                    Semester = reader.GetBoolean(3),
                    IdProfessor = reader.GetInt32(4),
                    IdCollege = reader.GetInt32(5),
                };
                result.Add(subject);
            }

            return result;
        }

        public bool AddSubjectInDatabase(Subject subject)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("AddSubject", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            var idParam = new SqlParameter("Id", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            cmd.Parameters.Add(idParam);
            cmd.Parameters.AddWithValue("@Name", subject.Name);
            cmd.Parameters.AddWithValue("@Year", subject.Year);
            cmd.Parameters.AddWithValue("@CollegeId", subject.IdCollege);
            cmd.Parameters.AddWithValue("@ProfessorId", subject.IdProfessor);
            cmd.Parameters.AddWithValue("@Semester", subject.Semester);

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            subject.Id = (int)idParam.Value;
            return rowsAffected > 0;
        }

        public bool ModifySubjectInDatabase(Subject subject)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("ModifySubject", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Id", subject.Id);
            cmd.Parameters.AddWithValue("@Name", subject.Name);
            cmd.Parameters.AddWithValue("@Year", subject.Year);
            cmd.Parameters.AddWithValue("@CollegeId", subject.IdCollege);
            cmd.Parameters.AddWithValue("@ProfessorId", subject.IdProfessor);
            cmd.Parameters.AddWithValue("@Semester", subject.Semester);

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool DeleteSubjectInDatabase(int subjectId)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("DeleteSubject", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Id", subjectId);

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
}
