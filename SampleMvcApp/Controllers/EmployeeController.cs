using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using SampleMvcApp.Models;
using SampleMvcApp.Repository;

namespace SampleMvcApp.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeRepository empobj = new EmployeeRepository();
        // GET: Employee
        public ActionResult RegisterPageLoad()
        {
            return View();
        }
        public ActionResult RegisterClick(EmployeeReg objcls, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file.ContentLength > 0)
                {
                    string filename = Path.GetFileName(file.FileName);
                    var s = Server.MapPath("~/pic");
                    string path = Path.Combine(s, filename);
                    file.SaveAs(path);

                    var fullpath = Path.Combine("~\\pic", filename);
                    objcls.photo = fullpath;//set

                }
                empobj.AddEmployee(objcls);
                objcls.message = "Successfully inserted";
                return View("Registerpageload", objcls);

            }
            return View("RegisterPageLoad", objcls);
        }

        public ActionResult LoginPageLoad()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult LoginClick(LoginClass objcls)
        {
            if (ModelState.IsValid)
            {
                bool isValidLogin = empobj.LoginEmployee(objcls); 
                
               
                if (isValidLogin)
                {
                    Session["username"] = objcls.username;
                    return RedirectToAction("Home");
                }
                else
                {
                    ModelState.Clear();
                    objcls.message= "invalid Login";
                    return View("LoginPageLoad", objcls);
                }
            }
            return View("LoginPageLoad", objcls);
        }

        public ActionResult ProfileLoad(LoginClass ob,ProfileClass objcls)
        {
            bool getdata = empobj.EmployeeProfile(ob, objcls);
            return View("ProfileLoad", objcls);
        }
    }
}