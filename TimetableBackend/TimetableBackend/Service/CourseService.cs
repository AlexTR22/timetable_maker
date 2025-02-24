using System.Data.SqlClient;
using System.Data;
using TimetableBackend.Model;

namespace TimetableBackend.Service
{
    public class CourseService
    {
        private readonly Helper _helper;


        public List<Course> GetAllCourses()
        {
            SqlConnection con = _helper.Connection;
            try
            {
                List<Course> result = new List<Course>();
                SqlCommand cmd = new SqlCommand("GetAllCourses", con);

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Course course = new Course();
                    course.Id = (int)reader[0];
                    course.Name = reader.GetString(1);
                    result.Add(course);
                }
                reader.Close();
                return result;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Course> GetAllCoursesByUniversity(string uniName)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                List<Course> result = new List<Course>();
                SqlCommand cmd = new SqlCommand("GetAllCoursesByUni", con);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter name = new SqlParameter("@name", uniName);

                cmd.Parameters.Add(name);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Course course = new Course();
                    course.Id = (int)reader[0];
                    course.Name = reader.GetString(1);
                    result.Add(course);
                }
                reader.Close();
                return result;
            }
            finally
            {
                con.Close();
            }
        }

        public void AddCourseInDatabase(Course course)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("AddCourse", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter id = new SqlParameter("@id", SqlDbType.Int);
                SqlParameter name = new SqlParameter("@name", course.Name);
                id.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(id);
                cmd.Parameters.Add(name);
                con.Open();
                cmd.ExecuteNonQuery();
                course.Id = (int)id.Value;
            }
            finally
            {
                con.Close();
            }
        }

        public void ModifyCourseInDatabase(Course course)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("ModifyCourse", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter id = new SqlParameter("@id", course.Id);
                SqlParameter name = new SqlParameter("@name", course.Name);

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

        public void DeleteCourseInDatabase(int courseId)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteCourse", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter id = new SqlParameter("@id", courseId);
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
