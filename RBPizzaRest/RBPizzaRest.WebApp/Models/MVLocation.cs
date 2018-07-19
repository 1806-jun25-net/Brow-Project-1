using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBPizzaRest.WebApp.Models
{
    public class MVLocation
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SelectListItem SelectedLocation { get; set; }
    }
}
