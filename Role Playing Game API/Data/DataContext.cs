using Microsoft.EntityFrameworkCore;
using Role_Playing_Game_API.Models;

namespace Role_Playing_Game_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }
        
        public DbSet<Character> Characters { get; set; }
    }
}
