using System.Data;
using Microsoft.Data.SqlClient;
using TimetableBackend.Model;

namespace TimetableBackend.Service
{
    public class ProfessorService
    {
        private readonly Helper _helper;

        public ProfessorService(Helper helper)
        {
            _helper = helper ?? throw new ArgumentNullException(nameof(helper));
        }

        public List<Professor> GetAllProfessors()
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("GetAllProfessors", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            con.Open();
            using var reader = cmd.ExecuteReader();
            var result = new List<Professor>();

            while (reader.Read())
            {
                var professor = new Professor
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    CollegeId= reader.GetInt32(2),
                };
                result.Add(professor);
            }

            return result;
        }

        public List<Professor> GetAllProfessorsByCollege(int collegeId)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("GetAllProfessorsByCollege", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@CollegeId", collegeId);

            con.Open();
            using var reader = cmd.ExecuteReader();
            var result = new List<Professor>();

            while (reader.Read())
            {
                var professor = new Professor
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    CollegeId = reader.GetInt32(2),
                };
                result.Add(professor);
            }

            return result;
        }

        public bool AddProfessorInDatabase(Professor professor)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("AddProfessor", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            var idParam = new SqlParameter("@id", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            cmd.Parameters.Add(idParam);
            cmd.Parameters.AddWithValue("@Name", professor.Name);
            cmd.Parameters.AddWithValue("@CollegeId", professor.CollegeId);

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            professor.Id = (int)idParam.Value;
            return rowsAffected > 0;
        }

        public bool ModifyProfessorInDatabase(Professor professor)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("ModifyProfessor", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Id", professor.Id);
            cmd.Parameters.AddWithValue("@Name", professor.Name);
            cmd.Parameters.AddWithValue("@CollegeId", professor.CollegeId);

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool DeleteProfessorInDatabase(int professorId)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("DeleteProfessor", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Id", professorId);

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public int GetProfessorIdByName(string name)
        {
            using var con = _helper.Connection;

            using var cmd = new SqlCommand("GetProfessorIdByName", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            
            cmd.Parameters.AddWithValue("@Name", name);
            int id = 0;
            con.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                id = reader.GetInt32(0);
            }
            return id;
        }

        public Professor GetProfessorById(int id)
        {
            using var con = _helper.Connection;

            using var cmd = new SqlCommand("GetProfessorById", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Id", id);
            
            con.Open();
            using var reader = cmd.ExecuteReader();
            Professor result= null;
            while (reader.Read())
            {
                var professor = new Professor
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    CollegeId = reader.GetInt32(2),
                };
                result= new Professor(professor);

            }
            return result;
        }
    }
}
