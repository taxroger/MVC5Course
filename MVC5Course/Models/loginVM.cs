using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public class loginVM
    {
        [Required]
        [MinLength(3)]
        public string username { get; set; }
        [Required]
        [MinLength(6)]
        public string password { get; set; }
    }
}