using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RBPizzaRest.Library;
using System;
using System.IO;
using System.Linq;

namespace RBPizzaRest.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            STM print = new STM();


            print.Menu();
        }
    }
}
