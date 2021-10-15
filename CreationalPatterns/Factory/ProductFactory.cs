using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    public class ProductFactory
    {
        public static IProduct CreateProduct<T>(string name, double price, double? discount)
            where T : IProduct, new()
        {
            IProduct product;
            product = new T();

            product.Name = name;

            if (discount.HasValue)
                product.Price = price * discount.Value / 100.0;
            else
                product.Price = price;

            return product;
        }
    }
}
