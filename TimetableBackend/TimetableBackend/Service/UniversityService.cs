using Microsoft.Data.SqlClient;
using System.Data;
using TimetableBackend.Model;

namespace TimetableBackend.Service
{
    public class UniversityService
    {
        private readonly Helper _helper;

        public UniversityService(Helper helper)
        {
            _helper = helper ?? throw new ArgumentNullException(nameof(helper));
        }

        /* ------------------------------------------------------------------ */
        /*  READ ALL UniversityS                                                   */
        /* ------------------------------------------------------------------ */
        public List<University> GetAllUniversities()
        {
            var result = new List<University>();

            // using-urile asigură eliberarea resurselor fără a mai apela Close()
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("GetAllUniversities", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            con.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new University
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    CityName = reader.GetString(2),
                });
            }

            return result;
        }

        /* ------------------------------------------------------------------ */
        /*  READ UniversityS BY COLLEGE                                            */
        /* ------------------------------------------------------------------ */
        public List<University> GetAllUniversitiesByCollege(string collegeName)
        {
            var result = new List<University>();

            using var con = _helper.Connection;
            using var cmd = new SqlCommand("GetAllUniversitiesByCollege", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Evităm AddWithValue—specificăm explicit tipul și dimensiunea
            cmd.Parameters
               .Add("@Name", SqlDbType.NVarChar, 100)
               .Value = collegeName;

            con.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new University
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    CityName = reader.GetString(2),
                });
            }

            return result;
        }

        /* ------------------------------------------------------------------ */
        /*  CREATE                                                            */
        /* ------------------------------------------------------------------ */
        public bool AddUniversityInDatabase(University University)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("AddUniversity", con)
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
               .Value = University.Name;
            cmd.Parameters
               .Add("@CityName", SqlDbType.Int)
               .Value = University.CityName;

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();

            University.Id = (int)idParam.Value;
            return rowsAffected > 0;
        }

        /* ------------------------------------------------------------------ */
        /*  UPDATE                                                            */
        /* ------------------------------------------------------------------ */
        public bool ModifyUniversityInDatabase(University University)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("ModifyUniversity", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = University.Id;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = University.Name;
            cmd.Parameters
               .Add("@Year", SqlDbType.Int)
               .Value = University.CityName;
           
            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        /* ------------------------------------------------------------------ */
        /*  DELETE                                                            */
        /* ------------------------------------------------------------------ */
        public bool DeleteUniversityInDatabase(int UniversityId)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("DeleteUniversity", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = UniversityId;

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
}
