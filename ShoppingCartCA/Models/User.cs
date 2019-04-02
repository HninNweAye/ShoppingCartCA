using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingCartCA.Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required (ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}