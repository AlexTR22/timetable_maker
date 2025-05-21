using System.Data.SqlClient;
using System.Data;
using TimetableBackend.Model;

namespace TimetableBackend.Service
{
    public class SubjectService
    {
        private readonly Helper _helper;

        public SubjectService(Helper helper)
        {
            helper = helper ?? throw new ArgumentNullException(nameof(helper));
        }

        public List<Subject> GetAllSubjects()
        {
            SqlConnection con = _helper.Connection;
            try
            {
                List<Subject> result = new List<Subject>();
                SqlCommand cmd = new SqlCommand("GetAllSubjects", con);

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Subject Subject = new Subject();
                    Subject.Id = (int)reader[0];
                    Subject.Name = reader.GetString(1);
                    result.Add(Subject);
                }
                reader.Close();
                return result;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Subject> GetAllSubjectsByCollege(string collegeName)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                List<Subject> result = new List<Subject>();
                SqlCommand cmd = new SqlCommand("GetAllSubjectsByCollege", con);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter name = new SqlParameter("@name", collegeName);

                cmd.Parameters.Add(name);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Subject Subject = new Subject();
                    Subject.Id = (int)reader[0];
                    Subject.Name = reader.GetString(1);
                    result.Add(Subject);
                }
                reader.Close();
                return result;
            }
            finally
            {
                con.Close();
            }
        }

        public void AddSubjectInDatabase(Subject Subject)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("AddSubject", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter id = new SqlParameter("@id", SqlDbType.Int);
                SqlParameter name = new SqlParameter("@name", Subject.Name);
                id.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(id);
                cmd.Parameters.Add(name);
                con.Open();
                cmd.ExecuteNonQuery();
                Subject.Id = (int)id.Value;
            }
            finally
            {
                con.Close();
            }
        }

        public void ModifySubjectInDatabase(Subject Subject)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("ModifySubject", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter id = new SqlParameter("@id", Subject.Id);
                SqlParameter name = new SqlParameter("@name", Subject.Name);

                cmd.Parameters.Add(id);
                cmd.Parameters.Add(name);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void DeleteSubjectInDatabase(int SubjectId)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteSubject", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter id = new SqlParameter("@id", SubjectId);
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
