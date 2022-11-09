using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace MyCartMVC.Models
{
    public partial class ExecutiveRegistration
    {
        [DisplayName("Executive Id")]
        public int ExecutiveId { get; set; }


        [Required(ErrorMessage = "Please enter your name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Enter only Alphabets with nospace")]
        [DisplayName("Executive Name")]
        public string ExecutiveName { get; set; } = null!;


       
        [DisplayName("Executive Age")]
        public int? ExecutiveAge { get; set; }

        [DisplayName("PhoneNumber")]
        [Required(ErrorMessage = "Please enter your phoneNumber")]
       // [RegularExpression(@"/^[0-9]+$/",ErrorMessage ="Must have only 10 numbers")]
        public long PhoneNumber { get; set; }


        [DisplayName("Password")]
        [Required(ErrorMessage = "Please enter your password")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,15}$", ErrorMessage = "Must have atleast 8-15 characters, one Lower case,upper case,one numeric digit,one special character")]
        public string Password { get; set; } = null!;
    }
}
