using ef.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ef
{
    internal class Program
    {
        static void CreateDatabase()
        {
            using var dbcontext = new ShopContext();
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
            using var dbcontext = new ShopContext();
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
        static void InsertData()
        {
            using var dbcontext = new ShopContext();
            dbcontext.Add(new Product() {Name = "iPhone", Price = 1000,CateId = 1});
            dbcontext.Add(new Product() {Name = "Samsung", Price = 900, CateId = 1 });
            dbcontext.Add(new Product() {Name = "Ruou vang Abc", Price = 500, CateId = 2 });
            dbcontext.Add(new Product() {Name = "Nokia Xyz", Price = 600,CateId = 1});
            dbcontext.Add(new Product() { Name = "Cafe ABC", Price = 100, CateId = 2 });
            dbcontext.SaveChanges();
        }
        static void Main(string[] args)
        {
            using var dbcontext = new ShopContext();

        }
    }
}
