



using Microsoft.EntityFrameworkCore; 
using API_PARCIAL_3.Models;  
using System.Collections.Generic;

namespace API_PARCIAL_3.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {
        }

       
        public DbSet<Game> Games { get; set; }

      
    }
}