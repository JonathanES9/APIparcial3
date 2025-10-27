using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_PARCIAL_3.Models
{
        
        public class Game
        {
            [Required]
            public int GameId { get; set; } 
             
            public int SecretNumber { get; set; }

            public DateTime CreateAt { get; set; }

            public bool IsFinished { get; set; }
        }
    
}
