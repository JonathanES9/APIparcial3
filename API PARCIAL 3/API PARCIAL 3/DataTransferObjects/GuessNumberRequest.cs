using System.ComponentModel.DataAnnotations;

namespace API_PARCIAL_3.DataTransferObjects
{
    public class GuessNumberRequest
    {
        [Required(ErrorMessage = "El ID del juego es obligatorio.")]
        public int GameId { get; set; }

        [Required(ErrorMessage = "El número intentado es obligatorio.")]
        [RegularExpression(@"^(?!.*(.).*\1)\d{4}$", ErrorMessage = "El número debe tener 4 dígitos únicos.")]
        public string AttemptedNumber { get; set; } = string.Empty;
    }
}
