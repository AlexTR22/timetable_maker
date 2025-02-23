using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using TimetableBackend.Model;

namespace TimetableBackend.Service
{
    public class ProfessorServicecs
    {
        private readonly Helper _helper;



        public List<Professor> GetAllProfessors()
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllProfessor", con);
                List<Professor> result = new List<Professor>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Professor professor = new Professor();
                    professor.Id = (int)reader[0];
                    professor.Name= reader.GetString(1);
                    result.Add(professor);
                }
                reader.Close();
                //ProfessorList = result;
                return result;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Professor> GetAllProfessorsByUniversity(string uniName)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllProfessorsByUni", con);
                List<Professor> result = new List<Professor>();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter name = new SqlParameter("@name", uniName);
                cmd.Parameters.Add(name);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Professor professor = new Professor();
                    professor.Id = (int)reader[0];
                    professor.Name = reader.GetString(1);
                    result.Add(professor);
                }
                reader.Close();
                //ProfessorList = result;
                return result;
            }
            finally
            {
                con.Close();
            }
        }

        public void AddProfessorInDatabase(Professor professor)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("AddProfessor", con);
                List<Professor> result = new List<Professor>();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter id = new SqlParameter("@id", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                SqlParameter name = new SqlParameter("@name", professor.Name);
                cmd.Parameters.Add(id);
                cmd.Parameters.Add(name);
                con.Open();
                cmd.ExecuteNonQuery();
                professor.Id = (int)id.Value;
            }
            finally
            {
                con.Close();
            }
        }

        public void ModifyProfessorInDatabase(Professor professor)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("ModifyProfessor", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter id = new SqlParameter("@id", professor.Id);
                SqlParameter name = new SqlParameter("@name", professor.Name);
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

        public void DeleteProfessorInDatabase(int professorId)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteProfessor", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter id= new SqlParameter("@id", professorId);
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
