
namespace Labb2Joseff
{
    public class Product
    {
        public string Name { get; private set; } 
        public decimal Price { get; private set; } 

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return Name + " (" + Price + " kr)";
        }
    }
}
