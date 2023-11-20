using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RapidPayService.Core.Dtos.Output
{
    public class OutCardDto
    {
        public int CardId { get; set; }
        public string CardNumber { get; set; }
        public Guid CardHolderId { get; set; }
        public OutCardHolderDto CardHolder { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CVV { get; set; }
        public decimal Balance { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
