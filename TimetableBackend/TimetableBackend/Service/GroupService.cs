﻿using System.Data.SqlClient;
using System.Data;
using TimetableBackend.Model;

namespace TimetableBackend.Service
{
    public class GroupService
    {
        private readonly Helper _helper;


        public List<Group> GetAllGorups()
        {
            SqlConnection con = _helper.Connection;
            try
            {
                List<Group> result = new List<Group>();
                SqlCommand cmd = new SqlCommand("GetAllCourses", con);

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Group group = new Group();
                    group.Id = (int)reader[0];
                    group.Name = reader.GetString(1);
                    result.Add(group);
                }
                reader.Close();
                return result;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Group> GetAllGorupsByUniversity(string uniName)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                List<Group> result = new List<Group>();
                SqlCommand cmd = new SqlCommand("GetAllRoomsByUni", con);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter name = new SqlParameter("@name", uniName);

                cmd.Parameters.Add(name);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Group group = new Group();
                    group.Id = (int)reader[0];
                    group.Name = reader.GetString(1);
                    result.Add(group);
                }
                reader.Close();
                return result;
            }
            finally
            {
                con.Close();
            }
        }

        public void AddGorupInDatabase(Group group)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("AddRoom", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter id = new SqlParameter("@id", SqlDbType.Int);
                SqlParameter name = new SqlParameter("@name", group.Name);
                id.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(id);
                cmd.Parameters.Add(name);
                con.Open();
                cmd.ExecuteNonQuery();
                group.Id = (int)id.Value;
            }
            finally
            {
                con.Close();
            }
        }

        public void ModifyGroupInDatabase(Group group)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("ModifyRoom", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter id = new SqlParameter("@id", group.Id);
                SqlParameter name = new SqlParameter("@name", group.Name);

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

        public void DeleteGroupInDatabase(int groupId)
        {
            SqlConnection con = _helper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteRoom", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter id = new SqlParameter("@id", groupId);
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
