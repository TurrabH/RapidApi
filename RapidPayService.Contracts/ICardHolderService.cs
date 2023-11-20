
using RapidPayService.Core.Dtos.Input;
using RapidPayService.Core.Dtos.Output;

namespace RapidPayService.Contracts
{
    public interface ICardHolderService
    {
        Task<OutCardHolderDto> CreateHolder(InCardHolderDto cardHolderDto);
        Task<OutCardDto> CreateCard(InCardDto inCardDto);
        Task<OutTransactionDto> Pay(PaymentDto paymentDto);
        Task<decimal> GetBalance(BalanceDto balanceDto);
    }
}
