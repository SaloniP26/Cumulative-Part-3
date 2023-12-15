using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cumulative_Part_3.Models;
using System.Diagnostics;
using System.Xml.Linq;
using Google.Protobuf.WellKnownTypes;

namespace Cumulative_Part_3.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teachers
        public ActionResult Index()
        {
            return View();
        }

        //GET : /Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.Findteacher(id);


            return View(NewTeacher);
        }


        //POST : /Teacher/Delete/{id}

        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.Findteacher(id);


            return View(NewTeacher);
        }

        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        //GET : /Author/Ajax_New
        public ActionResult Ajax_New()
        {
            return View();
        }

        //POST :/Teacher/Create
        [HttpPost]
        public ActionResult Create(string Name, string LastName, string EmpNum, DateTime HireDate, decimal Salary)
        {

            //Identify that this method is running
            //Identify the inputs provided from the form

            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(Name);
            Debug.WriteLine(LastName);
            Debug.WriteLine(EmpNum);
            Debug.WriteLine(HireDate);
            Debug.WriteLine(Salary);

            //If no errors create 
            Teacher NewTeacher = new Teacher();
            NewTeacher.Name = Name;
            NewTeacher.LastName = LastName;
            NewTeacher.EmpNum = EmpNum;
            NewTeacher.HireDate = HireDate;
            NewTeacher.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }

        /// <summary>
        /// Routes to a dynamically generated "Author Update" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the Author</param>
        /// <returns>A dynamic "Update Author" webpage which provides the current information of the author and asks the user for new information as part of a form.</returns>
        /// <example>GET : /Author/Update/5</example>
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.Findteacher(id);

            return View(SelectedTeacher);
        }

        public ActionResult Ajax_Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.Findteacher(id);

            return View(SelectedTeacher);
        }


        /// <summary>
        /// Receives a POST request containing information about an existing author in the system, with new values. Conveys this information to the API, and redirects to the "Author Show" page of our updated author.
        /// </summary>
        /// <param name="id">Id of the Author to update</param>
        /// <param name="AuthorFname">The updated first name of the author</param>
        /// <param name="AuthorLname">The updated last name of the author</param>
        /// <param name="AuthorBio">The updated bio of the author.</param>
        /// <param name="AuthorEmail">The updated email of the author.</param>
        /// <returns>A dynamic webpage which provides the current information of the author.</returns>
        /// <example>
        /// POST : /Author/Update/10
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"AuthorFname":"Christine",
        ///	"AuthorLname":"Bittle",
        ///	"AuthorBio":"Loves Coding!",
        ///	"AuthorEmail":"christine@test.ca"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string Name, string LastName, string EmpNum, DateTime HireDate, decimal Salary)
        {
            Teacher UpdateTeacher = new Teacher();
            UpdateTeacher.Name = Name;
            UpdateTeacher.LastName = LastName;
            UpdateTeacher.EmpNum = EmpNum;
            UpdateTeacher.HireDate = HireDate;
            UpdateTeacher.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, UpdateTeacher);

            return RedirectToAction("Show/" + id);
        }

    }
}