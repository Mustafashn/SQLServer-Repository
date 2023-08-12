using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeriTabani
{
    public class ProductManager : IProductRepo
    {
        IProductRepo _productRepo;
        public ProductManager(IProductRepo productRepo)
        {
            this._productRepo = productRepo;
        }

        public int count()
        {
            return this._productRepo.count();
        }


        public int createProduct(Product product)
        {
            return this._productRepo.createProduct(product);
        }


        public int deleteProduct(int productId)
        {
            return this._productRepo.deleteProduct(productId);
        }

        public List<Product> FindProduct(string name)
        {
            return this._productRepo.FindProduct(name);
        }

        public List<Product> getAllProducts()
        {
            return this._productRepo.getAllProducts();
        }

        public Product getProductById(int id)
        {
            return this._productRepo.getProductById(id);
        }

        public int updateProduct(Product product)
        {
            return this._productRepo.updateProduct(product);
        }
    }
}
