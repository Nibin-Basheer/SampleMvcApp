using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SampleMvcApp.Models
{
    public class LoginClass
    {
        [Required(ErrorMessage = "Enter username")]
        public string username { set; get; }
        [Required(ErrorMessage = "Enter Password")]
        public string password { set; get; }
        public string message { set; get; }
    }
}