using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace RBPizzaRest.Library
{
    public class STM
    {
        public void Menu()
        {
            bool exit = false;
            do
            {

                int u = 0;
                string password = "";

                Console.Clear();
                Console.WriteLine("**************************************************");
                Console.WriteLine("----------RBPizza Restaurants App----------");
                Console.WriteLine();
                Console.WriteLine("1) Create User.");
                Console.WriteLine("2) Place an Order.");
                Console.WriteLine("3) Administration.");
                Console.WriteLine("4) Confirm Customer.");
                Console.WriteLine("5) Exit.");
                Console.WriteLine();
                Console.WriteLine("---------- Revature ----------");
                Console.WriteLine("**************************************************");
                Console.WriteLine();
                Console.Write("----------> ");
                u = Convert.ToInt32(Console.ReadLine());

                switch (u)
                {
                    case 1:
                        {
                            CreateUser();
                            break;
                        }
                    case 2:
                        {
                            PlaceOrder2();
                            break;
                        }
                    case 3:
                        {
                            Admin();
                            break;
                        }
                    case 4:
                        {
                            SearchCustomer();
                            break;
                        }
                    case 5:
                        {
                            exit = false;
                            break;
                        }
                    default:
                        break;
                }

            } while (exit != false);
        }

        public void Admin()
        {
            
            Console.Clear();
            Console.WriteLine("**************************************************");
            Console.WriteLine("----------Admin----------");
            Console.WriteLine();
            Console.WriteLine("1) See history by location");
            Console.WriteLine("2) See history by user.");
            Console.WriteLine("3) sort.");
            Console.WriteLine("4) back.");
            Console.WriteLine();
            Console.WriteLine("---------- Revature ----------");
            Console.WriteLine("**************************************************");
            Console.WriteLine();
            Console.Write("----------> ");
            int u = Convert.ToInt32(Console.ReadLine());

            switch (u)
            {
                case 1:
                    {
                        OrdersbyLocation();
                        break;
                    }
                case 2:
                    {
                        OrdersbyUser();
                        break;
                    }
                case 3:
                    {
                        Sort();
                        break;
                    }
                case 4:
                    {
                        Menu();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Wrong input!");
                        Console.ReadLine();
                        Menu();
                    }
                    break;
            }
        }

        public void PlaceOrder1()
        {
            Random rdm = new Random();
            int ONum = rdm.Next(1, 1000000);
            string CName;
            string Pnum;
            string LName = "";
            string Olocation = "";
            double PPrice = 0;
            double PFPrice;
            DateTime ODate = DateTime.Now;
            string storeName = "";

            string Psize = "";
            string Ptopping;
            bool PGcrust = false;
            int Oid = ONum;

            List<Pizza> piz = new List<Pizza>();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<PizzaStoreDBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PizzaStoreDB"));

            var repo = new PizzaStoreRepository(new PizzaStoreDBContext(optionsBuilder.Options));

            var PST = repo.GetCustomers();

            Console.Clear();
            Console.WriteLine("------Place you're order------");
            Console.WriteLine("");
            Console.Write("Enter youre name: ");
            CName = Console.ReadLine();
            Console.Write("And you're phone number: ");
            Pnum = Console.ReadLine();
            if (Pnum == null || Pnum == " ")
            {
                do
                {
                    Console.WriteLine();
                    Console.WriteLine("Warning: You need to enter a Phone Number in order to identify you.");
                    Console.Write("Please enter your phone number: ");
                    Pnum = Console.ReadLine();

                } while (Pnum != null || Pnum != "");
            }

            var cust = PST.FirstOrDefault(x => x.FirstName == CName
                                           && x.PhoneNumber == Pnum);

            if (cust.FirstName != CName && cust.PhoneNumber != Pnum)
            {
                Console.WriteLine("WARNING: No such customer exists please try again...");
                Menu();
            }
            else
            {
                Console.WriteLine($"customer found!");
                Console.Clear();

                Suggested(CName, Pnum);

                var PSS = repo.GetStores();
                int count = 1;

                Console.WriteLine("------Pizza------");
                Console.WriteLine("");
                foreach (var item in PSS)
                {
                    Console.WriteLine();
                    Console.WriteLine("==============================");
                    Console.WriteLine($"{count}) {item.StoreName} ({item.StoreLocation})");
                    Console.WriteLine("==============================");
                    count++;
                }
                Console.WriteLine("Where do you want to order from: ");
                int ch = Convert.ToInt32(Console.ReadLine());

                switch (ch)
                {
                    case 1:
                        {
                            storeName = "RBPizzas";
                            break;
                        }
                    case 2:
                        {
                            storeName = "Washinton DC";
                            break;
                        }
                    case 3:
                        {
                            storeName = "Alexandria";
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong input");
                            Menu();
                        }
                        break;
                }
                //storeName = Console.ReadLine();
                //if (storeName == null || storeName == " " || storeName != "RBPizzas" || storeName != "T'Challa's Slice" || storeName != "Infinity Flavor")
                //{
                //    do
                //    {
                //        Console.WriteLine();
                //        Console.WriteLine("Warning: You need to enter a valid location.");
                //        Console.WriteLine("Where do you want to order from: ");
                //        storeName = Console.ReadLine();

                //    } while (storeName != null || storeName != "" || storeName == "RBPizzas" || storeName == "T'Challa's Slice" || storeName == "Infinity Flavor");
                //}

                Console.Write("How many pizzas do you want? (Max 12) ");
                int quant = Convert.ToInt32(Console.ReadLine());
                if (quant < 0 || quant > 12)
                {
                    do
                    {
                        Console.WriteLine();
                        Console.WriteLine("Warning: You need to enter a valid number of pizzas.");
                        Console.WriteLine("How many pizzas do you want? (Max 12) ");
                        quant = Convert.ToInt32(Console.ReadLine());

                    } while (quant > 0 || quant < 12);
                }
                if (quant != 0)
                {
                    for (int i = 0; i < quant; i++)
                    {
                        Console.Write("Choose you're pizza size: (1.REGULAR, 2.MEDIUM, 3.LARGE, 4.EXTRALARGE)");
                        int ps = Convert.ToInt32(Console.ReadLine());


                        switch (ps)
                        {
                            case 1:
                                {
                                    Psize = "REGULAR";
                                    break;
                                }
                            case 2:
                                {
                                    Psize = "MEDIUM";
                                    break;
                                }
                            case 3:
                                {
                                    Psize = "LARGE";
                                    break;
                                }
                            case 4:
                                {
                                    Psize = "EXTRALARGE";
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("Wrong input");
                                    Menu();
                                }
                                break;
                        }

                        //Psize.ToUpper();
                        //if (Psize == null || Psize == "" || Psize != "REGULAR" || Psize != "MEDIUM" || Psize != "LARGE" || Psize != "EXTRALARGE")
                        //{
                        //    do
                        //    {
                        //        Console.WriteLine();
                        //        Console.WriteLine("Warning: You need to enter a valid pizza size.");
                        //        Console.Write("Choose you're pizza size: (REGULAR, MEDIUM, LARGE, EXTRALARGE) (Enter size carefully) ");
                        //        Psize = Console.ReadLine();
                        //        Psize.ToUpper();

                        //    } while (Psize != null || Psize != "" || Psize == "REGULAR" || Psize == "MEDIUM" || Psize == "LARGE" || Psize == "EXTRALARGE");
                        //}

                        if (Psize == "REGULAR")
                        {
                            PPrice = +5;
                        }
                        else if (Psize == "MEDIUM")
                        {
                            PPrice = +10;
                        }
                        else if (Psize == "LARGE")
                        {
                            PPrice = +15;
                        }
                        else if (Psize == "EXTRALARGE")
                        {
                            PPrice = +20;
                        }

                        Console.Write("Choose you're tooping: (1.CHICKEN, 2.PEPPERONI, 3.VEGGIES)");
                        int pt = Convert.ToInt32(Console.ReadLine());
                        Ptopping = Console.ReadLine();


                        switch (pt)
                        {
                            case 1:
                                {
                                    Ptopping = "CHICKEN";
                                    break;
                                }
                            case 2:
                                {
                                    Ptopping = "PEPPERONI";
                                    break;
                                }
                            case 3:
                                {
                                    Ptopping = "VEGGIES";
                                    break;
                                }
                            case 4:
                                {
                                    Psize = "EXTRALARGE";
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("Wrong input");
                                    Menu();
                                }
                                break;
                        }
                        //if (Ptopping == null || Ptopping == "" || Ptopping != "CHICKEN" || Ptopping != "PEPPERONI" || Ptopping != "VEGGIES")
                        //{
                        //    do
                        //    {
                        //        Console.WriteLine();
                        //        Console.WriteLine("Warning: You need to enter a valid pizza topping.");
                        //        Console.Write("Choose you're tooping: (CHICKEN, PEPPERONI, VEGGIES) (Enter tooping carefully)");
                        //        Ptopping = Console.ReadLine();
                        //        Ptopping.ToUpper();

                        //    } while (Ptopping != null || Ptopping != "" || Ptopping == "CHICKEN" || Ptopping == "PEPPERONI" || Ptopping == "VEGGIES");
                        //}

                        if (Ptopping == "CHICKEN")
                        {
                            PPrice = +1;
                        }
                        else if (Ptopping == "PEPPERONI")
                        {
                            PPrice = +.50;
                        }
                        else if (Ptopping == "VEGGIES")
                        {
                            PPrice = +3;
                        }
                        Console.Write("Do you want garlic on you're pizza crust: [Y/N] ");
                        string ans = Console.ReadLine();
                        ans = ans.ToUpper();
                        if (ans == "Y")
                        {
                            PGcrust = true;
                        }
                        else if (ans == "N")
                        {
                            PGcrust = false;
                        }
                        else
                        {
                            PGcrust = false;
                        }

                        if (PGcrust == true)
                        {
                            PPrice = +1;
                        }

                        piz[i].GarlicCrust = false;
                        piz[i].OrderId = Oid;
                        piz[i].Size = Psize;
                        piz[i].Topping = Ptopping;
                    }
                }
                else
                {
                    Console.WriteLine("WARNING: You cant order 0 pizzas.");
                    Console.ReadLine();
                    Menu();
                }


                PFPrice = (PPrice + (PPrice * .08));
                if (PFPrice < 500)
                {
                    repo.AddOrders(ONum, CName, LName = cust.LastName, Olocation = cust.Location.ToUpper(), PPrice, PFPrice, ODate, storeName = "reston", Pnum);
                    repo.SaveChanges();
                    //repo.AddPizza(Psize, Ptopping, PGcrust, Oid
                    foreach (var item in piz)
                    {
                        repo.AddPizza(item.Size, item.Topping, item.GarlicCrust, item.OrderId);
                        repo.SaveChanges();
                    }
                }
                else
                {
                    Console.WriteLine("WARNING: TOTAL PRICE SURPASSES 500$ ");
                    Console.ReadLine();
                    Menu();
                }

                Console.Clear();
                Console.WriteLine("---Order Details---");
                Console.WriteLine();
                Console.WriteLine($"Order Number: {ONum}");
                Console.Write(CName.ToUpper() + " ");
                Console.WriteLine(LName.ToUpper());
                Console.WriteLine($"Customer #: {Pnum}");
                Console.WriteLine($"Order location: {Olocation}");
                Console.WriteLine($"Store: {storeName}");
                foreach (var item in piz)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Pizza size: {item.Size}");
                    Console.WriteLine($"Pizza Topping: {item.Topping}");
                    Console.WriteLine($"Garlic crust: {item.GarlicCrust}");
                    Console.WriteLine();
                }
                Console.WriteLine($"Garlic Crust: {PGcrust}");
                Console.WriteLine($"Price: {PPrice}");
                Console.WriteLine($"Total Price: {PFPrice}");
                Console.WriteLine($"Order Date: {ODate}");
            }





        }

        public void PlaceOrder2()
        {
            Random rdm = new Random();
            int ONum = rdm.Next(1, 1000000);
            string CName;
            string Pnum;
            string LName = "";
            string Olocation = "";
            double PPrice = 0;
            double PFPrice;
            DateTime ODate = DateTime.Now;
            string storeName = "";

            string Psize = "";
            string Ptopping;
            bool PGcrust = false;
            int Oid = ONum;

            List<Pizza> piz = new List<Pizza>();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<PizzaStoreDBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PizzaStoreDB"));

            var repo = new PizzaStoreRepository(new PizzaStoreDBContext(optionsBuilder.Options));

            var PST = repo.GetCustomers();

            Console.Clear();
            Console.WriteLine("------Place you're order------");
            Console.WriteLine("");
            Console.Write("Enter youre name: ");
            CName = Console.ReadLine();
            CName = CName.ToUpper();
            Console.Write("And you're phone number: ");
            Pnum = Console.ReadLine();
            if (Pnum == null || Pnum == " ")
            {
                do
                {
                    Console.WriteLine();
                    Console.WriteLine("Warning: You need to enter a Phone Number in order to identify you.");
                    Console.Write("Please enter your phone number: ");
                    Pnum = Console.ReadLine();

                } while (Pnum != null || Pnum != "");
            }

            var cust = PST.FirstOrDefault(x => x.FirstName == CName
                                           && x.PhoneNumber == Pnum);
            try
            {
                if (cust.FirstName != CName && cust.PhoneNumber != Pnum)
                {
                    Console.WriteLine("WARNING: No such customer exists please try again...");
                    Menu();
                }
                else
                {
                    Console.WriteLine($"customer found!");
                    Console.Clear();

                    Suggested(CName, Pnum);

                    var PSS = repo.GetStores();
                    int count = 1;

                    Console.WriteLine("------Pizza------");
                    Console.WriteLine("");
                    foreach (var item in PSS)
                    {
                        Console.WriteLine();
                        Console.WriteLine("==============================");
                        Console.WriteLine($"{count}) {item.StoreName} ({item.StoreLocation})");
                        Console.WriteLine("==============================");
                        count++;
                    }
                    Console.Write("Where do you want to order from: ");
                    int ch = Convert.ToInt32(Console.ReadLine());

                    switch (ch)
                    {
                        case 1:
                            {
                                storeName = "RBPizzas";
                                break;
                            }
                        case 2:
                            {
                                storeName = "Washinton DC";
                                break;
                            }
                        case 3:
                            {
                                storeName = "Alexandria";
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Wrong input");
                                Menu();
                            }
                            break;
                    }

                    Console.Write("Choose you're pizza size: (1.REGULAR, 2.MEDIUM, 3.LARGE, 4.EXTRALARGE)");
                    int ps = Convert.ToInt32(Console.ReadLine());
                    switch (ps)
                    {
                        case 1:
                            {
                                Psize = "REGULAR";
                                break;
                            }
                        case 2:
                            {
                                Psize = "MEDIUM";
                                break;
                            }
                        case 3:
                            {
                                Psize = "LARGE";
                                break;
                            }
                        case 4:
                            {
                                Psize = "EXTRALARGE";
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Wrong input");
                                Menu();
                            }
                            break;
                    }

                    if (Psize == "REGULAR")
                    {
                        PPrice = PPrice + 5;
                    }
                    else if (Psize == "MEDIUM")
                    {
                        PPrice = PPrice + 10;
                    }
                    else if (Psize == "LARGE")
                    {
                        PPrice = PPrice + 15;
                    }
                    else if (Psize == "EXTRALARGE")
                    {
                        PPrice = PPrice + 20;
                    }

                    Console.Write("Choose you're tooping: (1.CHICKEN, 2.PEPPERONI, 3.VEGGIES)");
                    int pt = Convert.ToInt32(Console.ReadLine());
                    Ptopping = Console.ReadLine();
                    switch (pt)
                    {
                        case 1:
                            {
                                Ptopping = "CHICKEN";
                                break;
                            }
                        case 2:
                            {
                                Ptopping = "PEPPERONI";
                                break;
                            }
                        case 3:
                            {
                                Ptopping = "VEGGIES";
                                break;
                            }
                        case 4:
                            {
                                Psize = "EXTRALARGE";
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Wrong input");
                                Menu();
                            }
                            break;
                    }

                    if (Ptopping == "CHICKEN")
                    {
                        PPrice = PPrice + 1;
                    }
                    else if (Ptopping == "PEPPERONI")
                    {
                        PPrice = PPrice + .50;
                    }
                    else if (Ptopping == "VEGGIES")
                    {
                        PPrice = PPrice + 3;
                    }
                    Console.Write("Do you want garlic on you're pizza crust: [Y/N] ");
                    string ans = Console.ReadLine();
                    ans = ans.ToUpper();
                    if (ans == "Y")
                    {
                        PGcrust = true;
                    }
                    else if (ans == "N")
                    {
                        PGcrust = false;
                    }
                    else
                    {
                        PGcrust = false;
                    }

                    if (PGcrust == true)
                    {
                        PPrice = PPrice + 1;
                    }


                    PFPrice = (PPrice + (PPrice * .08));
                    if (PFPrice < 500)
                    {
                        repo.AddOrders(ONum, CName, LName = cust.LastName, Olocation = cust.Location.ToUpper(), PPrice, PFPrice, ODate, storeName, Pnum);
                        repo.SaveChanges();
                        repo.AddPizza(Psize, Ptopping, PGcrust, Oid);
                        repo.SaveChanges();

                    }
                    else
                    {
                        Console.WriteLine("WARNING: TOTAL PRICE SURPASSES 500$ ");
                        Console.ReadLine();
                        Menu();
                    }

                    Console.Clear();
                    Console.WriteLine("---Order Details---");
                    Console.WriteLine();
                    Console.WriteLine($"Order Number: {ONum}");
                    Console.Write(CName.ToUpper() + " ");
                    Console.WriteLine(LName.ToUpper());
                    Console.WriteLine($"Customer #: {Pnum}");
                    Console.WriteLine($"Order location: {Olocation}");
                    Console.WriteLine($"Store: {storeName}");
                    Console.WriteLine($"Pizza size: {Psize}");
                    Console.WriteLine($"Pizza topping: {Ptopping}");
                    Console.WriteLine($"Garlic Crust: {PGcrust}");
                    Console.WriteLine($"Price: {PPrice}");
                    Console.WriteLine($"Total Price: {PFPrice}");
                    Console.WriteLine($"Order Date: {ODate}");
                    Console.ReadLine();
                    Menu();
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Error!");
                Console.ReadLine();
                Menu();
            }
            





        }


        public void OrdersbyLocation()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<PizzaStoreDBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PizzaStoreDB"));

            var repo = new PizzaStoreRepository(new PizzaStoreDBContext(optionsBuilder.Options));

            string location = "";
            int loc;
            int count = 1;

            var STS = repo.GetStores();

            foreach (var item in STS)
            {
                Console.WriteLine("==============================");
                Console.WriteLine($"{count}) {item.StoreName} ({item.StoreLocation})");
                Console.WriteLine("==============================");
                count++;
            }
            loc = Convert.ToInt32(Console.ReadLine());

            switch (loc)
            {
                case 1:
                    {
                        var l = STS.FirstOrDefault(x => x.Id == loc);
                        location = l.StoreLocation;
                        break;
                    }
                case 2:
                    {
                        var l = STS.FirstOrDefault(x => x.Id == loc);
                        location = l.StoreLocation;
                        break;
                    }
                case 3:
                    {
                        var l = STS.FirstOrDefault(x => x.Id == loc);
                        location = l.StoreLocation;
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Wrong input!!");
                        Menu();
                    }
                    break;
            }


            var PSO = repo.GetOrders().Where(x => x.OrderLocaton == location);
            var PSP = repo.GetPizzas();

            Console.WriteLine("********** ORDER HISTORY by Location **********");
            Console.WriteLine();
            foreach (var item in PSO)
            {
                Console.WriteLine("______________________________");
                Console.WriteLine($"Order Number: {item.OrderNumber}");
                Console.WriteLine($"Customer Name: {item.CustomerName} {item.CustomerLastname}");
                Console.WriteLine($"Customer #: {item.CustomerPhoneNumber}");
                Console.WriteLine($"Order location: {item.OrderLocaton}");
                Console.WriteLine($"Store: {item.StoreName}");
                foreach (var pizza in PSP.Where(x => x.OrderId == item.OrderNumber))
                {
                    Console.WriteLine();
                    Console.WriteLine("||||||||||||||||||||");
                    Console.WriteLine($"Pizza size: {pizza.Size}");
                    Console.WriteLine($"Pizza Topping: {pizza.Topping}");
                    Console.WriteLine("||||||||||||||||||||");
                    Console.WriteLine();
                }
                Console.WriteLine($"Price: {item.PizzaPrice}");
                Console.WriteLine($"Total Price: {item.PizzaFprice}");
                Console.WriteLine($"Order Date: {item.OrderDate}");
                Console.WriteLine("______________________________");
                Console.WriteLine();

            }

            Console.ReadLine();
            Admin();
        }

        public void OrdersbyUser()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<PizzaStoreDBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PizzaStoreDB"));

            var repo = new PizzaStoreRepository(new PizzaStoreDBContext(optionsBuilder.Options));

            string CName;
            string Pnumber;



            Console.Write("Enter youre name: ");
            CName = Console.ReadLine();
            CName = CName.ToUpper();
            Console.Write("And you're phone number: ");
            Pnumber = Console.ReadLine();



            var PSC = repo.GetCustomers().FirstOrDefault(x => x.FirstName == CName
                                                         && x.PhoneNumber == Pnumber);

            if (PSC.FirstName == CName && PSC.PhoneNumber == Pnumber)
            {
                var PSO = repo.GetOrders().Where(x => x.CustomerName == CName
                                                 && x.CustomerPhoneNumber == Pnumber);
                var PSP = repo.GetPizzas();

                Console.WriteLine("********** ORDER HISTORY by Location **********");
                Console.WriteLine();
                foreach (var item in PSO)
                {
                    Console.WriteLine("______________________________");
                    Console.WriteLine($"Order Number: {item.OrderNumber}");
                    Console.WriteLine($"Customer Name: {item.CustomerName} {item.CustomerLastname}");
                    Console.WriteLine($"Customer #: {item.CustomerPhoneNumber}");
                    Console.WriteLine($"Order location: {item.OrderLocaton}");
                    Console.WriteLine($"Store: {item.StoreName}");
                    foreach (var pizza in PSP.Where(x => x.OrderId == item.OrderNumber))
                    {
                        Console.WriteLine();
                        Console.WriteLine("||||||||||||||||||||");
                        Console.WriteLine($"Pizza size: {pizza.Size}");
                        Console.WriteLine($"Pizza Topping: {pizza.Topping}");
                        Console.WriteLine("||||||||||||||||||||");
                        Console.WriteLine();
                    }
                    Console.WriteLine($"Price: {item.PizzaPrice}");
                    Console.WriteLine($"Total Price: {item.PizzaFprice}");
                    Console.WriteLine($"Order Date: {item.OrderDate}");
                    Console.WriteLine("______________________________");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Error");
                Console.ReadLine();
                Admin();
            }

            Console.ReadLine();
            Admin();
        }

        public void Sort()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<PizzaStoreDBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PizzaStoreDB"));

            var repo = new PizzaStoreRepository(new PizzaStoreDBContext(optionsBuilder.Options));


            var PSP = repo.GetPizzas();

            int i = 0;


            Console.WriteLine("1) by earliest.");
            Console.WriteLine("2) by latest.");
            Console.WriteLine("3) by lowest price.");
            Console.WriteLine("4) by highest price.");
            Console.WriteLine("How would you like to see the orders: ");
            i = Convert.ToInt32(Console.ReadLine());

            switch (i)
            {

                case 1:
                    {
                        var OE = repo.GetOrders().OrderBy(x => x.OrderDate);
                        foreach (var item in OE)
                        {
                            Console.WriteLine("______________________________");
                            Console.WriteLine($"Order Number: {item.OrderNumber}");
                            Console.WriteLine($"Customer Name: {item.CustomerName} {item.CustomerLastname}");
                            Console.WriteLine($"Customer #: {item.CustomerPhoneNumber}");
                            Console.WriteLine($"Order location: {item.OrderLocaton}");
                            Console.WriteLine($"Store: {item.StoreName}");
                            foreach (var pizza in PSP.Where(x => x.OrderId == item.OrderNumber))
                            {
                                Console.WriteLine();
                                Console.WriteLine("||||||||||||||||||||");
                                Console.WriteLine($"Pizza size: {pizza.Size}");
                                Console.WriteLine($"Pizza Topping: {pizza.Topping}");
                                Console.WriteLine("||||||||||||||||||||");
                                Console.WriteLine();
                            }
                            Console.WriteLine($"Price: {item.PizzaPrice}");
                            Console.WriteLine($"Total Price: {item.PizzaFprice}");
                            Console.WriteLine($"Order Date: {item.OrderDate}");
                            Console.WriteLine("______________________________");
                            Console.WriteLine();
                        }
                        Console.ReadLine();
                        Admin();
                        break;
                    }
                case 2:
                    {
                        var OE = repo.GetOrders().OrderByDescending(x => x.OrderDate);
                        foreach (var item in OE)
                        {
                            Console.WriteLine("______________________________");
                            Console.WriteLine($"Order Number: {item.OrderNumber}");
                            Console.WriteLine($"Customer Name: {item.CustomerName} {item.CustomerLastname}");
                            Console.WriteLine($"Customer #: {item.CustomerPhoneNumber}");
                            Console.WriteLine($"Order location: {item.OrderLocaton}");
                            Console.WriteLine($"Store: {item.StoreName}");
                            foreach (var pizza in PSP.Where(x => x.OrderId == item.OrderNumber))
                            {
                                Console.WriteLine();
                                Console.WriteLine("||||||||||||||||||||");
                                Console.WriteLine($"Pizza size: {pizza.Size}");
                                Console.WriteLine($"Pizza Topping: {pizza.Topping}");
                                Console.WriteLine("||||||||||||||||||||");
                                Console.WriteLine();
                            }
                            Console.WriteLine($"Price: {item.PizzaPrice}");
                            Console.WriteLine($"Total Price: {item.PizzaFprice}");
                            Console.WriteLine($"Order Date: {item.OrderDate}");
                            Console.WriteLine("______________________________");
                            Console.WriteLine();
                        }
                        Console.ReadLine();
                        Admin();
                        break;
                    }
                case 3:
                    {
                        var OE = repo.GetOrders().OrderBy(x => x.PizzaFprice);
                        foreach (var item in OE)
                        {
                            Console.WriteLine("______________________________");
                            Console.WriteLine($"Order Number: {item.OrderNumber}");
                            Console.WriteLine($"Customer Name: {item.CustomerName} {item.CustomerLastname}");
                            Console.WriteLine($"Customer #: {item.CustomerPhoneNumber}");
                            Console.WriteLine($"Order location: {item.OrderLocaton}");
                            Console.WriteLine($"Store: {item.StoreName}");
                            foreach (var pizza in PSP.Where(x => x.OrderId == item.OrderNumber))
                            {
                                Console.WriteLine();
                                Console.WriteLine("||||||||||||||||||||");
                                Console.WriteLine($"Pizza size: {pizza.Size}");
                                Console.WriteLine($"Pizza Topping: {pizza.Topping}");
                                Console.WriteLine("||||||||||||||||||||");
                                Console.WriteLine();
                            }
                            Console.WriteLine($"Price: {item.PizzaPrice}");
                            Console.WriteLine($"Total Price: {item.PizzaFprice}");
                            Console.WriteLine($"Order Date: {item.OrderDate}");
                            Console.WriteLine("______________________________");
                            Console.WriteLine();
                        }
                        Console.ReadLine();
                        Admin();
                        break;
                    }
                case 4:
                    {
                        var OE = repo.GetOrders().OrderByDescending(x => x.PizzaFprice);
                        foreach (var item in OE)
                        {
                            Console.WriteLine("______________________________");
                            Console.WriteLine($"Order Number: {item.OrderNumber}");
                            Console.WriteLine($"Customer Name: {item.CustomerName} {item.CustomerLastname}");
                            Console.WriteLine($"Customer #: {item.CustomerPhoneNumber}");
                            Console.WriteLine($"Order location: {item.OrderLocaton}");
                            Console.WriteLine($"Store: {item.StoreName}");
                            foreach (var pizza in PSP.Where(x => x.OrderId == item.OrderNumber))
                            {
                                Console.WriteLine();
                                Console.WriteLine("||||||||||||||||||||");
                                Console.WriteLine($"Pizza size: {pizza.Size}");
                                Console.WriteLine($"Pizza Topping: {pizza.Topping}");
                                Console.WriteLine("||||||||||||||||||||");
                                Console.WriteLine();
                            }
                            Console.WriteLine($"Price: {item.PizzaPrice}");
                            Console.WriteLine($"Total Price: {item.PizzaFprice}");
                            Console.WriteLine($"Order Date: {item.OrderDate}");
                            Console.WriteLine("______________________________");
                            Console.WriteLine();
                        }
                        Console.ReadLine();
                        Admin();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Error!");
                        Console.ReadLine();
                        Admin();
                    }
                    break;
            }

        }

        public void Suggested(string name, string number)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<PizzaStoreDBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PizzaStoreDB"));

            var repo = new PizzaStoreRepository(new PizzaStoreDBContext(optionsBuilder.Options));

            var PST = repo.GetCustomers();

            var cust = PST.FirstOrDefault(x => x.FirstName == name
                                          && x.PhoneNumber == number);

            if (cust != null && cust.FirstName == name && cust.PhoneNumber == number)
            {
                try
                {
                    var od = repo.GetOrders().LastOrDefault(x => x.CustomerName == name
                                                                && x.CustomerPhoneNumber == number);
                    var p = repo.GetPizzas().LastOrDefault(x => x.OrderId == od.OrderNumber);

                    Console.WriteLine("Heres a Suggestion: ");
                    Console.WriteLine($"A {p.Size} Pizza ");
                    Console.WriteLine($"With {p.Topping} topping");

                }
                catch (Exception)
                {

                    Console.WriteLine("Nothing to suggest.");
                }

            }
        }

        public void CreateUser()
        {
            string FName = null;
            string LName = null;
            string PNumber = null;
            string Loc = "";

            Console.Clear();
            Console.WriteLine("------User Creation------");
            Console.WriteLine(" ");
            Console.Write("Please enter your first name: ");
            FName = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Please enter your last name: ");
            LName = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Please enter your phone number: ");
            PNumber = Console.ReadLine();
            if (PNumber == null || PNumber == " ")
            {
                do
                {
                    Console.WriteLine();
                    Console.WriteLine("Warning: You need to enter a Phone Number in order to identify you.");
                    Console.Write("Please enter your phone number: ");
                    PNumber = Console.ReadLine();

                } while (PNumber != null || PNumber != "");
            }
            Console.WriteLine();
            Console.WriteLine("Please enter if you're from: ");
            Console.WriteLine("1) Reston.");
            Console.WriteLine("2) Washington DC.");
            Console.WriteLine("3) Alexandria.");
            int l = Convert.ToInt32(Console.ReadLine());

            switch (l)
            {
                case 1:
                    {
                        Loc = "Reston";
                        break;
                    }
                case 2:
                    {
                        Loc = "Washinton DC";
                        break;
                    }
                case 3:
                    {
                        Loc = "Alexandria";
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Wrong input!");
                        Console.ReadLine();
                        Menu();
                    }
                    break;
            }


            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<PizzaStoreDBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PizzaStoreDB"));

            var repo = new PizzaStoreRepository(new PizzaStoreDBContext(optionsBuilder.Options));

            var PST = repo;

            PST.AddCustomer(FName = FName.ToUpper(), LName = LName.ToUpper(), PNumber, Loc = Loc.ToUpper());
            PST.SaveChanges();
            Console.ReadLine();
            Menu();

        }

        public void SearchCustomer()
        {
            string Name;
            string Pnumber;

            Console.WriteLine("------Look for Customer------");
            Console.WriteLine("");
            Console.WriteLine("Please enter customer Name: ");
            Name = Console.ReadLine();
            Name = Name.ToUpper();
            Console.WriteLine("And phone number: ");
            Pnumber = Console.ReadLine();
            if (Pnumber == null || Pnumber == " ")
            {
                do
                {
                    Console.WriteLine();
                    Console.WriteLine("Warning: You need to enter a Phone Number in order to identify you.");
                    Console.Write("Please enter your phone number: ");
                    Pnumber = Console.ReadLine();

                } while (Pnumber != null || Pnumber != "");
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<PizzaStoreDBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PizzaStoreDB"));

            var repo = new PizzaStoreRepository(new PizzaStoreDBContext(optionsBuilder.Options));

            var PST = repo;

            PST.FindbyNameandnumber(Name, Pnumber);
            Menu();


        }
    }
}
