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
    public class LocationsController : Controller
    {
        private readonly PizzaStoreDBContext _context;

        public LocationsController(PizzaStoreDBContext context)
        {
            _context = context;
        }

        public IActionResult ChooseLocation(MVCustomer cust)
        {
            MVLocation location = new MVLocation();

            return View(location);

        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult ChooseLocation(MVLocation location, IFormCollection loc)
        {
            var selected = location.Name;
            // var selected = loc["LocationId"];
            location.Name = loc["SelectedLocation"];
            TempData["locationName"] = location.Name;
            //TempData["msg"] = "user id " + user.UsersId + "name " + user.FirstName;
            // TempData["name"] = "user name " + user.FirstName + "last name " + user.FirstName;


            return RedirectToAction("MakeOrder", "Orders");

        }


            // GET: Locations
            public async Task<IActionResult> Index()
        {
            return View(await _context.Location.ToListAsync());
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Location
                .FirstOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Location location)
        {
            if (ModelState.IsValid)
            {
                _context.Add(location);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Location.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Location location)
        {
            if (id != location.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.Id))
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
            return View(location);
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Location
                .FirstOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var location = await _context.Location.FindAsync(id);
            _context.Location.Remove(location);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
            return _context.Location.Any(e => e.Id == id);
        }

        // GET
        public IActionResult ChooseALocation(MVCustomer user)
        {
            MVLocation location = new MVLocation();
            return View(location);
        }

        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChooseALocation(MVLocation location, MVCustomer cust)
        {
            var select = location.Id;
            cust.Id = select;

            return RedirectToAction("PlaceAnOrder", "Orders", cust);
        }
    }
}
