﻿using CardStorageService.Models.Requests;
using CardStorageServiceProtos;
using Grpc.Core;
using static CardStorageServiceProtos.ClientService;

namespace CardStorageService.Services.Impl
{
    public class ClientService : ClientServiceBase
    {
        #region Services

        private readonly IClientRepositoryService _clientRepositoryService;

        #endregion


        public ClientService(IClientRepositoryService clientRepositoryService)
        {
            _clientRepositoryService = clientRepositoryService;
        }

        public override Task<CreateClientResponse> Create(CreateClientRequest request, ServerCallContext context)
        {
            var clientId = _clientRepositoryService.Create(new Data.Client
            {
                FirstName = request.FirstName,
                Surname = request.SurName,
                Patronymic = request.Patronymic
            });

            var response = new CreateClientResponse
            {
                ClientId = clientId
            };

            return Task.FromResult(response);
        }

    }
}
