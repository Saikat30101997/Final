using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    public class Product : IProduct
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double? Discount { get; set; }

        public Product()
        {

        }

        public Product(string name, double price, double? discount)
        {

        }

    }
}
