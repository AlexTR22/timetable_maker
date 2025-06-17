using System.Data;
using Microsoft.Data.SqlClient;                 
using TimetableBackend.Model;

namespace TimetableBackend.Service
{
    public class TimeConstraintService
    {
        private readonly Helper _helper;

        public TimeConstraintService(Helper helper)
        {
            _helper = helper ?? throw new ArgumentNullException(nameof(helper));
        }

        /* ------------------------------------------------------------------ */
        /*  READ ALL                                                          */
        /* ------------------------------------------------------------------ */
        public List<TimeConstraint> GetAllTimes()
        {
            var result = new List<TimeConstraint>();

            using var con = _helper.Connection;
            using var cmd = new SqlCommand("GetAllTimeConstraints", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            con.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new TimeConstraint
                {
                    Id = reader.GetInt32(0),
                    ProfessorId = reader.GetInt32(1),
                    FromHour = reader.GetInt32(2),
                    ToHour = reader.GetInt32(3),
                    Day = reader.GetInt32(4),
                    CollegeId = reader.GetInt32(5)   
                });
            }

            return result;
        }

        /* ------------------------------------------------------------------ */
        /*  READ BY COLLEGE                                                   */
        /* ------------------------------------------------------------------ */
        public List<TimeConstraint> GetAllTimeConstraintsByCollege(int collegeId)
        {
            var result = new List<TimeConstraint>();

            using var con = _helper.Connection;
            using var cmd = new SqlCommand("GetAllTimeConstraintsByCollege", con)
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
                result.Add(new TimeConstraint
                {
                    Id = reader.GetInt32(0),
                    ProfessorId = reader.GetInt32(1),
                    FromHour = reader.GetInt32(2),
                    ToHour = reader.GetInt32(3),
                    Day = reader.GetInt32(4),
                    CollegeId = reader.GetInt32(5)
                });
            }

            return result;
        }

        /* ------------------------------------------------------------------ */
        /*  CREATE                                                            */
        /* ------------------------------------------------------------------ */
        public bool AddTimeConstraintInDatabase(TimeConstraint tc)
        {
            if (tc.ToHour < tc.FromHour || tc.ToHour>20 || tc.FromHour<8 || tc.Day<0 || tc.Day>4)
            {
                return false;
            }
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("AddTimeConstraint", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            
            var idParam = new SqlParameter("@Id", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(idParam);

            cmd.Parameters.Add("@ProfessorId", SqlDbType.Int).Value = tc.ProfessorId;
            cmd.Parameters.Add("@FromHour", SqlDbType.Int).Value = tc.FromHour;
            cmd.Parameters.Add("@ToHour", SqlDbType.Int).Value = tc.ToHour;
            cmd.Parameters.Add("@Day", SqlDbType.Int).Value = tc.Day;
            cmd.Parameters.Add("@CollegeId", SqlDbType.Int).Value = tc.CollegeId;

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            tc.Id = (int)idParam.Value;
            return rowsAffected > 0;
        }

        /* ------------------------------------------------------------------ */
        /*  UPDATE                                                            */
        /* ------------------------------------------------------------------ */
        public bool ModifyTimeConstraintInDatabase(TimeConstraint tc)
        {
            if (tc.ToHour < tc.FromHour && tc.ToHour > 20 && tc.FromHour < 8 && tc.Day < 0 && tc.Day > 4)
            {
                return false;
            }
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("ModifyTimeConstraint", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = tc.Id;
            cmd.Parameters.Add("@ProfessorId", SqlDbType.Int).Value = tc.ProfessorId;
            cmd.Parameters.Add("@FromHour", SqlDbType.Int).Value = tc.FromHour;
            cmd.Parameters.Add("@ToHour", SqlDbType.Int).Value = tc.ToHour;
            cmd.Parameters.Add("@Day", SqlDbType.Int).Value = tc.Day;
            cmd.Parameters.Add("@CollegeId", SqlDbType.Int).Value = tc.CollegeId;

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        /* ------------------------------------------------------------------ */
        /*  DELETE                                                            */
        /* ------------------------------------------------------------------ */
        public bool DeleteTimeConstraintInDatabase(int timeConstraintId)
        {
            using var con = _helper.Connection;
            using var cmd = new SqlCommand("DeleteTimeConstraint", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = timeConstraintId;

            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
}
