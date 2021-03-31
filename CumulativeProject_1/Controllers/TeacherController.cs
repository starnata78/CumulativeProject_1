using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CumulativeProject_1.Models;

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

        public ActionResult List()
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers();
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
            Teacher NewTeacher = controller.FindTeacher(id);
            

            return View(NewTeacher);
        }
    }
}