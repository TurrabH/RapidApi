using System.ComponentModel.DataAnnotations;

namespace RapidPayService.Dtos.Input
{
    public class BalanceDto
    {
        [Required]
        public Guid CardHolderId { get; set; }

        [Required]
        public int CardId { get; set; }
    }
}
