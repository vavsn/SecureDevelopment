using AutoMapper;
using CardStorageService.Data;
using CardStorageService.Models;
using CardStorageService.Models.Requests;
using CardStorageService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardStorageService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        #region Services

        private readonly IClientRepositoryService _clientRepositoryService;
        private readonly ILogger<ClientController> _logger;
        private readonly IMapper _mapper;

        #endregion


        #region Constructors

        public ClientController(
            ILogger<ClientController> logger,
            IClientRepositoryService clientRepositoryService,
            IMapper mapper)
        {
            _logger = logger;
            _clientRepositoryService = clientRepositoryService;
            _mapper = mapper;
        }

        #endregion

        #region METHODS

        [HttpPost("create")]
        [ProducesResponseType(typeof(CreateClientResponse), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreateClientRequest request)
        {
            try
            {
                var clientId = _clientRepositoryService.Create(_mapper.Map<Client>(request));
                return Ok(new CreateClientResponse
                {
                    ClientId = clientId
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Create client error.");
                return Ok(new CreateCardResponse
                {
                    ErrorCode = 912,
                    ErrorMessage = "Create client error."
                });
            }
        }

        [HttpGet("getall")]
        [ProducesResponseType(typeof(CreateClientResponse), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            try
            {
                var clients = _clientRepositoryService.GetAll();
                return Ok(clients);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Read clients error.");
                return Ok(new CreateCardResponse
                {
                    ErrorCode = 913,
                    ErrorMessage = "Read clients error."
                });
            }
        }

        [HttpGet("getbyid")]
        [ProducesResponseType(typeof(CreateClientResponse), StatusCodes.Status200OK)]
        public IActionResult GetById(string clientId)
        {
            try
            {
                var client = _clientRepositoryService.GetById(Int32.Parse(clientId));

                if (client == null)
                {
                    _logger.LogError(0, "Client with id {0} not found.", clientId);
                    return Ok(new CreateCardResponse
                    {
                        ErrorCode = 915,
                        ErrorMessage = String.Format("Client with id {0} not found.", clientId)
                    });
                }


                return Ok(new GetClientResponse
                {
                    Client = _mapper.Map<ClientDto>(client)
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Read client with id {0} error.", clientId);
                return Ok(new CreateCardResponse
                {
                    ErrorCode = 914,
                    ErrorMessage = String.Format("Read client with id {0} error.", clientId)
                });
            }
        }

        [HttpDelete("del")]
        [ProducesResponseType(typeof(CreateClientResponse), StatusCodes.Status200OK)]
        public IActionResult Delete(string clientId)
        {
            try
            {
                var client = _clientRepositoryService.Delete(Int32.Parse(clientId));

                if (client == -1)
                {
                    _logger.LogError(0, "Client with id {0} not found.", clientId);
                    return Ok(new CreateCardResponse
                    {
                        ErrorCode = 917,
                        ErrorMessage = String.Format("Client with id {0} not found.", clientId)
                    });
                }
                return Ok(client);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Read client with id {0} error.", clientId);
                return Ok(new CreateCardResponse
                {
                    ErrorCode = 916,
                    ErrorMessage = String.Format("Read client with id {0} error.", clientId)
                });
            }
        }

        [HttpPatch("update")]
        [ProducesResponseType(typeof(CreateClientResponse), StatusCodes.Status200OK)]
        public IActionResult Update([FromBody] Client request)
        {
            try
            {
                var result = _clientRepositoryService.Update(request);

                if (result == -1)
                {
                    _logger.LogError(0, "Client with id {0} not found.", request.ClientId);
                    return Ok(new CreateCardResponse
                    {
                        ErrorCode = 919,
                        ErrorMessage = String.Format("Client with id {0} not found.", request.ClientId)
                    });
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Read client with id {0} error.", request.ClientId);
                return Ok(new CreateCardResponse
                {
                    ErrorCode = 918,
                    ErrorMessage = String.Format("Read client with id {0} error.", request.ClientId)
                });
            }
        }

        #endregion

    }
}
