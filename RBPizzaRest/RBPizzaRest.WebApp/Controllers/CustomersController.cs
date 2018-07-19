using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RBPizzaRest.DataAccess;
using RBPizzaRest.WebApp.Models;

namespace RBPizzaRest.WebApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly PizzaStoreDBContext _context;
        private readonly PizzaStoreRepository Repo;
        

        public CustomersController(PizzaStoreDBContext context, PizzaStoreRepository repo)
        {
            _context = context;
            Repo = repo;
        }


        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customer.ToListAsync());
        }

        public IActionResult Register()
        {
            MVCustomer cust = new MVCustomer();
            return View(cust);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Register(MVCustomer cust)
        {
            bool isUser = false;
            TempData["firstname"] = cust.FirstName;
            TempData["lastname"] = cust.LastName;
            TempData["phone"] = cust.PhoneNumber;
            TempData["Count"] = 1;// counter for numbers pizza increments in the ordersController
            TempData["order_total"] = 0;

            var allCust = Repo.GetCustomers();

            foreach (var oneCust in allCust)
            {
                if (oneCust.FirstName == cust.FirstName && oneCust.PhoneNumber == cust.PhoneNumber)
                {
                    cust.Id = oneCust.Id;
                    TempData["custid"] = cust.Id;
                    isUser = true;
                    break;
                }

            }

            if (isUser == true)
            {

            }
            else if (isUser == false)
            {
                TempData["welcomemsg"] = "Welcome " + cust.FirstName;

                //create new user
                Repo.AddCustomer(cust.FirstName, cust.LastName, cust.PhoneNumber, cust.Location = "RESTON");
                Repo.SaveChanges();

                //get id of new user here 
                var item = Repo.GetCustomers().FirstOrDefault(x => x.FirstName == cust.FirstName
                                                    && x.PhoneNumber == cust.PhoneNumber); 
                cust.Id = item.Id;
                TempData["custid"] = cust.Id;
            }

            return RedirectToAction("ChooseLocation", "Locations", cust);
        }

        public IActionResult getCust()
        {
            
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public  IActionResult Register(MVCustomer Cust)
        //{
        //    bool isUser = false;
        //    // var user =  _context.Users.FindAsync(users.UsersId);
        //    // var currentUserID =  Repo.GetUserIDByPhone(user.Name, user.Phone);

        //    //get users 
        //    //var u = new OrdersRepository(new PizzaPlaceContext());

        //    var allCustomers = Repo.GetCustomers();

        //    foreach (var oneCustomer in allCustomers)
        //    {
        //        if (oneCustomer.FirstName == Cust.FirstName && oneCustomer.PhoneNumber == Cust.PhoneNumber)
        //        {
        //            isUser = true;             
        //            goto newUser;
        //        }
               
        //    }

        //    newUser:
        //    if(isUser == true)
        //    {
        //        var myCustomer = new MVCustomer
        //        {
        //            FirstName = Cust.FirstName,
        //            LastName = Cust.LastName,
        //            PhoneNumber = Cust.Location
        //        };
                
        //    }
        //    else if (isUser == false)
        //    {


        //        //create new user
        //        Repo.AddCustomer(Cust.FirstName, Cust.LastName, Cust.PhoneNumber);
        //        Repo.SaveChanges();
                
               
                
        //    }

        //    return RedirectToAction("Index","Locations");
        //   // return View(user);

        //}

        


        //public async Task<IActionResult> VerifiCust(string searchString, string pnum)
        //{
        //    var Cust = from m in _context.Customer
        //                 select m;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        Cust = Cust.Where(s => s.FirstName.Contains(searchString)
        //                                && s.PhoneNumber.Contains(pnum));
        //    }

        //    return View(await Cust.ToListAsync());
        //}

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,PhoneNumber,Location")] MVCustomer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }


        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,PhoneNumber,Location")] MVCustomer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }

        public IActionResult AddNewUser()
        {
            MVCustomer customer = new MVCustomer();
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewUser(MVCustomer customer)
        {

            bool foundcust = false;


            var allcust = Repo.GetCustomers();

            foreach (var aUser in allcust)
            {
                if (aUser.FirstName == customer.FirstName && aUser.PhoneNumber == customer.PhoneNumber)
                {
                    customer.Id = aUser.Id ;
                    foundcust = true;
                    goto aNewUser;
                }

            }

            aNewUser:
            if (foundcust == true)
            {
                var aCust = new MVCustomer
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                    Location = "RESTON"
                };

            }
            else if (foundcust == false)
            {


                //create new user
                Repo.AddCustomer(customer.FirstName, customer.LastName, customer.PhoneNumber);
                Repo.SaveChanges();



            }

            return RedirectToAction("Index", "Locations", customer);
            // return View(user);
        }


    }

}
