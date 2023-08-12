using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeriTabani
{
    public class Product
    {
        public Product(string ProductName, double ProductPrice)
        {
            this.ProductName = ProductName;
            this.ProductPrice = ProductPrice;
        }
        public Product(int id, string ProductName, double ProductPrice) : this(ProductName, ProductPrice)
        {
            this.ProductId = id;
        }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public double ProductPrice { get; set; }

        public override String ToString()
        {
            return $"name: {this.ProductName}, productPrice: {this.ProductPrice}";
        }

    }
}