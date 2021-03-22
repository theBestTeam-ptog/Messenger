using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Helpers;
using AutoMapper;
using Domain.Mappers;
using Domain.Models;
using Domain.Repositories;
using Domain.Repositories.Users;
using Grpc.Core;
using Messenger.ChatService.Protos;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using User = Domain.Models.User;

namespace ChatService
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly IMapper _mapper;
        private readonly UserRepository _repository;

        public GreeterService(ILogger<GreeterService> logger, IMapper mapper, UserRepository repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        public override async Task<PickUpUserReply> TakeUser(PickUpUser request, ServerCallContext context)
        {
            var user = await _repository.GetUserValidation(request.Login);
            return new PickUpUserReply{User = _mapper.Map<Messenger.ChatService.Protos.User>(user)};
        }
    }
}