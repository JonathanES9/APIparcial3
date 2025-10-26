using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_PARCIAL_3.Models
{
        
        public class Game
        {
            public int Id { get; set; } 
            [Required] 
            public string SecretNumber { get; set; }

            public DateTime CreateAt { get; set; }

            public bool IsFinished { get; set; }

            
            // 1. Esta línea crea la columna "PlayerId" en tu tabla Games.
            public int PlayerId { get; set; }

            
            //  Esta línea le dice a Entity Framework cómo conectar clase 'Game' con la clase 'Player' de tu compañero.
            [ForeignKey("PlayerId")]
            public Player Player { get; set; }
        }
    
}
