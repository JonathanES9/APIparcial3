using System.ComponentModel.DataAnnotations; 

namespace API_PARCIAL_3.DataTransferObjects 
{
    public class RegisterPlayerRequest
    {
        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "La edad es obligatoria.")] // Ver si se necesita que sea Required
        public int Age { get; set; }
    }
}
