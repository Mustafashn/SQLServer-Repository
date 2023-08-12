using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeriTabani
{
    public interface IProductRepo
    {
        List<Product> getAllProducts();
        Product getProductById(int id);
        List<Product> FindProduct(string name);
        int count();
        int createProduct(Product product);
        int updateProduct(Product product);
        int deleteProduct(int productId);

    }
}