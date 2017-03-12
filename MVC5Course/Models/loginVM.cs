using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public class loginVM:IValidatableObject
    {
        [Required]
        [MinLength(3)]
        public string username { get; set; }
        [Required]
        [MinLength(6)]
        public string password { get; set; }

        public Boolean checkLogin()
        {
            //FabricsEntities db = new FabricsEntities();

            //var loginDb = db.Client.AsQueryable();
            //loginDb = loginDb.Where(p => p.FirstName == this.username);

            if (username == "123456" && password == "88888888")
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!this.checkLogin())
            {
                yield return new ValidationResult("登入失敗", new string[] { "Username" });
                yield break;
            }

            yield return ValidationResult.Success;
        }
    }
}