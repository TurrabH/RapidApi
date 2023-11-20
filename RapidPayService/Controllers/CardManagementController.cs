using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPayService.Contracts;
using RapidPayService.Core.Dtos.Input;
using RapidPayService.Core.Dtos.Output;

namespace RapidPayService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CardManagementController : ControllerBase
    {
        public ICardHolderService _cardHolderService;
        private readonly ILogger<CardManagementController> _logger;
        public CardManagementController(ICardHolderService cardHolderService, ILogger<CardManagementController> logger)
        {
            _cardHolderService = cardHolderService;
            _logger = logger;
        }

        [HttpPost]
        [Route("cardHolder")]
        [ProducesResponseType(typeof(OutCardHolderDto), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateCardHolder([FromBody] InCardHolderDto cardHolderDto)
        {
            _logger.LogInformation("Attempting to create a card holder");
            var response = await _cardHolderService.CreateHolder(cardHolderDto);
            return Ok(response);
        }

        [HttpPost]
        [Route("card")]
        [ProducesResponseType(typeof(OutCardDto), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateCard([FromBody] InCardDto inCardDto)
        {
            _logger.LogInformation("Attempting to create a card holder");
            var response = await _cardHolderService.CreateCard(inCardDto);
            return Ok(response);
        }

        [HttpPost]
        [Route("pay")]
        [ProducesResponseType(typeof(OutTransactionDto), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Pay(PaymentDto paymentDto)
        {
            _logger.LogInformation("Attempting start a transaction");
            var response = await _cardHolderService.Pay(paymentDto);
            return Ok(response);
        }

        [HttpGet]
        [Route("balance")]
        [ProducesResponseType(typeof(decimal), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetBalance([FromBody] BalanceDto balanceDto)
        {
            _logger.LogInformation("Attempting get balance");
            var response = await _cardHolderService.GetBalance(balanceDto);
            return Ok(response);
        }
    }
}
