using Microsoft.EntityFrameworkCore;
using _2022E_WebApp.Entities;

namespace _2022E_WebApp
{
    public class AppDbContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }

        public DbSet<User> Users { get; set; }  

        public string DbPath { get; set; }

        public AppDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "WebAppDb");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
        }
    }
}
