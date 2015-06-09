using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataVendingMachine2015
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine vm = new VendingMachine();

            for (int i = 0; i < 50; i++)
            {
                vm.AddProduct(new Product(ProductType.Chips));
                vm.AddProduct(new Product(ProductType.Candy));
                vm.AddProduct(new Product(ProductType.Cola));
            }

            for (int i = 0; i < 20; i++)
            {
                vm.AddCoin(ValuedCoin.Quarter);
                vm.AddCoin(ValuedCoin.Dime);
                vm.AddCoin(ValuedCoin.Nickel);
            }

            bool isLooping = true;

            while(isLooping)
            {
                var products = vm.GetProductsFromTray();
                if(products.Any())
                {
                    var productString = String.Join(", ", products.Select(p => p.ProductType.DisplayName));
                    string message = String.Format(
                        "You retrieve the following from the vending machine's tray: {0}.",
                        productString
                    );
                    Console.WriteLine(message);

                }

                var returnedCoins = vm.GetReturnedCoins();
                if(returnedCoins.Any())
                {
                    string message = String.Format(
                        "You retrieve {0:0} coin(s) totalling ${1:0.00} from the coin return.",
                        returnedCoins.Count(),
                        returnedCoins.Sum(c => (c as ValuedCoin).ValueInUsCents) / 100.0
                    );
                    Console.WriteLine(message);
                }

                Console.WriteLine("There is a vending machine in front of you.");
                Console.WriteLine("There are buttons for Cola, Chips, and Candy.");
                Console.WriteLine("The display reads: " + vm.GetDisplayText());
                Console.WriteLine(String.Empty);
                Console.WriteLine("Options:");
                Console.WriteLine("Insert (Q)uarter");
                Console.WriteLine("Insert (D)ime");
                Console.WriteLine("Insert (N)ickel");
                Console.WriteLine("Insert (P)enny");
                Console.WriteLine("Select (C)hips");
                Console.WriteLine("Select Co(L)a");
                Console.WriteLine("Select Cand(Y)");
                Console.WriteLine("Coin (R)eturn");
                Console.WriteLine("Quit (Esc)");

                Console.Write("> ");
                var command = Console.ReadKey();
                Console.WriteLine(String.Empty);
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine(String.Empty);

                switch(command.Key)
                {
                    case ConsoleKey.Q:
                        vm.InsertCoin(ValuedCoin.Quarter);
                        break;
                    case ConsoleKey.D:
                        vm.InsertCoin(ValuedCoin.Dime);
                        break;
                    case ConsoleKey.N:
                        vm.InsertCoin(ValuedCoin.Nickel);
                        break;
                    case ConsoleKey.P:
                        vm.InsertCoin(ValuedCoin.Penny);
                        break;
                    case ConsoleKey.C:
                        vm.SelectProduct(ProductType.Chips);
                        break;
                    case ConsoleKey.L:
                        vm.SelectProduct(ProductType.Cola);
                        break;
                    case ConsoleKey.Y:
                        vm.SelectProduct(ProductType.Candy);
                        break;
                    case ConsoleKey.R:
                        vm.MakeChange();
                        break;
                    case ConsoleKey.Escape:
                        isLooping = false;
                        break;
                }
                
            }
        }
    }
}
