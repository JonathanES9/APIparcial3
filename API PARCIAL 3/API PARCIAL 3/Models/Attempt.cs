using System.ComponentModel.DataAnnotations;

namespace API_PARCIAL_3.Models
{
/// <summary>
/// Intento realizado por el jugador en un juego específico.
/// </summary>
    public class Attempt
    {
        public int AttemptId { get; set; }

        [Required]
        public int GameId { get; set; }

        public Game Game { get; set; } = null!;

        [Required]
        [StringLength(4)]
        public string AttemptedNumber { get; set; } = string.Empty;

        public DateTime AttemptedAt { get; set; } = DateTime.UtcNow;

        public string ResultMessage { get; set; } = string.Empty;
    }
}
