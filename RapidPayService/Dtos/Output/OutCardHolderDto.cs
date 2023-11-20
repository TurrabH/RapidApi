using System.ComponentModel.DataAnnotations;

namespace RapidPayService.Dtos.Output
{
    public class OutCardHolderDto
    {
        public Guid CardHolderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime CardHolderSince { get; set; }
    }
}
