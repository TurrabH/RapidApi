using System.ComponentModel.DataAnnotations;

namespace RapidPayService.Entities
{
    public class CardHolder
    {
        [Key]
        public Guid CardHolderId { get; set; }

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

        public ICollection<Card> Cards { get; set; }
    }
}
