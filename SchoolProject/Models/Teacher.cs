using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolProject.Models
{
    public class Teacher
    {
        // a Teacher description
        public int id;
        public string firstname;
        public string lastname;
        public string employeeNumber;
        public decimal salary;
        public DateTime hireDate;
        public List<Class> classes=new List<Class>();

        
        
    }
}