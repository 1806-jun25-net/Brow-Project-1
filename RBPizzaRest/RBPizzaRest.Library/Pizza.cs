using System;
using System.Collections.Generic;

namespace RBPizzaRest.Library
{
    public partial class Pizza
    {
        public int Id { get; set; }
        public string Size { get; set; }
        public string Topping { get; set; }
        public bool GarlicCrust { get; set; }
        public int OrderId { get; set; }
    }
}
