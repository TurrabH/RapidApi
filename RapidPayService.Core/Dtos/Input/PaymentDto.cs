using System.ComponentModel.DataAnnotations;

namespace RapidPayService.Core.Dtos.Input
{
    public class PaymentDto
    {
        [Required]
        public Guid CardHolderId { get; set; }

        [Required]
        public int CardId { get; set; }

        [Required]
        public decimal PaymentAmount { get; set; }
        public string? TransactionType { get; set; }
    }
}
