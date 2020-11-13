using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolProject.Models;
using MySql.Data.MySqlClient;
using System.Web.Http.Cors;
using System.Web.Routing;

namespace SchoolProject.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext school= new SchoolDbContext();
        //This Controller Will access the teachers table of our schooldb database.
        /// <summary>
        /// Returns a list of teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of teachers(first names and last names)
        /// </returns>
        [HttpGet]
        public List<Teacher> ListTeachers()
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers order by teacherfname asc";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers Names
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {   
                
                //Access Column information by the DB column name as an index
                Teacher newTeacher = new Teacher();
                newTeacher.id = ResultSet.GetInt32(0);
                newTeacher.firstname = ResultSet.GetString(1);
                newTeacher.lastname = ResultSet.GetString(2);
                newTeacher.employeeNumber = ResultSet.GetString(3);
                newTeacher.hireDate = ResultSet.GetDateTime(4);
                newTeacher.salary = ResultSet.GetDecimal(5);
                
                //Add the teacher Name to the List
                Teachers.Add(newTeacher);               

            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teachers names
            return Teachers;

        }
        /// <summary>
        /// get information on a teacher in the system
        /// </summary>
        /// <example> GET: api/GetTeacherData/Show/1   display the teacher's profile with the id 1</example>
        /// <param name="id"> teacher's id</param>
        /// <returns> the teacher with the given id</returns>
        [HttpGet]
        [Route("api/TeacherData/GetTeacherById/{id}")]
        public Teacher GetTeacherById(int? id)
        {
            Teacher newTeacher= new Teacher();
            if (id != null)
            {
                //Create an instance of a connection
                MySqlConnection Conn = school.AccessDatabase();

                //Open the connection between the web server and database
                Conn.Open();

                //Establish a new command (query) for our database
                MySqlCommand cmd = Conn.CreateCommand();

                //SQL QUERY
                cmd.CommandText = "Select teachers.*, classname, classcode, startdate, finishdate from teachers left join classes on  " +
                "teachers.teacherid=classes.teacherid  where teachers.teacherid="+ id;

                //Gather Result Set of Query into a variable
                MySqlDataReader ResultSet = cmd.ExecuteReader();
                int i = 0;
                //Loop Through Each Row the Result Set
                while (ResultSet.Read())
                {
                    if (i == 0)
                    {
                        //Access Column information by the DB column name as an index                
                        newTeacher.id = ResultSet.GetInt32(0);
                        newTeacher.firstname = ResultSet.GetString(1);
                        newTeacher.lastname = ResultSet.GetString(2);
                        newTeacher.employeeNumber = ResultSet.GetString(3);
                        newTeacher.hireDate = ResultSet.GetDateTime(4);
                        newTeacher.salary = ResultSet.GetDecimal(5);
                        // add the new class to the teacher
                        Class newClass = new Class();
                        newClass.className = ResultSet["classname"].ToString();
                        newClass.classCode = ResultSet["classcode"].ToString();
                        newClass.startDate = ResultSet["startdate"].ToString();
                        newClass.finishDate = ResultSet["finishdate"].ToString();
                        newTeacher.classes.Add(newClass);
                        i += 1;
                    }
                    else
                    {
                        // the teacher has more than one class,
                        // registered the new class
                        Class newClass = new Class();
                        newClass.className = ResultSet["classname"].ToString();
                        newClass.classCode = ResultSet["classcode"].ToString();
                        newClass.startDate = ResultSet["startdate"].ToString();
                        newClass.finishDate = ResultSet["finishdate"].ToString();

                        newTeacher.classes.Add(newClass);
                    }
                }

                //Close the connection between the MySQL Database and the WebServer
                Conn.Close();
            }
            //Return the final list of teachers names
            return newTeacher;

        }
    }
}
