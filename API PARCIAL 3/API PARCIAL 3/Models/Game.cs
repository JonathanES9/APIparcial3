using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_PARCIAL_3.Models
{
        
        public class Game
        {
            [Required]
            public int GameId { get; set; } 
             
            public string SecretNumber { get; set; } = string.Empty;

            public DateTime CreateAt { get; set; }

            public bool IsFinished { get; set; } = false;

            
            public int PlayerId { get; set; }
        }
    
}
