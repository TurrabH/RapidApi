using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RapidPayService.Core.Dtos.Input
{ 
    public class InCardDto
    {
        [Required]
        public Guid CardHolderId { get; set; }

        [Required]
        [StringLength(15)]
        public string CardNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

        [Required]
        [StringLength(3)]
        public string CVV { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreationDate { get; set; }
    }
}
