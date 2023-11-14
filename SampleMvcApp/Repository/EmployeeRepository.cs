using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SampleMvcApp.Models;
using SampleMvcApp.Controllers;

namespace SampleMvcApp.Repository
{
    public class EmployeeRepository
    {
        SqlConnection con = new SqlConnection(@"server=LENOVO\SQLEXPRESS;initial catalog=ClaysysDB;Integrated security=true");

        public bool AddEmployee(EmployeeReg obj)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "sp_EmpInsert";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@name", obj.name);
            cmd.Parameters.AddWithValue("@age", obj.age);
            cmd.Parameters.AddWithValue("@address", obj.address);
            cmd.Parameters.AddWithValue("@email", obj.email);
            cmd.Parameters.AddWithValue("@photo", obj.photo);
            cmd.Parameters.AddWithValue("@username", obj.username);
            cmd.Parameters.AddWithValue("@password", obj.password);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if(i>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool LoginEmployee(LoginClass obj)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "sp_Login";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@username", obj.username);
            cmd.Parameters.AddWithValue("@password", obj.password);


            SqlParameter sp = new SqlParameter();
            sp.DbType = DbType.Int32;
            sp.ParameterName = "@status";
            sp.Direction = ParameterDirection.Output;
           
            
            cmd.Parameters.Add(sp);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            int output = Convert.ToInt32(sp.Value);
            if(output==1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool EmployeeProfile(LoginClass obj,ProfileClass ob)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "sp_profile";

            cmd.CommandType = CommandType.StoredProcedure;

           

            cmd.Parameters.Add("@username");

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                ob.name = reader["Name"].ToString();
                ob.age = (int)reader["Age"];
                ob.address = reader["Address"].ToString();
                ob.photo = reader["Photo"].ToString();
                ob.email = reader["Email"].ToString();

                reader.Close();
                con.Close();

                return true;
            }
            else
            {
                reader.Close();
                con.Close();

                return false;
            }







        }
    }
}