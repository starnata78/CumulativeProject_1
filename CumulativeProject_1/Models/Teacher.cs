using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CumulativeProject_1.Models
{
    public class Teacher
    {
        /*For every teacher in the database we can extentsiate a new teacher object 
          as we'll be talking about a particular teacher. We can populate teachers' 
          properties (first name, last name, employee number, hiredate, salary) 
          with the information provided by database.
         Path: Models - Add - NewItem - Class - Teacher.cs - Add*/
        //The following fields define a teacher.

        public int TeacherId;
        public string TeacherFname;
        public string TeacherLname;
        public string TeacherEnumber;
        public DateTime TeacherHdate;
        public decimal TeacherSalary;
       
        //parameter - less constructor function
        public Teacher()
        {

        }

    }
}