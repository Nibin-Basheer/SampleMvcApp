using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SampleMvcApp.Models;

namespace SampleMvcApp.Repository
{
    public class StudentRepository
    {
        SqlConnection con = new SqlConnection(@"server=LENOVO\SQLEXPRESS;initial catalog=ClaysysDB;Integrated security=true");
        public bool AddStudent(StudentClass obj)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "sp_AddStudent";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", obj.id);
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
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<StudentClass> GetAllStudents()
        {
           
            List<StudentClass> StudentList = new List<StudentClass>();


            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "GetStudents";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sda.Fill(dt);
            con.Close();
               
            foreach (DataRow dr in dt.Rows)
            {

                StudentList.Add(

                    new StudentClass
                    {

                        id=Convert.ToInt32(dr["StudentId"]),
                        name = Convert.ToString(dr["Name"]),
                        age = Convert.ToInt32(dr["Age"]),
                        address = Convert.ToString(dr["Address"]),
                        email= Convert.ToString(dr["Email"]),
                        photo= Convert.ToString(dr["Photo"]),
                        username = Convert.ToString(dr["Username"]),
                        password= Convert.ToString(dr["Password"])


                    }
                    );
            }

            return StudentList;
        }
        public bool UpdateStudent(StudentClass obj,int Id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "sp_UpdateStudent";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StudentId", Id);
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
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteStudent(int Id)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "DeleteStudent";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StudentId", Id);
           

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public StudentClass GetStudentById(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "GetStudentById"; 
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", id);

            StudentClass student=null;

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                student = new StudentClass
                {
                    id = Convert.ToInt32(reader["StudentId"]),
                    name = Convert.ToString(reader["Name"]),
                    age = Convert.ToInt32(reader["Age"]),
                    address = Convert.ToString(reader["Address"]),
                    email = Convert.ToString(reader["Email"]),
                    photo = Convert.ToString(reader["Photo"]),
                    username = Convert.ToString(reader["Username"]),
                    password = Convert.ToString(reader["Password"])
                };
            }

            con.Close();

            return student;
        }


    }
}