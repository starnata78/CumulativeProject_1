using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CumulativeProject_1.Models;
using System.Diagnostics;

namespace CumulativeProject_1.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        /// <summary>
        /// Purpose of this Controller is similar to API controller 
        /// except for linking to dynamically rendered Webpages which exist in the Views folder.
        /// Objective: to create a dynamically rendered page which accesses the information 
        /// from our TeacherData ListTeacher method.
        /// To achive the objective we create a controller which listens for HTTP requests 
        /// and links us to dynamically rendered  pages.
        /// Path: Controllers - Add - New Controller - MVC 5 Controller - Empty(it allows to access dynamically rendered pages from HTTP requests) - Add - TeacherController - Add
        /// </summary>
        //A Teacher folder will be created simultaneously in the View folder

        public ActionResult Index()
        {
            return View();
        }
        //We link TeacherController with list.cshtml page as TeacherController listens for a request 
        //and links a specific page which we want to render.
        //GET: /Teacher/List 

        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            //We connect TeacherController to the actual Web.API Controller which accesses the data
            return View(Teachers);
        }
       
        //GET: /Teacher/Show/{id}
        /// <summary>
        ///Similar to list.cshtml page, we link TeacherController to show.schtml. We set a "listener" for GET request.
        ///Upon recieving the HTTP request, the method below will retun View.
        /// </summary>
        /// <returns>Show Teacher</returns>

        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);
            

            return View(SelectedTeacher);
        }
        //GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }
        //POST :/Teacher/Delete/{id}
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }
        //GET: /Teacher/New
        public ActionResult New()
        {
            return View();
        }
        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string TeacherEnumber, DateTime TeacherHdate, decimal TeacherSalary)
        {
            //Identify that theis method is running 
            //Indentify the inputs provided from teh form

            Debug.WriteLine("I have access to Create Method!");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(TeacherEnumber);
           

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.TeacherEnumber = TeacherEnumber;
            NewTeacher.TeacherHdate = TeacherHdate;
            NewTeacher.TeacherSalary = TeacherSalary;
        

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);
            return RedirectToAction("List");
        }
        //GET : /Teacher/Update/{id}
        ///
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);
            return View(SelectedTeacher);
        }

        //POST :/Teacher/Update/{id}
        /// <summary>
        /// Recieves a POST requrest contatining information about an existing teacher in the
        /// system, with new values. Conveys this information to the API, and redirects to the "Teacher Show" 
        /// page of out updated teacher. 
        /// </summary>
        /// <param name="id">Id of the Teacher to update</param>
        /// <param name="TeacherFname">The updated first name of the teacher</param>
        /// <param name="TeacherLname">The updated last name of the teacher</param>
        /// <param name="TeacherEnumber">The updated Employee number of the teacher</param>
        /// <param name="TeacherHdate">The updated hire date of the teacher</param>
        /// <param name="TeacherSalary">The updated salary of the teacher</param>
        /// <returns>A dynamic webpate with provides the current information of the teacher</returns>
        /// <example>POST : /Teacher/Update/{id}
        /// POST : /Teacher/Update/2
        /// FROM DATA / POST DATA / REQUEST BODY
        /// {
        /// "TeacherFname":"Natalia"
        /// "TeacherLname":"Kolesnikova"
        /// "TeacherEnumber:"T3456"
        /// "TeacherHdate:"2020-12-01"
        /// "TeacherSalary":"50.50"
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string
            TeacherLname, string TeacherEnumber, DateTime TeacherHdate, 
            decimal TeacherSalary)

        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFname = TeacherFname;
            TeacherInfo.TeacherLname = TeacherLname;
            TeacherInfo.TeacherEnumber = TeacherEnumber;
            TeacherInfo.TeacherHdate = TeacherHdate;
            TeacherInfo.TeacherSalary = TeacherSalary;


            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id,TeacherInfo);
            
            return RedirectToAction("Show/"+id);
        }
       
    }
}