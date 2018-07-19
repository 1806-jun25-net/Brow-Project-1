using Microsoft.AspNetCore.Mvc.Rendering;
using RBPizzaRest.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBPizzaRest.WebApp.Models
{
    public class DetailModel
    {
        
        
            public IEnumerable<MVCustomer> User { get; set; }
            public IEnumerable<MVOrders> Orders { get; set; }
            public IEnumerable<MVPizza> Pizza { get; set; }

            public int Counter { get; set; } = 1;
            public SelectListItem SelectTopping { get; set; }
            public SelectListItem SelectSize { get; set; }
        
    }
}
