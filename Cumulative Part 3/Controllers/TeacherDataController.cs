using MySql.Data.MySqlClient;
using Cumulative_Part_3.Models;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Web.Http;

namespace Cumulative_Part_3.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the teachers table of our school database. Non-Deterministic.
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <returns>
        /// A list of Teachers Objects with fields mapped to the database column values (first name, last name, employee number, hire date & salary).
        /// </returns>
        /// <example>GET api/TeacherData/ListTeachers -> {Teacher Object, Teacher Object, Teacher Object...}</example>

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]

        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {
            //Create a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "SELECT * FROM Teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key);";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int Id = Convert.ToInt32(ResultSet["teacherid"]);
                string Name = ResultSet["teacherfname"].ToString();
                string LastName = ResultSet["teacherlname"].ToString();
                string EmpNum = ResultSet["employeenumber"].ToString();
                DateTime HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                Decimal Salary = Convert.ToDecimal(ResultSet["salary"]);

                Teacher NewTeacher = new Teacher();
                NewTeacher.Id = Id;
                NewTeacher.Name = Name;
                NewTeacher.LastName = LastName;
                NewTeacher.EmpNum = EmpNum;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                //Add the Teacher Name to the List
                Teachers.Add(NewTeacher);

            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teacher names
            return Teachers;
        }


        /// <summary>
        /// Finds an teacher from the MySQL Database through an id. Non-Deterministic.
        /// </summary>
        /// <param name="id">The teacher ID</param>
        /// <returns>teacher object containing information about the teacher with a matching ID. Empty teacher Object if the ID does not match any teachers in the system.</returns>
        /// <example>api/TeacherData/FindTeacher/6 -> {Teacher Object}</example>
        /// <example>api/TeacherData/FindTeacher/10 -> {Teacher Object}</example>

        [HttpGet]
        public Teacher Findteacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create a connection

            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database

            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int Id = Convert.ToInt32(ResultSet["teacherid"]);
                string Name = ResultSet["teacherfname"].ToString();
                string LastName = ResultSet["teacherlname"].ToString();
                string EmpNum = ResultSet["employeenumber"].ToString();
                DateTime HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                Decimal Salary = Convert.ToDecimal(ResultSet["salary"]);

                NewTeacher.Id = Id;
                NewTeacher.Name = Name;
                NewTeacher.LastName = LastName;
                NewTeacher.EmpNum = EmpNum;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
            }
            Conn.Close();

            return NewTeacher;
        }


        /// <summary>
        /// Deletes an Teacher from the connected MySQL Database if the ID of that Teacher exists. Does NOT maintain relational integrity. Non-Deterministic.
        /// </summary>
        /// <param name="id">The ID of the teacher.</param>
        /// <example>POST /api/TeacherData/DeleteTeacher/3</example>

        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from Teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }



        /// <summary>
        /// Adds an Teacher to the MySQL Database. Non-Deterministic.
        /// </summary>
        /// <param name="NewTeacher">An object with fields that map to the columns of the teacher's table. </param>
        /// <example>
        /// POST api/teacherData/AddTeacher 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"Name":Saloni"",
        ///	"lastName":"Pawar",
        ///	"EmpNum":"T677",
        ///	"Hiredate":"27/11/2023"
        ///	"Salary":"50.60"
        /// }
        /// </example>

        [HttpPost]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            //Create a connection
            MySqlConnection Conn = School.AccessDatabase();

            Debug.WriteLine(NewTeacher.Name);

            //Open the connection
            Conn.Open();

            //Command for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "insert into Teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) values (@Name,@LastName,@EmpNum,@HireDate, @Salary)";
            cmd.Parameters.AddWithValue("@Name", NewTeacher.Name);
            cmd.Parameters.AddWithValue("@LastName", NewTeacher.LastName);
            cmd.Parameters.AddWithValue("@EmpNum", NewTeacher.EmpNum);
            cmd.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }

        /// <summary>
        /// Updates an Teacher on the MySQL Database. Non-Deterministic.
        /// </summary>
        /// <param name="TeacherInfo">An object with fields that map to the columns of the teacher's table.</param>
        /// <example>
        /// POST api/TeacherData/UpdateTeacher/208 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"Name":Saloni"",
        ///	"lastName":"Pawar",
        ///	"EmpNum":"T677",
        ///	"Hiredate":"27/11/2023"
        ///	"Salary":"50.60"
        /// }
        /// </example>
        [HttpPost]

        public void UpdateTeacher(int Id, [FromBody] Teacher TeacherInfo)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "update Teachers set teacherfname=@Name, teacherlname=@LastName, employeenumber=@EmpNum, hiredate=@HireDate, salary=@Salary  where teacherid=@Id";
            cmd.Parameters.AddWithValue("@Name", TeacherInfo.Name);
            cmd.Parameters.AddWithValue("@LastName", TeacherInfo.LastName);
            cmd.Parameters.AddWithValue("@EmpNum", TeacherInfo.EmpNum);
            cmd.Parameters.AddWithValue("@HireDate", TeacherInfo.HireDate);
            cmd.Parameters.AddWithValue("@Salary", TeacherInfo.Salary);
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }

    }
}