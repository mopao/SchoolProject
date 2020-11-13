using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolProject.Models;

namespace SchoolProject.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: Teacher/List
        // acquire the list of teachers and send it to List.cshtml
        [HttpGet]
        
        public ActionResult List()
        {
            //we will to gather the list of teachers 
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> teachers = controller.ListTeachers();
            return View(teachers);
        }


       
        // acquire a teacher's data  and send it to show.cshtml       
        [HttpGet]
       public ActionResult Show( int? id)
        {
            //we will to gather the list of teachers 
            TeacherDataController teacherController = new TeacherDataController();
            Teacher teacher = teacherController.GetTeacherById(id);
            // gather teacher classes
            ClassDataController classesController = new ClassDataController();
            teacher.classes = classesController.ListTeachersClasses(id);

            return View(teacher); ;
        }
    }
}