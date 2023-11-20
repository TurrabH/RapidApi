using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RapidPayService.EntityFramework.Entities
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [ForeignKey("CardId")]
        public int CardId { get; set; }
        public Card Card { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? TransactionDate { get; set; }

        [Required]
        [MaxLength(10)]
        public string TransactionType { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TransactionAmount { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal NewBalance { get; set; }
    }
}
