using System.Data.SqlClient;
using System.Data;
using TimetableBackend.Model;

namespace TimetableBackend.Service
{
    public class TimeConstraintService
    {
        private readonly Helper _helper;

        public TimeConstraintService(Helper helper)
        {
            helper = helper ?? throw new ArgumentNullException(nameof(helper));
        }

        public List<TimeConstraint> GetAllTimes()
        {
            SqlConnection con = _helper.Connection;
            try
            {
                List<TimeConstraint> result = new List<TimeConstraint>();
                SqlCommand cmd = new SqlCommand("GetAllTimeConstraints", con);

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TimeConstraint TimeConstraint = new TimeConstraint();
                    TimeConstraint.Id = (int)reader[0];
                    Professor professor = new Professor();
                    professor.Id = (int) reader[1];
                    professor.Name = reader.GetString(2);
                    TimeConstraint.Professor= new Professor(professor);
                    TimeConstraint.FromHour = (int)reader[3];
                    TimeConstraint.ToHour= (int)reader[4];
                    TimeConstraint.Day= (int)reader[5];
                    result.Add(TimeConstraint);
                }
                reader.Close();
                return result;
            }
            finally
            {
                con.Close();
            }
        }

        public List<TimeConstraint> GetAllTimeConstraintsByCollege(string collegeName)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                List<TimeConstraint> result = new List<TimeConstraint>();
                SqlCommand cmd = new SqlCommand("GetAllTimeConstraintsByCollege", con);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter name = new SqlParameter("@UniversityName", collegeName);

                cmd.Parameters.Add(name);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TimeConstraint TimeConstraint = new TimeConstraint();
                    TimeConstraint.Id = (int)reader[0];
                    Professor professor = new Professor();
                    professor.Id = (int)reader[1];
                    professor.Name = reader.GetString(2);
                    TimeConstraint.Professor = new Professor(professor);
                    TimeConstraint.FromHour = (int)reader[3];
                    TimeConstraint.ToHour = (int)reader[4];
                    TimeConstraint.Day = (int)reader[5];
                    result.Add(TimeConstraint);
                }
                reader.Close();
                return result;
            }
            finally
            {
                con.Close();
            }
        }

        public void AddTimeConstraintInDatabase(TimeConstraint TimeConstraint)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("AddTimeConstraint", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter id = new SqlParameter("@id", TimeConstraint.Id);
                SqlParameter professorId = new SqlParameter("@professorId", TimeConstraint.Professor.Id);
                SqlParameter fromHour = new SqlParameter("@fromHour", TimeConstraint.FromHour);
                SqlParameter toHour = new SqlParameter("@toHour", TimeConstraint.ToHour);
                SqlParameter day = new SqlParameter("@day", TimeConstraint.Day);
                SqlParameter university = new SqlParameter("@university", TimeConstraint.College);
                id.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(id);
                cmd.Parameters.Add(professorId);
                cmd.Parameters.Add(fromHour);
                cmd.Parameters.Add(toHour);
                cmd.Parameters.Add(day);
                cmd.Parameters.Add(university);

                con.Open();
                cmd.ExecuteNonQuery();
                TimeConstraint.Id = (int)id.Value;
            }
            finally
            {
                con.Close();
            }
        }

        public void ModifyTimeConstraintInDatabase(TimeConstraint TimeConstraint)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("ModifyTimeConstraint", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter id = new SqlParameter("@id", TimeConstraint.Id);
                SqlParameter professorId = new SqlParameter("@professorId", TimeConstraint.Professor.Id);
                SqlParameter fromHour = new SqlParameter("@fromHour", TimeConstraint.FromHour);
                SqlParameter toHour = new SqlParameter("@toHour", TimeConstraint.ToHour);
                SqlParameter day = new SqlParameter("@day", TimeConstraint.Day);
                SqlParameter university = new SqlParameter("@university", TimeConstraint.University);

                cmd.Parameters.Add(id);
                cmd.Parameters.Add(professorId);
                cmd.Parameters.Add(fromHour);
                cmd.Parameters.Add(toHour);
                cmd.Parameters.Add(day);
                cmd.Parameters.Add(university);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void DeleteTimeConstraintInDatabase(int TimeConstraintId)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteTimeConstraint", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter id = new SqlParameter("@id", TimeConstraintId);
                cmd.Parameters.Add(id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

    }
}
