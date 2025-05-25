using Microsoft.Data.SqlClient;
using System.Data;
using TimetableBackend.Model;

namespace TimetableBackend.Service
{
    public class CollegeService
    {
        private readonly Helper _helper;

        public CollegeService(Helper helper)
        {
            _helper = helper ?? throw new ArgumentNullException(nameof(helper));
        }

        /* ------------------------------------------------------------------ */
        /*  READ ALL CollegeS                                                   */
        /* ------------------------------------------------------------------ */
        public List<College> GetAllColleges()
        {
            var result = new List<College>();

            // using-urile asigură eliberarea resurselor fără a mai apela Close()
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("GetAllColleges", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            con.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new College
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    UniverisyId = reader.GetInt32(2),
                });
            }

            return result;
        }

        /* ------------------------------------------------------------------ */
        /*  READ CollegeS BY COLLEGE                                            */
        /* ------------------------------------------------------------------ */
        public List<College> GetAllCollegesByCollege(string collegeName)
        {
            var result = new List<College>();

            using var con = _helper.Connection;
            using var cmd = new SqlCommand("GetAllCollegesByCollege", con)
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
                result.Add(new College
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    UniverisyId = reader.GetInt32(2),
                });
            }

            return result;
        }

        /* ------------------------------------------------------------------ */
        /*  CREATE                                                            */
        /* ------------------------------------------------------------------ */
        public bool AddCollegeInDatabase(College College)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("AddCollege", con)
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
               .Value = College.Name;
            cmd.Parameters
               .Add("@UniversityId", SqlDbType.Int)
               .Value = College.UniverisyId;

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();

            College.Id = (int)idParam.Value;
            return rowsAffected > 0;
        }

        /* ------------------------------------------------------------------ */
        /*  UPDATE                                                            */
        /* ------------------------------------------------------------------ */
        public bool ModifyCollegeInDatabase(College College)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("ModifyCollege", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = College.Id;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = College.Name;
            cmd.Parameters
               .Add("@UniversityId", SqlDbType.Int)
               .Value = College.UniverisyId;
            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        /* ------------------------------------------------------------------ */
        /*  DELETE                                                            */
        /* ------------------------------------------------------------------ */
        public bool DeleteCollegeInDatabase(int CollegeId)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("DeleteCollege", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = CollegeId;

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
}
