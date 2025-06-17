using System.Data;
using Microsoft.Data.SqlClient;          // ↩️  Folosește noul provider recomandat
using TimetableBackend.Model;

namespace TimetableBackend.Service
{
    public class GroupService
    {
        private readonly Helper _helper;

        public GroupService(Helper helper)
        {
            _helper = helper ?? throw new ArgumentNullException(nameof(helper));
        }

        /* ------------------------------------------------------------------ */
        /*  READ ALL GROUPS                                                   */
        /* ------------------------------------------------------------------ */
        public List<Group> GetAllGroups()
        {
            var result = new List<Group>();

            // using-urile asigură eliberarea resurselor fără a mai apela Close()
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("GetAllGroups", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            con.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new Group
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Year= reader.GetInt32(2),
                    CollegeId = reader.GetInt32(3),
                });
            }

            return result;
        }

        /* ------------------------------------------------------------------ */
        /*  READ GROUPS BY COLLEGE                                            */
        /* ------------------------------------------------------------------ */
        public List<Group> GetAllGroupsByCollege(int collegeId)
        {
            var result = new List<Group>();

            using var con = _helper.Connection;
            using var cmd = new SqlCommand("GetAllGroupsByCollege", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Evităm AddWithValue—specificăm explicit tipul și dimensiunea
            cmd.Parameters
               .Add("@CollegeId", SqlDbType.Int)
               .Value = collegeId;

            con.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new Group
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Year = reader.GetInt32(2),
                    CollegeId = reader.GetInt32(3),
                });
            }

            return result;
        }

        /* ------------------------------------------------------------------ */
        /*  CREATE                                                            */
        /* ------------------------------------------------------------------ */
        public bool AddGroupInDatabase(Group group)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("AddGroup", con)
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
               .Value = group.Name;
            cmd.Parameters
               .Add("@Year", SqlDbType.Int)
               .Value = group.Year;
            cmd.Parameters
               .Add("@CollegeId", SqlDbType.Int)
               .Value = group.CollegeId;

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();

            group.Id = (int)idParam.Value;
            return rowsAffected > 0;
        }

        /* ------------------------------------------------------------------ */
        /*  UPDATE                                                            */
        /* ------------------------------------------------------------------ */
        public bool ModifyGroupInDatabase(Group group)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("ModifyGroup", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = group.Id;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = group.Name;
            cmd.Parameters
               .Add("@Year", SqlDbType.Int)
               .Value = group.Year;
            cmd.Parameters
               .Add("@CollegeId", SqlDbType.Int)
               .Value = group.CollegeId;
            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        /* ------------------------------------------------------------------ */
        /*  DELETE                                                            */
        /* ------------------------------------------------------------------ */
        public bool DeleteGroupInDatabase(int groupId)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("DeleteGroup", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = groupId;

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
}
