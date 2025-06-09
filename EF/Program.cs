using ef.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel;
using System.Net.NetworkInformation;

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
            dbcontext.Add(new Category() {Name = "Dien thoai",Description="Cac loai dien thoai"});
            dbcontext.Add(new Category() { Name = "Do uong", Description = "Cac loai do uong" });
            dbcontext.Add(new Product() {Name = "iPhone", Price = 1000,CateId = 1});
            dbcontext.Add(new Product() {Name = "Samsung", Price = 900, CateId = 1 });
            dbcontext.Add(new Product() {Name = "Ruou vang Abc", Price = 500, CateId = 2 });
            dbcontext.Add(new Product() {Name = "Nokia Xyz", Price = 600,CateId = 1});
            dbcontext.Add(new Product() { Name = "Cafe ABC", Price = 100, CateId = 2 });
            dbcontext.Add(new Product() { Name = "Nuoc ngot", Price = 50, CateId = 2 });
            dbcontext.Add(new Product() { Name = "Bia", Price = 200, CateId = 2 });
            dbcontext.SaveChanges();
        }
        static void Main(string[] args)
        {
            //DropDatabase();
            //CreateDatabase();
            //InsertData();
            using var dbcontext = new ShopContext();
            var kq = from p in dbcontext.products
                     join c in dbcontext.categories on p.CateId equals c.CategoryId
                     select new
                     {
                         ten = p.Name,
                         danhmuc = c.Name,
                         gia = p.Price
                     };
            kq.ToList().ForEach(abc => Console.WriteLine(abc));
        }
    }
}
