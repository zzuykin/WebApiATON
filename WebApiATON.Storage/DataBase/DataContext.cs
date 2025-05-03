
using Microsoft.EntityFrameworkCore;
using WebApiATON.Storage.Models;

namespace WebApiATON.Storage.DataBase
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Data Source=MydataBase.db");
        //}
    }
}
