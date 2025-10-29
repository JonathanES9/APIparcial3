using System.ComponentModel.DataAnnotations; 

namespace API_PARCIAL_3.DataTransferObjects 
{
    public class RegisterPlayerRequest
    {
        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "La edad es obligatoria.")]
        [Range(5, 120, ErrorMessage = "La edad debe ser entre 5 y 120 años")]
        public int Age { get; set; }
    }
}
