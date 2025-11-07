
namespace Labb2Joseff
{
    public class Customer
    {
        public string Name { get; private set; }
        private string Password;
        private List<Product> Cart = new List<Product>();

        public Customer(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public bool VerifyPassword(string input)
        {
            return input == Password;
        }

        public void AddToCart(Product p)
        {
            Cart.Add(p);
        }

        public void ClearCart()
        {
            Cart.Clear();
        }

        public decimal TotalPrice()
        {
            decimal total = 0;
            for (int i = 0; i < Cart.Count; i++)
            {
                total = total + Cart[i].Price;
            }
            return total;
        }

        public List<Product> GetCart()
        {
            return Cart;
        }

        public void ShowCustomerInfo()
        {
            Console.WriteLine("Kund: " + Name);
            Console.WriteLine("Lösenord: " + Password);
            Console.WriteLine("Kundvagn:");

            if (Cart.Count == 0)
            {
                Console.WriteLine("(tom)");
            }
            else
            {
                for (int i = 0; i < Cart.Count; i++)
                {
                    Product p = Cart[i];
                    Console.WriteLine(p.Name + " " + p.Price + " kr");
                }
            }

            Console.WriteLine("Total: " + TotalPrice() + " kr");
        }
    }
}
