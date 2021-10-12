using StoreAdminMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoreAdminMVC.Filters
{
    public class LoopOverProductVM
    {
        public LoopOverProductVM(string name)
        {
            ShowConsole(name);
        }

        public void ShowConsole(string name)
        {
            Console.WriteLine($"Aboba is {name}");
        }
    }
}
