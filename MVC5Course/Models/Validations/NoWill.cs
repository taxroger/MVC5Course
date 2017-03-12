using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.Validations
{
    public class NoWill : DataTypeAttribute
    {
        public NoWill() : base(DataType.Text)
        {

        }

        public override bool IsValid(object value)
        {
            string str = Convert.ToString(value).ToUpper();
            
            if (str.Contains("WILL"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}