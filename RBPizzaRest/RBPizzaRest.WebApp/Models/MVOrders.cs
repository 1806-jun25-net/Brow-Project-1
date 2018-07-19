using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RBPizzaRest.WebApp.Models
{
    public class MVOrders
    {
        public int Id { get; set; }
        [Display(Name = "Order Number")]
        public int OrderNumber { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Customer Last Name")]
        public string CustomerLastname { get; set; }
        public string OrderLocaton { get; set; }
        [Display(Name = "Price")]
        public double PizzaPrice { get; set; }
        [Display(Name = "Total")]
        public double PizzaFprice { get; set; }
        public DateTime OrderDate { get; set; }
        public string StoreName { get; set; }
        public string CustomerPhoneNumber { get; set; }
    }
}
