using RapidPayService.Entities;
using System.ComponentModel.DataAnnotations;

namespace RapidPayService.Dtos.Input
{
    public class InCardHolderDto
    {

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [MaxLength(100)]
        public string? Address { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CardHolderSince { get; set; }
    }
}
