using System;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace VeriTabani
{
    class Program
    {
        static void Main(String[] args)
        {
            var productManager = new ProductManager(new MySqlProductRepo());
            //  List<Product> products = productManager.getAllProducts();
            // foreach (var item in products)
            // {
            //     Console.WriteLine(item.ToString());
            // }

            // List<Product> products2 = productManager.FindProduct("Northwind");
            // foreach (var item in products2)
            // {
            //     Console.WriteLine(item.ToString());
            // }

            Product product = productManager.getProductById(1);
            //    Console.WriteLine(product.ToString());
            int productCount = productManager.count();
            // Console.WriteLine("Toplam ürün sayısı: " + productCount);
            Product productNew = new Product(ProductName: "Chocolabs Sarma", ProductPrice: 10.2);
            //  int count = productManager.createProduct(productNew);
            Product updateProduct = new Product(id: 100, ProductName: "Chocolabs Sarma", ProductPrice: 150);
            //int count = productManager.updateProduct(updateProduct);
            int count = productManager.deleteProduct(100);

        }

        static void getSqlConnection()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=SSPI";

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Baglanti kuruldu");
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
