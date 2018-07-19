using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RBPizzaRest.WebApp.Models
{
    public class MVCustomer
    {
        public int Id { get; set; }
        [Required]
        [Display (Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display (Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
    }
}
