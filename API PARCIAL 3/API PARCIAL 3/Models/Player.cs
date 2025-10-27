using System.ComponentModel.DataAnnotations; 
namespace API_PARCIAL_3.Models
{
    public class Player
    {
        [Key] 
        public int PlayerId { get; set; }

        [Required] 
        [MaxLength(50)] 
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public required string LastName { get; set; }

        [Required]
        public required int Age { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}