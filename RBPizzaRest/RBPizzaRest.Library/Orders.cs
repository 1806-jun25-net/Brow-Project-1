using System;
using System.Collections.Generic;

namespace RBPizzaRest.Library
{
    public partial class Orders
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLastname { get; set; }
        public string OrderLocaton { get; set; }
        public double PizzaPrice { get; set; }
        public double PizzaFprice { get; set; }
        public DateTime OrderDate { get; set; }
        public string StoreName { get; set; }
        public string CustomerPhoneNumber { get; set; }
    }
}
