using System; // Para poder usar DateTime
using System.ComponentModel.DataAnnotations; // Para las validaciones [Key] y [Required]

namespace APIPARCIAL3.Models
{
    public class Player
    {
        [Key] // Le dice a Entity Framework que esta es la Primary Key
        public int PlayerId { get; set; }

        [Required] // Validación: este campo es obligatorio
        [MaxLength(50)] // Validación: máximo 50 caracteres
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public required string LastName { get; set; }

        [Required]
        public required int Age { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}