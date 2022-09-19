﻿using CardStorageService.Data;
using CardStorageService.Models.Requests;
using CardStorageService.Models;
using CardStorageService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CardStorageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {

        #region Services

        private readonly ILogger<CardController> _logger;
        private readonly ICardRepositoryService _cardRepositoryService;

        #endregion

        #region Constructors

        public CardController(ILogger<CardController> logger,
            ICardRepositoryService cardRepositoryService)
        {
            _logger = logger;
            _cardRepositoryService = cardRepositoryService;
        }

        #endregion

        #region Pulbic Methods

        [HttpPost("create")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreateCardRequest request)
        {
            try
            {
                var cardId = _cardRepositoryService.Create(new Card
                {
                    ClientId = request.ClientId,
                    CardNo = request.CardNo,
                    ExpDate = request.ExpDate,
                    CVV2 = request.CVV2
                });
                return Ok(new CreateCardResponse
                {
                    CardId = cardId.ToString()
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Create card error.");
                return Ok(new CreateCardResponse
                {
                    ErrorCode = 1012,
                    ErrorMessage = "Create card error."
                });
            }
        }

        [HttpGet("get-by-client-id")]
        [ProducesResponseType(typeof(GetCardsResponse), StatusCodes.Status200OK)]
        public IActionResult GetByClientId([FromQuery] string clientId)
        {
            try
            {
                var cards = _cardRepositoryService.GetByClientId(clientId);
                return Ok(new GetCardsResponse
                {
                    Cards = cards.Select(card => new CardDto
                    {
                        CardNo = card.CardNo,
                        CVV2 = card.CVV2,
                        Name = card.Name,
                        ExpDate = card.ExpDate.ToString("MM/yy")
                    }).ToList()
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get cards error.");
                return Ok(new GetCardsResponse
                {
                    ErrorCode = 1013,
                    ErrorMessage = "Get cards error."
                });
            }
        }

        [HttpGet("getall")]
        [ProducesResponseType(typeof(CreateClientResponse), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            try
            {
                var clients = _cardRepositoryService.GetAll();
                return Ok(clients);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Read cards error.");
                return Ok(new CreateCardResponse
                {
                    ErrorCode = 1014,
                    ErrorMessage = "Read cards error."
                });
            }
        }

        [HttpDelete("del")]
        [ProducesResponseType(typeof(CreateClientResponse), StatusCodes.Status200OK)]
        public IActionResult Delete(string cardId)
        {
            try
            {
                var card = _cardRepositoryService.Delete(cardId);

                if (card == -1)
                {
                    _logger.LogError(0, "Card with id {0} not found.", cardId);
                    return Ok(new CreateCardResponse
                    {
                        ErrorCode = 1015,
                        ErrorMessage = String.Format("Card with id {0} not found.", cardId)
                    });
                }
                return Ok(cardId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Read card with id {0} error.", cardId);
                return Ok(new CreateCardResponse
                {
                    ErrorCode = 1016,
                    ErrorMessage = String.Format("Read card with id {0} error.", cardId)
                });
            }
        }

        [HttpGet("getbyid")]
        [ProducesResponseType(typeof(CreateClientResponse), StatusCodes.Status200OK)]
        public IActionResult GetById(string cardId)
        {
            try
            {
                var card = _cardRepositoryService.GetById(cardId);

                if (card == null)
                {
                    _logger.LogError(0, "Card with id {0} not found.", cardId);
                    return Ok(new CreateCardResponse
                    {
                        ErrorCode = 1017,
                        ErrorMessage = String.Format("Card with id {0} not found.", cardId)
                    });
                }
                return Ok(card);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Read card with id {0} error.", cardId);
                return Ok(new CreateCardResponse
                {
                    ErrorCode = 1018,
                    ErrorMessage = String.Format("Read card with id {0} error.", cardId)
                });
            }
        }

        [HttpPatch("update")]
        [ProducesResponseType(typeof(CreateClientResponse), StatusCodes.Status200OK)]
        public IActionResult Update([FromBody] Card request)
        {
            try
            {
                var result = _cardRepositoryService.Update(request);

                if (result == -1)
                {
                    _logger.LogError(0, "Card with id {0} not found.", request.ClientId);
                    return Ok(new CreateCardResponse
                    {
                        ErrorCode = 1019,
                        ErrorMessage = String.Format("Card with id {0} not found.", request.ClientId)
                    });
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Read card with clientid {0} error.", request.ClientId);
                return Ok(new CreateCardResponse
                {
                    ErrorCode = 1020,
                    ErrorMessage = String.Format("Read card with clientid {0} error.", request.ClientId)
                });
            }
        }

        #endregion



    }
}
