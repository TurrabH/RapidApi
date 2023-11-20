

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidPayService.EntityFramework.Entities
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }

        [Required]
        [StringLength(15)]
        public string CardNumber { get; set; }

        [Required]
        [ForeignKey("CardHolderId")]
        public Guid CardHolderId { get; set; }
        public CardHolder CardHolder { get; set; }

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

        public ICollection<Transaction> Transactions { get; set; }
    }


}
