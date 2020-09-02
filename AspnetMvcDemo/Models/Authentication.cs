using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AspnetMvcDemo.Models
{
    public class Authentication
    {
    }

    public class UserLogin
    {
        [Key]
        [Required]
        [Display(Name = "User Name")  ]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


    }
}