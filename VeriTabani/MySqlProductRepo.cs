using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;

namespace VeriTabani
{
    public class MySqlProductRepo : IProductRepo
    {

        private MySqlConnection getMySqlConnection()
        {
            string connectionString = @"server=localhost;port=3306;database=northwind;user=root;password=mysql1234";

            return new MySqlConnection(connectionString);
        }
        public List<Product> getAllProducts()
        {
            List<Product> products = null;
            using (var connection = getMySqlConnection())
            {
                try
                {
                    connection.Open();
                    //  Console.WriteLine("Baglanti saglandi");
                    string sql = "Select * from products";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    products = new List<Product>();
                    while (reader.Read())
                    {
                        products.Add(
                            new Product(
                            int.Parse(reader["id"]?.ToString()), reader[3]?.ToString(), Double.Parse(reader["list_price"]?.ToString()
                            )));
                        break;
                        // {
                        //     ProductId = int.Parse(reader["id"]?.ToString()),
                        //     ProductName = reader[3]?.ToString(),
                        //     ProductPrice = Double.Parse(reader["list_price"]?.ToString())
                        // });
                    }
                    reader.Close();

                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                    //  Console.WriteLine("Baglanti kapandi");

                }
                return products;
            }
        }

        public Product getProductById(int id)
        {
            Product product = null;
            using (var connection = getMySqlConnection())
            {
                try
                {
                    connection.Open();
                    string query = "select * from products where id =@productId";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.Add("@productId", MySqlDbType.Int32).Value = id;
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();

                    if (reader.HasRows)
                    {
                        product = new Product(int.Parse(reader["id"]?.ToString()), reader[3]?.ToString(), Double.Parse(reader["list_price"]?.ToString()));
                    }
                    reader.Close();
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }
                return product;
            }
        }

        public List<Product> FindProduct(string name)
        {
            List<Product> products = null;
            using (var connection = getMySqlConnection())
            {
                try
                {
                    connection.Open();
                    string query = "select * from products where product_name LIKE @name";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.Add("@name", MySqlDbType.String).Value = "%" + name + "%";
                    MySqlDataReader reader = command.ExecuteReader();

                    products = new List<Product>();
                    while (reader.Read())
                    {

                        products.Add(
                            new Product(int.Parse(reader["id"]?.ToString()), reader[3]?.ToString(), Double.Parse(reader["list_price"]?.ToString())
                        ));

                    }
                    reader.Close();
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }
                return products;
            }
        }

        public int count()
        {
            int count = 0;

            using (var connection = getMySqlConnection())
            {
                try
                {
                    connection.Open();
                    string query = "select count(*) from products";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        count = Convert.ToInt32(result);
                    }


                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }
                return count;
            }
        }

        public int createProduct(Product product)
        {
            using (var connection = getMySqlConnection())
            {
                int result = 0;
                try
                {
                    connection.Open();
                    string query = "insert into products (product_name,list_price,discontinued) VALUES(@name,@list_price,@discontinued)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.Add("@name", MySqlDbType.String).Value = product.ProductName;
                    command.Parameters.Add("@list_price", MySqlDbType.Decimal).Value = product.ProductPrice;
                    command.Parameters.Add("@discontinued", MySqlDbType.Bit).Value = 1;

                    result = command.ExecuteNonQuery();
                    Console.WriteLine($"{result} adet kayıt başarıyla eklendi");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }
                return result;
            }
        }

        public int updateProduct(Product product)
        {
            int result = 0;
            using (var connection = getMySqlConnection())
            {
                try
                {
                    connection.Open();
                    string query = "update products set product_name=@name,list_price=@price where id=@id";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.Add("@name", MySqlDbType.String).Value = product.ProductName;
                    command.Parameters.Add("@price", MySqlDbType.Decimal).Value = product.ProductPrice;
                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = product.ProductId;
                    result = command.ExecuteNonQuery();
                    Console.WriteLine($"{result} adet kayıt başarıyla güncellendi");
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
            return result;
        }

        public int deleteProduct(int productId)
        {
            int result = 0;
            using (var connection = getMySqlConnection())
            {
                try
                {
                    connection.Open();
                    string query = "delete from products where id=@id ";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = productId;
                    result = command.ExecuteNonQuery();
                    Console.WriteLine("Kayıt başarıyla silindi");
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
            return result;
        }
    }
}