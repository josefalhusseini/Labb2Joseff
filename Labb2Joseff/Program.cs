
namespace Labb2Joseff
{
    internal class Program
    {
        static List<Customer> customers = new List<Customer>()
        {
            new Customer("Knatte", "123"),
            new Customer("Fnatte", "321"),
            new Customer("Tjatte", "213")
        };

        static List<Product> products = new List<Product>()
        {
            new Product("Korv", 25m),
            new Product("Dricka", 15m),
            new Product("Äpple", 7m)
        };

        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("=== Josefs Webbshop ===");
                Console.WriteLine("1) Registrera ny kund");
                Console.WriteLine("2) Logga in");
                Console.WriteLine("3) Avsluta");
                Console.Write("Välj: ");

                int choice = ReadInt();

                if (choice == 1)
                {
                    Register();
                }
                else if (choice == 2)
                {
                    Login();
                }
                else if (choice == 3)
                {
                    running = false;
                }
                else
                {
                    Console.WriteLine("Fel val.");
                }

                Console.WriteLine();
            }
        }

        static void Register()
        {
            Console.WriteLine("=== Registrera ny kund ===");
            Console.Write("Namn: ");
            string name = Console.ReadLine();
            Console.Write("Lösenord: ");
            string pw = Console.ReadLine();

            if (name == "" || pw == "")
            {
                Console.WriteLine("Namn och lösenord får inte vara tomt.");
                return;
            }

            for (int i = 0; i < customers.Count; i++)
            {
                if (customers[i].Name == name)
                {
                    Console.WriteLine("Kunden finns redan.");
                    return;
                }
            }

            customers.Add(new Customer(name, pw));
            Console.WriteLine("Kund skapad!");
        }

        static void Login()
        {
            Console.WriteLine("=== Logga in ===");
            Console.Write("Namn: ");
            string name = Console.ReadLine();
            Console.Write("Lösenord: ");
            string pw = Console.ReadLine();

            Customer found = null;

            for (int i = 0; i < customers.Count; i++)
            {
                if (customers[i].Name == name)
                {
                    found = customers[i];
                }
            }

            if (found == null)
            {
                Console.WriteLine("Kunden finns inte.");
                return;
            }

            if (!found.VerifyPassword(pw))
            {
                Console.WriteLine("Fel lösenord.");
                return;
            }

            LoggedInMenu(found);
        }

        static void LoggedInMenu(Customer customer)
        {
            bool stay = true;

            while (stay)
            {
                Console.WriteLine();
                Console.WriteLine("Inloggad som: " + customer.Name);
                Console.WriteLine("1) Handla");
                Console.WriteLine("2) Visa kundvagn");
                Console.WriteLine("3) Kassa");
                Console.WriteLine("4) Visa kundinfo");
                Console.WriteLine("5) Logga ut");
                Console.Write("Välj: ");

                int choice = ReadInt();

                if (choice == 1)
                {
                    Shop(customer);
                }
                else if (choice == 2)
                {
                    ShowCart(customer);
                }
                else if (choice == 3)
                {
                    Checkout(customer);
                }
                else if (choice == 4)
                {
                    customer.ShowCustomerInfo();
                }
                else if (choice == 5)
                {
                    stay = false;
                }
                else
                {
                    Console.WriteLine("Fel val.");
                }
            }
        }

        static void Shop(Customer customer)
        {
            bool shopping = true;

            while (shopping)
            {
                Console.WriteLine();
                Console.WriteLine("=== Handla ===");

                for (int i = 0; i < products.Count; i++)
                {
                    Product p = products[i];
                    Console.WriteLine((i + 1) + ") " + p.Name + " - " + p.Price + " kr");
                }

                Console.WriteLine("0) Tillbaka");
                Console.Write("Välj produktnummer: ");
                int idx = ReadInt();

                if (idx == 0)
                {
                    shopping = false;
                }
                else if (idx < 1 || idx > products.Count)
                {
                    Console.WriteLine("Fel val.");
                }
                else
                {
                    Product chosen = products[idx - 1];
                    Console.Write("Antal: ");
                    int qty = ReadInt();

                    if (qty <= 0)
                    {
                        qty = 1;
                    }

                    for (int i = 0; i < qty; i++)
                    {
                        customer.AddToCart(chosen);
                    }

                    Console.WriteLine("La till " + qty + " st " + chosen.Name + " i kundvagnen.");
                }
            }
        }

        static void ShowCart(Customer customer)
        {
            Console.WriteLine();
            Console.WriteLine("=== Kundvagn ===");

            List<Product> cart = customer.GetCart();

            if (cart.Count == 0)
            {
                Console.WriteLine("(tom)");
            }
            else
            {
                for (int i = 0; i < cart.Count; i++)
                {
                    Product p = cart[i];
                    Console.WriteLine(p.Name + " " + p.Price + " kr");
                }

                Console.WriteLine("Total: " + customer.TotalPrice() + " kr");
            }
        }

        static void Checkout(Customer customer)
        {
            Console.WriteLine();
            Console.WriteLine("=== Kassa ===");
            Console.WriteLine("Att betala: " + customer.TotalPrice() + " kr");
            Console.Write("Vill du genomföra köpet? (j/n): ");
            string s = Console.ReadLine();

            if (s == "j" || s == "J")
            {
                customer.ClearCart();
                Console.WriteLine("Tack för ditt köp!");
            }
            else
            {
                Console.WriteLine("Köpet avbröts.");
            }
        }

        static int ReadInt()
        {
            string s = Console.ReadLine();
            int result;

            try
            {
                result = int.Parse(s);
            }
            catch
            {
                result = -1;
            }

            return result;
        }
    }
}
