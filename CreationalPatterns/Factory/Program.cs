using System;

namespace Factory
{
    class Program
    {
        static void Main(string[] args)
        {
            var name = "Camera";
            var price = 2000;
            double? discount = 20;

            IProduct product = ProductFactory.CreateProduct<Stationary>(name, price, discount);

            Console.WriteLine(product.Price);
            Console.WriteLine(product.GetType());
        }
    }
}
