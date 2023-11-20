using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RapidPayService.Contracts;
using RapidPayService.Core.Dtos.Input;
using RapidPayService.Core.Dtos.Output;
using RapidPayService.EntityFramework.Data;
using RapidPayService.EntityFramework.Entities;
using RapidPayService.Helpers;
using RapidPayService.Models;

namespace RapidPayService.Services
{
    public class CardHolderService : ICardHolderService
    {
        private readonly IMapper _mapper;
        private readonly RapidPayDbContext _rapidPayDbContext;
        private readonly ILogger<CardHolderService> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly UFEService _uFEService;

        public CardHolderService(RapidPayDbContext rapidPayDbContext, IMapper mapper, UFEService uFEService, ILogger<CardHolderService> logger, UserManager<IdentityUser> userManager)
        {
            _rapidPayDbContext = rapidPayDbContext;
            _mapper = mapper;
            _uFEService = uFEService;
            _logger = logger;
            _userManager = userManager;
        }
        public async Task<OutCardHolderDto> CreateHolder(InCardHolderDto cardHolderDto)
        {
            _logger.LogDebug("Attempting to create holder - CardHolderService");
            var cardHolder = _mapper.Map<InCardHolderDto, CardHolder>(cardHolderDto);
            await _rapidPayDbContext.CardHolders.AddAsync(cardHolder);
            await _rapidPayDbContext.SaveChangesAsync();
            var outCardHolder = _mapper.Map<CardHolder, OutCardHolderDto>(cardHolder);
            _logger.LogDebug($"New Card Holder created with email {outCardHolder.Email} - CardHolderService");
            return outCardHolder;
        }

        public async Task<OutCardDto> CreateCard(InCardDto inCardDto)
        {
            _logger.LogDebug("Attempting to create card - CardHolderService");
            var cardHolder = await _rapidPayDbContext.CardHolders.SingleOrDefaultAsync(holder => holder.CardHolderId == inCardDto.CardHolderId);
            if (ObjectHelpers.IsAnyNull(inCardDto.CardHolderId, cardHolder) || inCardDto.CardHolderId == Guid.Empty)
            {
                string errorMessage = "Card Holder not found!";
                _logger.LogWarning(errorMessage);
                throw new CustomException()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = errorMessage
                };
            }
                
            var card = _mapper.Map<InCardDto, Card>(inCardDto);
            await _rapidPayDbContext.Cards.AddAsync(card);
            await _rapidPayDbContext.SaveChangesAsync();
            var outCard = _mapper.Map<Card, OutCardDto>(card);
            _logger.LogInformation($"New Card created for card holder {outCard.CardHolderId} - CardHolderService");
            return outCard;
        }

        public async Task<OutTransactionDto> Pay(PaymentDto paymentDto)
        {
            if (paymentDto.PaymentAmount < 1 && paymentDto.PaymentAmount == null)
            {
                string errorMessage = "Payment amount is not valid!";
                _logger.LogWarning(errorMessage);
                throw new CustomException()
                {
                    StatusCode = StatusCodes.Status204NoContent,
                    ErrorMessage = "Payment amount is not valid!"
                };
            }
                
            if (ObjectHelpers.IsAnyNull(paymentDto.CardId, paymentDto.CardHolderId) || paymentDto.CardHolderId == Guid.Empty)
            {
                string errorMessage = "Card Holder or Card is Empty!";
                _logger.LogWarning(errorMessage);
                throw new CustomException()
                {
                    StatusCode = StatusCodes.Status204NoContent,
                    ErrorMessage = errorMessage
                };
            }
               
            var cardHolder = await _rapidPayDbContext.CardHolders.Include(holder => holder.Cards).SingleOrDefaultAsync(holder => holder.CardHolderId == paymentDto.CardHolderId);
            if (cardHolder == null)
            {
                string errorMessage = "Card Holder not found!";
                _logger.LogWarning(errorMessage);
                throw new CustomException()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = errorMessage
                };
            }
                
            var card = cardHolder.Cards.Where(card => card.CardId == paymentDto.CardId).FirstOrDefault();
            if (card == null)
            {
                string errorMessage = "Card for Card Holder not found!";
                _logger.LogWarning(errorMessage);
                throw new CustomException()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = errorMessage
                };
            }
                
            var newBalance = card.Balance - (paymentDto.PaymentAmount + _uFEService.FeeAmount);
            if (card.Balance - paymentDto.PaymentAmount < 0)
            {
                string errorMessage = "Insufficient Balance!";
                throw new CustomException()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = errorMessage
                };
            }
                
            var transaction = new Transaction()
            {
                CardId = paymentDto.CardId,
                Card = card,
                TransactionDate = DateTime.Now,
                TransactionType = paymentDto.TransactionType ?? "Debit",
                TransactionAmount = paymentDto.PaymentAmount,
                NewBalance = newBalance
            };
            _rapidPayDbContext.Transactions.Add(transaction);
            card.Balance = newBalance;
            await _rapidPayDbContext.SaveChangesAsync();
            var outTransaction = _mapper.Map<Transaction, OutTransactionDto>(transaction);
            _logger.LogInformation($"A transaction occured for CardID - {outTransaction.CardId} - CardHolderService");
            return outTransaction;
        }

        public async Task<decimal> GetBalance(BalanceDto balanceDto)
        {
            if (ObjectHelpers.IsAnyNull(balanceDto.CardId, balanceDto.CardHolderId) || balanceDto.CardHolderId == Guid.Empty)
            {
                string errorMessage = "Card Holder or Card is Empty!";
                _logger.LogWarning(errorMessage);
                throw new CustomException()
                {
                    StatusCode = StatusCodes.Status204NoContent,
                    ErrorMessage = errorMessage
                };
            }
               
            var cardHolder = await _rapidPayDbContext.CardHolders.Include(holder => holder.Cards).SingleOrDefaultAsync(holder => holder.CardHolderId == balanceDto.CardHolderId);
            if (cardHolder == null)
            {
                string errorMessage = "Card Holder not found!";
                _logger.LogWarning(errorMessage);
                throw new CustomException()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = errorMessage
                };
            }
                
            var card = cardHolder.Cards.Where(card => card.CardId == balanceDto.CardId).FirstOrDefault();
            if (card == null)
            {
                string errorMessage = "Card for Card Holder not found!";
                _logger.LogWarning(errorMessage);
                throw new CustomException()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = errorMessage
                };
            }
               
            return card.Balance;
        }
    }
}
