using EFMigration.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationExample.Model
{
    public class WebContext: DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        private const string connectionString = "Server=DESKTOP-6FO9FCK\\SQLEXPRESS;Initial Catalog = webdb;Trusted_Connection=yes;TrustServerCertificate=True";
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>{
            builder.AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Warning)
                   .AddFilter(DbLoggerCategory.Query.Name, LogLevel.Debug)
                   .AddConsole();
          }
        );
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.UseLoggerFactory(loggerFactory);
        }
    }
}
