using Microsoft.EntityFrameworkCore;
using ParcingYamaha.ClassesDB;




namespace ParcingYamaha
{
    /// <summary>
    /// Класс контекста для работы Entity framework
    /// </summary>
    public class MotoContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=WIN10\\SQLEXPRESS;Database=Yamaha;Trusted_Connection=True;TrustServerCertificate=true;");
        }


        public MotoContext()
        {
            //Database.EnsureDeleted();   // удаляем бд со старой схемой
            //Database.EnsureCreated();   // создаем бд с новой схемой
        }

        public DbSet<ModelsDB> ModelDB { get; set; }
        public DbSet<ChaptersDB> ChapterDB { get; set; } = null!;
        public DbSet<PartsDB> PartDB { get; set; } = null!;

    }
}
