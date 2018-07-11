using System;
using System.Collections.Generic;
using System.Text;

namespace RBPizzaRest.Library
{
    public class Store
    {
        public struct Inventory
        {
            public int Douhg { get; set; }
            public int souce { get; set; }
            public int cheese { get; set; }
            public int pepperoni { get; set; }
            public int chicken { get; set; }
            public int sousage { get; set; }
            public int bacon { get; set; }
            public int veggies { get; set; }
        }

        public string Name { get; set; }

        public string location { get; set; }

        //add order instance

    }
}
