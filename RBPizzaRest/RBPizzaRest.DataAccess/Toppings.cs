using System;
using System.Collections.Generic;

namespace RBPizzaRest.DataAccess
{
    public partial class Toppings
    {
        public int Id { get; set; }
        public string Topping { get; set; }
        public int Quantity { get; set; }
        public int StoreId { get; set; }

        public Stores Store { get; set; }
    }
}
