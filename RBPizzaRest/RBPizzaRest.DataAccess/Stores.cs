using System;
using System.Collections.Generic;

namespace RBPizzaRest.DataAccess
{
    public partial class Stores
    {
        public Stores()
        {
            Toppings = new HashSet<Toppings>();
        }

        public int Id { get; set; }
        public string StoreName { get; set; }
        public string StoreLocation { get; set; }
        public int Dough { get; set; }
        public int Sauce { get; set; }
        public int Cheese { get; set; }

        public ICollection<Toppings> Toppings { get; set; }
    }
}
