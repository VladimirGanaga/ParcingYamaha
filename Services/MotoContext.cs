using Microsoft.EntityFrameworkCore;
using ParcingYamaha.Dtos;

namespace ParcingYamaha.Services
{
    /// <summary>
    /// Класс контекста для работы Entity framework
    /// </summary>
    public class MotoContext : DbContext
    {  
        public MotoContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ModelsDB> ModelDB { get; set; }
        public DbSet<ChaptersDB> ChapterDB { get; set; } = null!;
        public DbSet<PartsDB> PartDB { get; set; } = null!;

    }
}
