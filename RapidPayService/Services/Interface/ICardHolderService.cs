using Microsoft.AspNetCore.Mvc;
using RapidPayService.Dtos.Input;
using RapidPayService.Dtos.Output;

namespace RapidPayService.Services.Interface
{
    public interface ICardHolderService
    {
        Task<OutCardHolderDto> CreateHolder(InCardHolderDto cardHolderDto);
        Task<OutCardDto> CreateCard(InCardDto inCardDto);
        Task<OutTransactionDto> Pay(PaymentDto paymentDto);
        Task<decimal> GetBalance(BalanceDto balanceDto);
    }
}
