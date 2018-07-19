using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RBPizzaRest.DataAccess
{
    public class PizzaStoreRepository
    {
        private readonly PizzaStoreDBContext _db;

        public PizzaStoreRepository(PizzaStoreDBContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IEnumerable<Customer> GetCustomers()
        {
            List<Customer> cust = _db.Customer.AsNoTracking().ToList();
            return cust;
        }

        public IEnumerable<Location> GetLocations()
        {
            List<Location> loc = _db.Location.AsNoTracking().ToList();
            return loc;
        }

        public IEnumerable<Orders> GetOrders()
        {
            List<Orders> OD = _db.Orders.AsNoTracking().ToList();
            return OD;
        }

        public IEnumerable<Pizza> GetPizzas()
        {
            List<Pizza> Piz = _db.Pizza.AsNoTracking().ToList();
            return Piz;
        }

        public IEnumerable<Stores> GetStores()
        {
            List<Stores> ST = _db.Stores.AsNoTracking().ToList();
            return ST;
        }

        public IEnumerable<Toppings> GetToppings()
        {
            List<Toppings> topping = _db.Toppings.AsNoTracking().ToList();
            return topping;
        }

        public void FindbyNameandnumber(string name, string num)
        {
            // with LINQ it would be, .FirstOrDefault(m => m.Id == id)
            // but Find is better/faster
            var cust = _db.Customer.FirstOrDefault(x => x.FirstName == name
                                                    && x.PhoneNumber == num);
            if (cust == null)
            {
                throw new ArgumentException("no such Customer exists");
            }
            else
            {
                Console.WriteLine(value: $"customer found: {cust.FirstName}" );
                Console.ReadLine();
            }
            
        }

        public void AddCustomer(string FName, string LName, string PNumber, string location = "RESTON")
        {
            // LINQ: First fails by throwing exception,
            // FirstOrDefault fails to just null
            //var genre = _db.Genre.FirstOrDefault(g => g.Name == PNumber);
            //if (genre == null)

            //    throw new ArgumentException("genre not found", nameof(PNumber));
            //}
            var customer = new Customer
            {
                FirstName = FName,
                LastName = LName,
                PhoneNumber = PNumber,
                Location = location
            };
            _db.Add(customer);
        }

        public void AddLocation(string location)
        {
            //var genre = _db.Genre.FirstOrDefault(g => g.Name == PNumber);
            //if (genre == null)

            //    throw new ArgumentException("genre not found", nameof(PNumber));
            //}
            var loc = new Location
            {
                Name = location,
            };
            _db.Add(loc);
        }

        public void AddOrders(int ONumber, string CName, string CLName, string OLoc, double PPrice, double PFPrice, DateTime ODate, string STN, string CPN)
        {
            // LINQ: First fails by throwing exception,
            // FirstOrDefault fails to just null
            //var genre = _db.Genre.FirstOrDefault(g => g.Name == PNumber);
            //if (genre == null)

            //    throw new ArgumentException("genre not found", nameof(PNumber));
            //}
            var Ord = new Orders
            {
                OrderNumber = ONumber,
                CustomerName = CName,
                CustomerLastname = CLName,
                OrderLocaton = OLoc,
                PizzaPrice = PPrice,
                PizzaFprice = PFPrice,
                OrderDate = ODate,
                StoreName = STN,
                CustomerPhoneNumber = CPN
            };
            _db.Add(Ord);
        }

        public void AddPizza(string size, string topping, bool GCrust, int OId)
        {
            // LINQ: First fails by throwing exception,
            // FirstOrDefault fails to just null
            //var genre = _db.Genre.FirstOrDefault(g => g.Name == PNumber);
            //if (genre == null)

            //    throw new ArgumentException("genre not found", nameof(PNumber));
            //}
            var pizza = new Pizza
            {
                Size = size,
                Topping = topping,
                GarlicCrust = GCrust,
                OrderId = OId
            };
            _db.Add(pizza);
        }

        public void AddStores(string STName, string STLoc, int dough, int sauce, int cheese)
        {
            // LINQ: First fails by throwing exception,
            // FirstOrDefault fails to just null
            //var genre = _db.Genre.FirstOrDefault(g => g.Name == PNumber);
            //if (genre == null)

            //    throw new ArgumentException("genre not found", nameof(PNumber));
            //}
            var stores = new Stores
            {
                StoreName = STName,
                StoreLocation = STLoc,
                Dough = dough,
                Sauce = sauce,
                Cheese = cheese
            };
            _db.Add(stores);
        }

        public void AddTopping(string topping, int quant, int ODId)
        {
            // LINQ: First fails by throwing exception,
            // FirstOrDefault fails to just null
            //var genre = _db.Genre.FirstOrDefault(g => g.Name == PNumber);
            //if (genre == null)

            //    throw new ArgumentException("genre not found", nameof(PNumber));
            //}
            var Top = new Toppings
            {
                Topping = topping,
                Quantity = quant,
                StoreId = ODId
            };
            _db.Add(Top);
        }

        //public void DeleteById(int id)
        //{
        //    // with LINQ it would be, .FirstOrDefault(m => m.Id == id)
        //    // but Find is better/faster
        //    var movie = _db.Movie.Find(id);
        //    if (movie == null)
        //    {
        //        throw new ArgumentException("no such movie id", nameof(id));
        //    }
        //    _db.Remove(movie);
        //}

        public void EditCustomer(Customer cust)
        {
            // would add it if it didn't exist
            _db.Update(cust);

            // sometimes we need to do it a different way
            //var trackedMovie = _db.Movie.Find(movie.Id);
            //_db.Entry(trackedMovie).CurrentValues.SetValues(movie);
        }

        public void EditLocation(Location loc)
        {
            // would add it if it didn't exist
            _db.Update(loc);

            // sometimes we need to do it a different way
            //var trackedMovie = _db.Movie.Find(movie.Id);
            //_db.Entry(trackedMovie).CurrentValues.SetValues(movie);
        }

        public void EditOrders(Orders OD)
        {
            // would add it if it didn't exist
            _db.Update(OD);

            // sometimes we need to do it a different way
            //var trackedMovie = _db.Movie.Find(movie.Id);
            //_db.Entry(trackedMovie).CurrentValues.SetValues(movie);
        }

        public void EditPizza(Pizza piz)
        {
            // would add it if it didn't exist
            _db.Update(piz);

            // sometimes we need to do it a different way
            //var trackedMovie = _db.Movie.Find(movie.Id);
            //_db.Entry(trackedMovie).CurrentValues.SetValues(movie);
        }

        public void EditStore(Stores ST)
        {
            // would add it if it didn't exist
            _db.Update(ST);

            // sometimes we need to do it a different way
            //var trackedMovie = _db.Movie.Find(movie.Id);
            //_db.Entry(trackedMovie).CurrentValues.SetValues(movie);
        }

        public void EditTopping(Toppings cust)
        {
            // would add it if it didn't exist
            _db.Update(cust);

            // sometimes we need to do it a different way
            //var trackedMovie = _db.Movie.Find(movie.Id);
            //_db.Entry(trackedMovie).CurrentValues.SetValues(movie);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

    }
}
