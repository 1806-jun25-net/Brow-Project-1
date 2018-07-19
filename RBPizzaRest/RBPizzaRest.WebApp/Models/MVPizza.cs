using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace RBPizzaRest.WebApp.Models
{
    public class MVPizza
    {
        public int Id { get; set; }
        public string Size { get; set; }
        public string Topping { get; set; }
        public bool GarlicCrust { get; set; }
        public int OrderId { get; set; }

        public SelectListItem SelectedSize { get; set; }
        public SelectListItem SelectedTopping { get; set; }
        public SelectListItem SelectedCrust { get; set; }
    }
}
