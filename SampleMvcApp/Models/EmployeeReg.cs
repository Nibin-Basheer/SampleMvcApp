using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SampleMvcApp.Models
{
    public class EmployeeReg
    {
        [Required(ErrorMessage = "Enter the name")]
        public string name { set; get; }
        [Range(20, 50, ErrorMessage = "Age between 20 and 50")]
        public int age { set; get; }
        [Required(ErrorMessage = "Enter the address")]
        public string address { set; get; }
        [EmailAddress(ErrorMessage = "Enter valid address")]
        public string email { set; get; }
        public string photo { set; get; }
        [Required(ErrorMessage = "Enter the username")]
        public string username { set; get; }
        public string password { set; get; }
        [Compare("password", ErrorMessage = "Password mismatch")] // must be same variable name given for the password
        public string confirmPassword { set; get; }
        public string message { set; get; }
    }
}