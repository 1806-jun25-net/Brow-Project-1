using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RBPizzaRest.DataAccess;
using RBPizzaRest.WebApp.Models;

namespace RBPizzaRest.WebApp.Controllers
{
    public class OrdersController : Controller
    {
        public int Count = 0;
        public double total = 0;
        public double price = 0;

        private readonly PizzaStoreDBContext _context;
        private readonly PizzaStoreRepository Repo;

        public OrdersController(PizzaStoreDBContext context, PizzaStoreRepository repo)
        {
            _context = context;
            Repo = repo;
        }

        // GET: Orders
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Orders.ToListAsync());
        //}

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            int orderid = int.Parse(TempData.Peek("orderId").ToString());
            

            var orders = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == orderid);
           

            return View(orders);
        }





        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        public ActionResult MakeOrder()
        {


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeOrder(IFormCollection viewCollection, MVCustomer cust)
        {
            Random rdm = new Random();
            int ONum = rdm.Next(1, 1000000);
            int UseridTD = int.Parse(TempData.Peek("custid").ToString());
            string LocationTD = TempData.Peek("locationName").ToString();
            string NameTD = TempData.Peek("firstname").ToString();
            string LastnameTD = TempData.Peek("lastname").ToString();
            string PhoneTD = TempData.Peek("phone").ToString();

            if (Count == 12)
            {
                ViewData["msg"] = "ERROR YOU CANNOT ORDER MORE THAN 12 PIZZAS!!!!! ";
                return RedirectToAction(nameof(MakeOrder));
            }

            string selectedSize = viewCollection["SelectedSize"];
            string selectedTopping = viewCollection["SelectedTopping"];
            string selectedCrust = viewCollection["SelectedCrust"];

            if (selectedSize == "Small")
            {
                price = 5.00;

                total = int.Parse(TempData.Peek("order_total").ToString());
                total += 5.00;
                TempData["order_total"] = total;
            }
            else if (selectedSize == "Medium")
            {
                price = 10.00;
                total = int.Parse(TempData.Peek("order_total").ToString());
                total += 10.00;
                TempData["order_total"] = total;

            }
            else if (selectedSize == "Large")
            {
                price = 15.00;
                total = int.Parse(TempData.Peek("order_total").ToString());
                total += 15.00;
                TempData["order_total"] = total;

            }
            else if (selectedCrust == "1")
            {
                price = .50;
                total = int.Parse(TempData.Peek("order_total").ToString());
                total += .50;
                TempData["order_total"] = total;

            }
            else
            {
                price = 20.00;
                total = int.Parse(TempData.Peek("order_total").ToString());
                total += 20.00;
                TempData["order_total"] = total;
            }

            if (TempData.Peek("Count").ToString() == "1")
            {
                Count = 1;

            }
            else
            {
                Count = int.Parse(TempData.Peek("Count").ToString());
            }

            if (Count < 2)//only if the order is new is going to be created
            {
                //add order
                Repo.AddOrders(ONum,NameTD,LastnameTD, LocationTD,price, total, DateTime.Now,"T'Challa Slice",PhoneTD);
                Repo.SaveChanges();

            }
            bool PG = bool.Parse(selectedCrust);
            Repo.AddPizza(selectedSize, selectedTopping, PG, ONum);
            Repo.SaveChanges();
            // increment counter to know next time, that this is not  a new order.
            Count++;
            TempData["Count"] = Count;
            var orderId = Repo.GetOrders().FirstOrDefault(x => x.CustomerPhoneNumber == PhoneTD
                                                            && x.CustomerName == NameTD);
            TempData["orderId"] = orderId.Id;




            return RedirectToAction(nameof(MakeOrder));
        }



        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderNumber,CustomerName,CustomerLastname,OrderLocaton,PizzaPrice,PizzaFprice,OrderDate,StoreName,CustomerPhoneNumber")] MVOrders orders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orders);
        }

        public ActionResult Index(string sortOrder)
        {
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Price" : "Price_desc";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            var Oder = from s in _context.Orders
                       select s;
            switch (sortOrder)
            {
                case "Price":
                    Oder = Oder.OrderBy(s => s.PizzaFprice);
                    break;
                case "Price_desc":
                    Oder = Oder.OrderByDescending(s => s.PizzaFprice);
                    break;
                case "Date":
                    Oder = Oder.OrderBy(s => s.OrderDate);
                    break;
                case "Date_desc":
                    Oder = Oder.OrderByDescending(s => s.OrderDate);
                    break;
                default:
                    Oder = Oder.OrderBy(s => s.PizzaFprice);
                    break;
            }
            return View(Oder.ToList());
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderNumber,CustomerName,CustomerLastname,OrderLocaton,PizzaPrice,PizzaFprice,OrderDate,StoreName,CustomerPhoneNumber")] MVOrders orders)
        {
            if (id != orders.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        //Get: placeAnOrder
        public IActionResult PlaceAnOrder()
        {
            MVPizza pizza = new MVPizza();
            MVOrders order = new MVOrders();

            return ViewBag(pizza, order);
        }

        // Post: PlaceAnOrder
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult PlaceAnOrder(MVPizza pizza, MVOrders order, MVCustomer cust)
        //{
        //    DetailModel myModel = new DetailModel();

        //    pizza.Topping = myModel.SelectTopping.Value;
        //    pizza.Size = myModel.SelectSize.Value;
        //    pizza.OrderId = order.OrderNumber;
        //    pizza.GarlicCrust = false;

        //    Random rdm = new Random();

        //    order.OrderNumber = rdm.Next(1, 1000000);

        //    // Sets the values
        //    order.PizzaFprice = Total;


        //    if (Count <= 12)
        //    {


        //        // Calculates the total
        //        if (pizza.Size == "small")
        //        {
        //            order.PizzaPrice = 5;
        //            Total = Total += 5;
        //        }
        //        else if (pizza.Size == "medium")
        //        {
        //            order.PizzaPrice = 10;
        //            order.PizzaFprice = Total += 10;
        //        }
        //        else if (pizza.Size == "large")
        //        {
        //            order.PizzaPrice = 16;
        //            order.PizzaFprice = Total += 16;
        //        }

        //        //int price = int.Parse(pizza.P.ToString());

        //        // Checks if there are enough resourses
        //        //bool checkInventory = Repo.SubsInventory(pizza.Name, user.UsersId, pizza.Size);




        //        // Add new pizza
        //        Repo.AddPizza(pizza.Size, pizza.Topping, pizza.GarlicCrust, pizza.OrderId);

        //        // For creating just an order for every pizza
        //        if (Count < 1)
        //        {
        //            // Add New Order
        //            Repo.AddOrders(order.OrderNumber,cust.FirstName,cust.LastName, order.OrderLocaton, order.PizzaPrice, order.PizzaFprice, order.OrderDate = DateTime.Now, order.StoreName, order.CustomerPhoneNumber);

        //        }
        //        Count++;



        //        // Search for the pizza and order Id
        //        //int? pizzaId = Repo.GetPizzaIdBySize(pizza.Name, pizza.Size);
        //        //int? orderId = Repo.GetOrderId(user.UsersId);


        //        // Add new orderPizza
        //        //Repo.AddOrderPizza(orderId, pizzaId);

        //        return RedirectToAction("PlaceAnOrder");
        //    }

        // if there are no more toppings for the pizza's

        //}
    }
}





