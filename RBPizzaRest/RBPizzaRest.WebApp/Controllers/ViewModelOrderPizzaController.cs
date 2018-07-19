using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RBPizzaRest.DataAccess;

namespace RBPizzaRest.WebApp.Controllers
{
    public class ViewModelOrderPizzaController : Controller
    {
        public PizzaStoreRepository Repo;
        private readonly PizzaStoreDBContext _context;

        public ViewModelOrderPizzaController(PizzaStoreRepository repo, PizzaStoreDBContext context)
        {
            Repo = repo;
            _context = context;

        }

        // GET: ViewModelOrderPizzaq
        public ActionResult IndexVM(string sortOrder, string searchString)
        {

            ViewBag.Message = "Welcome to the Orders!";
            ViewModelOrderPizzas ODSP = new ViewModelOrderPizzas
            {
                ODS = Repo.GetOrders(),
                ODP = Repo.GetPizzas()
            };

            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            ODSP.ODS = from s in _context.Orders
                       select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                ODSP.ODS = ODSP.ODS.Where(s => s.CustomerName.ToUpper().Contains(searchString.ToUpper())
                                            || s.CustomerPhoneNumber.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Price":
                    ODSP.ODS = ODSP.ODS.OrderBy(s => s.PizzaFprice);
                    break;
                case "Price_desc":
                    ODSP.ODS = ODSP.ODS.OrderByDescending(s => s.PizzaFprice);
                    break;
                case "Date":
                    ODSP.ODS = ODSP.ODS.OrderBy(s => s.OrderDate);
                    break;
                case "Date_desc":
                    ODSP.ODS = ODSP.ODS.OrderByDescending(s => s.OrderDate);
                    break;
                default:
                    ODSP.ODS = ODSP.ODS.OrderBy(s => s.PizzaFprice);
                    break;
            }

            return View(ODSP);
        }

        //public ActionResult IndexVM(string sortOrder, string searchString)
        //{
        //    ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
        //    ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
        //    var Oder = from s in _context.Orders
        //               select s;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        Oder = Oder.Where(s => s.CustomerName.ToUpper().Contains(searchString.ToUpper())
        //                               || s.CustomerPhoneNumber.Contains(searchString));
        //    }

        //    switch (sortOrder)
        //    {
        //        case "Price":
        //            Oder = Oder.OrderBy(s => s.PizzaFprice);
        //            break;
        //        case "Price_desc":
        //            Oder = Oder.OrderByDescending(s => s.PizzaFprice);
        //            break;
        //        case "Date":
        //            Oder = Oder.OrderBy(s => s.OrderDate);
        //            break;
        //        case "Date_desc":
        //            Oder = Oder.OrderByDescending(s => s.OrderDate);
        //            break;
        //        default:
        //            Oder = Oder.OrderBy(s => s.PizzaFprice);
        //            break;
        //    }
        //    return View(Oder.ToList());
        //}


        // GET: ViewModelOrderPizza/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ViewModelOrderPizza/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ViewModelOrderPizza/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: ViewModelOrderPizza/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ViewModelOrderPizza/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: ViewModelOrderPizza/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ViewModelOrderPizza/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}