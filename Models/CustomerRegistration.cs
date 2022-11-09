using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace MyCartMVC.Models
{
    public partial class CustomerRegistration
    {
        [DisplayName("Customer Id")]
        public int CustomerId { get; set; }

        
        [DisplayName("Customer Name")]
        [Required(ErrorMessage ="Please enter your name")]
        [RegularExpression(@"^[a-zA-Z]+$",ErrorMessage ="Enter only Alphabets with no space")]
        public string CustomerName { get; set; } = null!;

        [DisplayName("Customer Age")]
        public int? CustomerAge { get; set; }
        [Required(ErrorMessage ="Please enter your address")]
        [DisplayName("Address")]
        public string Address { get; set; } = null!;
        [Required(ErrorMessage = "Please enter your password")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,15}$",ErrorMessage ="Must have atleast 8-15 characters, one Lower case,upper case,one numeric digit,one special character")]
        [DisplayName("Password")]
        public string Password { get; set; } = null!;
    }
}
