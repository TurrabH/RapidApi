using RapidPayService.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RapidPayService.Dtos.Output
{
    public class OutTransactionDto
    {
        public int TransactionId { get; set; }
        public int CardId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal NewBalance { get; set; }
    }
}
