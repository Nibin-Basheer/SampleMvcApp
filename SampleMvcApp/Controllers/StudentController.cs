using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleMvcApp.Repository;
using SampleMvcApp.Models;
using System.IO;

namespace SampleMvcApp.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult GetAllStudents()
        {

            StudentRepository StObj = new StudentRepository();
            ModelState.Clear();
            return View(StObj.GetAllStudents());
        }
        public ActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddStudent(StudentClass obj, HttpPostedFileBase file)
                
        {
            try
            {
                if (ModelState.IsValid)
                {
                    StudentRepository stObj=new StudentRepository();
                    //if (file.ContentLength > 0)
                    //{
                    //    string filename = Path.GetFileName(file.FileName);
                    //    var s = Server.MapPath("~/pic");
                    //    string path = Path.Combine(s, filename);
                    //    file.SaveAs(path);

                    //    var fullpath = Path.Combine("~\\pic", filename);
                    //    obj.photo = fullpath;//set

                    //}

                    if (stObj.AddStudent(obj))
                    {
                        ViewBag.Message = "Student details added...";
                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult EditStudentDetails(int id)
        {
            StudentRepository stObj = new StudentRepository();



            return View(stObj.GetAllStudents().Find(st => st.id == id));

        }
        [HttpPost]

        public ActionResult EditStudentDetails(StudentClass obj, int Id) { 
            try
            {
                StudentRepository stObj = new StudentRepository();

                stObj.UpdateStudent(obj,Id);
                return RedirectToAction("GetAllStudents");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeleteStudent(int id)
        {
            try
            {
                StudentRepository stObj = new StudentRepository();
                if (stObj.DeleteStudent(id))
                {
                    ViewBag.AlertMsg = "Student details deleted successfully";

                }
                return RedirectToAction("GetAllStudents");

            }
            catch
            {
                return View();
            }
        }
        public ActionResult StudentDetails(int id)
        {
            StudentRepository stObj = new StudentRepository();
            StudentClass student = stObj.GetStudentById(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }


    }
}
