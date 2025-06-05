using ef.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ef
{
    internal class Program
    {
        static void CreateDatabase()
        {
            using var dbcontext = new ProductDbContext();
            string dbname = dbcontext.Database.GetDbConnection().Database;
            var kq = dbcontext.Database.EnsureCreated();
            if (kq)
            {
                Console.WriteLine($"Tao db {dbname} thanh cong");
            }
            else
            {
                Console.WriteLine($"Khong tao duoc {dbname}");
            }
        }
        static void DropDatabase()
        {
            using var dbcontext = new ProductDbContext();
            string dbname = dbcontext.Database.GetDbConnection().Database;
            var kq = dbcontext.Database.EnsureDeleted();
            if (kq)
            {
                Console.WriteLine($"Xoa db {dbname} thanh cong");
            }
            else
            {
                Console.WriteLine($"Khong xoa duoc {dbname}");
            }
        }
        static void InsertProduct()
        {
            using var dbcontext = new ProductDbContext();
            /*
             -Model (Product)
             -Add,AddAsyc
             -SaveChanges
             */
            var products = new object[]
            {
                new Product() {ProductName = "San pham 3", Provider = "CTY A"},
                new Product() {ProductName = "San pham 4", Provider = "CTY B"},
                new Product() {ProductName = "San pham 5", Provider = "CTY C"},

            };
            dbcontext.AddRange(products);
            int number_rows = dbcontext.SaveChanges();
            Console.WriteLine($"Da chen {number_rows} du lieu");
        }
        static void ReadProducts()
        {
            using var dbcontext = new ProductDbContext();
            //LinQ
            
            var products = dbcontext.products.ToList();
            products.ForEach(product => product.PrintInfo());
            


            //var qr = from product in dbcontext.products
            //         where product.Provider.Contains("CTY")
            //         orderby product.ProductId descending
            //         select product;
            //qr.ToList().ForEach(product => product.PrintInfo());
            //Product product = (from p in dbcontext.products
            //                   where p.Provider == "CTY A"
            //                   select p).FirstOrDefault();
            //if (product != null)
            //{
            //    product.PrintInfo();
            //}
        }
        static void RenameProduct(int id, string newName)
        {
            using var dbcontext = new ProductDbContext();
            Product product = (from p in dbcontext.products
                               where p.ProductId == id
                               select p).FirstOrDefault();
            if (product != null)
            {
                // product -> DbContext
                product.ProductName = newName;
                int number_rows = dbcontext.SaveChanges();
                Console.WriteLine($"Da cap nhat {number_rows} du lieu");
            }
        }
        static void DeleteProduct(int id)
        {
            using var dbcontext = new ProductDbContext();
            Product product = (from p in dbcontext.products
                               where p.ProductId == id
                               select p).FirstOrDefault();
            if (product != null)
            {
                dbcontext.Remove(product);
                int number_rows = dbcontext.SaveChanges();
                Console.WriteLine($"Da xoa {number_rows} du lieu");
            }
        }
        static void Main(string[] args)
        {
            // Logging - 
            DeleteProduct(4);
        }
    }
}
