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
    public class ClassDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext school = new SchoolDbContext();
        /// <summary>
        /// return a teacher's classes in the system 
        /// </summary>
        /// <example> GET: api/ListTeacherClasses/2       return the classes of teacher with id 2 in he system </example>
        /// <param name="id"></param>
        /// <returns> return the list of classes of the teacher with the given id</returns>
        [HttpGet]
        [Route("api/ClassData/ListTeacherClasses/{id}")]
        public List<Class> ListTeachersClasses(int? id)
        {
            //create an empty list for the classes
            List<Class> classes = new List<Class>();

            if (id != null)
            {
                //Create an instance of a connection
                MySqlConnection Conn = school.AccessDatabase();

                //Open the connection between the web server and database
                Conn.Open();

                //Establish a new command (query) for our database
                MySqlCommand cmd = Conn.CreateCommand();

                //SQL QUERY
                cmd.CommandText = "Select classes.* from classes left join teachers on  " +
                    "teachers.teacherid=classes.teacherid  where teachers.teacherid=" + id;

                //Gather Result Set of Query into a variable
                MySqlDataReader ResultSet = cmd.ExecuteReader();

                while (ResultSet.Read())
                {
                    // add the new class to the teacher
                    Class newClass = new Class();
                    newClass.className = ResultSet["classname"].ToString();
                    newClass.classCode = ResultSet["classcode"].ToString();
                    newClass.startDate = ResultSet["startdate"].ToString();
                    newClass.finishDate = ResultSet["finishdate"].ToString();
                    classes.Add(newClass);

                }

            }
            return classes;
        }
    }
}
