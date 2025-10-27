



using Microsoft.EntityFrameworkCore; 
using API_PARCIAL_3.Models;  

namespace API_PARCIAL_3.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {
        }

       public DbSet<Player> players { get; set; }
        public DbSet<Game> Games { get; set; }
        
      
    }
}