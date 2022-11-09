using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace MyCartMVC.Models
{
    public partial class AddBooking
    {
        [DisplayName("Order Id")]
        public int OrderId { get; set; }


        [DisplayName("Customer Id")]
        public int? CustomerId { get; set; }


        [Required(ErrorMessage =  "Please enter delivery date")]
        [DataType(DataType.Date)]
        [DisplayName("Delivery Date")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please enter time of pickup")]
        [DisplayName("Time Of Pickup")]
       public DateTime TimeOfPickup { get; set; }

        [Required(ErrorMessage = "Please enter the parcel weight")]
        [DisplayName("Weight")]
        [RegularExpression(@"^(?<!-)\d+$",ErrorMessage ="weight must be in positive number")]
        public string Weight { get; set; } = null!;

        [DisplayName("Price")]
        public int? Price { get; set; }
        [DisplayName("Executive Id")]
        public int? ExecutiveId { get; set; }
    }
}
