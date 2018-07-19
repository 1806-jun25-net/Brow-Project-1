using System;
using System.Collections.Generic;
using System.Text;

namespace RBPizzaRest.DataAccess
{
    public class ViewModelOrderPizzas
    {
        public IEnumerable<Orders> ODS { get; set; }
        public IEnumerable<Pizza> ODP { get; set; }
    }
}
