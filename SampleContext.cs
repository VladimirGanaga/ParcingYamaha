using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcingYamaha
{
    public class SampleContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=WIN10\\SQLEXPRESS;Database=Yamaha;Trusted_Connection=True;TrustServerCertificate=true;");
        }


        public SampleContext()
        {
            //Database.EnsureDeleted();   // удаляем бд со старой схемой
            //Database.EnsureCreated();   // создаем бд с новой схемой
        }

        public DbSet<Modeldatacollection> Modeldatacollection { get; set; }
        public DbSet<Partsdatacollection> Partsdatacollection { get; set; } = null!;

    }
}
