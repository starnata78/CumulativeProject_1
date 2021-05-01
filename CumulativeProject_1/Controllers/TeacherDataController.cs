using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CumulativeProject_1.Models;
using MySql.Data.MySqlClient;

namespace CumulativeProject_1.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext school = new SchoolDbContext();
        //This controller provides access to Teachers table of the school database.
        ///<summary> It returns a list of teachers in the system</summary>
        ///<example> Get: api/TeacherData/listTeachers</example>
        ///<returns>The controller returns the list of teachers in the system</returns>
        ///The purpose of an api Controller is to listen for HTTP requests and provide the information form MySQL database.
    
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            //We need to connect to datadase
            MySqlConnection Conn = school.AccessDatabase();

            //This method opens connection between the web server and the database
            Conn.Open();

            //The following method established a new command (query) for the school database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL query
            cmd.CommandText = "Select * from Teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";
            cmd.Parameters.AddWithValue("@key","%" + SearchKey + "%");
            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers
            List<Teacher> Teachers = new List<Teacher>{};

            //Loop through an empty list of teacher names
            while (ResultSet.Read())
            {
                //Access column data by the databse column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string TeacherEnumber = (string)ResultSet["employeenumber"];
                DateTime TeacherHdate = (DateTime)ResultSet["hiredate"];
                decimal TeacherSalary = (decimal)ResultSet["salary"];

                Teacher NewTeacher = new Teacher();
                //Setting the properties of a new Teacher object as an extentsiation of the Teacher class 
                //to match the properties we have retrieved from the database.
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.TeacherEnumber = TeacherEnumber;
                NewTeacher.TeacherHdate = TeacherHdate;
                NewTeacher.TeacherSalary = TeacherSalary;

                //Add a teacher name to the list
                Teachers.Add(NewTeacher);
            }
            //This method closes the connection between the MySQL Database and the Webserver
            Conn.Close();

            //Return the final list of teacher names
            return Teachers;
        }

        /// <summary>
        /// Obective: to create a method which allows to extract teachers by a unique identifier (id).
        /// To achieve it, we create a new FindTeacher method with a paramenter (int id).
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a teacher object</returns>
        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{id}")]

        public Teacher FindTeacher(int id)
        {
            /*To returt an object (NewTeacher) we must navigate to the database, select a teacher
              by its id, render this information from the resultset into our object rendered properties
              and than finally return this information.*/

            //Setting a new extentsiation of the Teacher class
            Teacher NewTeacher = new Teacher();
            
            //We need to connect to datadase
            MySqlConnection Conn = school.AccessDatabase();

            //This method opens connection between the web server and the database
            Conn.Open();

            //The following method establishes a new command (query) for the school database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL query
            cmd.CommandText = "Select * FROM Teachers WHERE teacherid = "+id;
            //we change the query to reflect our API method

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access column data by the databse column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string TeacherEnumber = (string)ResultSet["employeenumber"];
                DateTime TeacherHdate = (DateTime)ResultSet["hiredate"];
                decimal TeacherSalary = (decimal)ResultSet["salary"];

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.TeacherEnumber = TeacherEnumber;
                NewTeacher.TeacherHdate = TeacherHdate;
                NewTeacher.TeacherSalary = TeacherSalary;
            }

            return NewTeacher;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <example>POST: /api/TeacherData/DeleteTeahcer/3</example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from Teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();            
        }
        [HttpPost]
        public void AddTeacher([FromBody]Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) values (@TeacherFname,@TeacherLname,@TeacherEnumber,@TeacherHdate,@TeacherSalary)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@TeacherEnumber", NewTeacher.TeacherEnumber);
            cmd.Parameters.AddWithValue("@TeacherHdate", NewTeacher.TeacherHdate);
            cmd.Parameters.AddWithValue("@TeacherSalary", NewTeacher.TeacherSalary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }
        [HttpPost]
        public void UpdateTeacher(int id, [FromBody]Teacher TeacherInfo)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "update teachers set teacherfname=@TeacherFname, teacherlname=@TeacherLname, employeenumber=@TeacherEnumber, hiredate=@TeacherHdate, salary=@TeacherSalary where teacherid=@TeacherId";
            cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLname);
            cmd.Parameters.AddWithValue("@TeacherEnumber", TeacherInfo.TeacherEnumber);
            cmd.Parameters.AddWithValue("@TeacherHdate", TeacherInfo.TeacherHdate);
            cmd.Parameters.AddWithValue("@TeacherSalary", TeacherInfo.TeacherSalary);
            cmd.Parameters.AddWithValue("@TeacherId", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();



        }
    }
}
