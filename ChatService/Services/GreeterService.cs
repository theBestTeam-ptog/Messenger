using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Helpers;
using AutoMapper;
using Domain.DbModels;
using Domain.Mappers;
using Domain.Models;
using Domain.Repositories;
using Domain.Repositories.Users;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Messenger.ChatService.Protos;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Chat = Domain.Models.Chat;
using User = Domain.Models.User;

namespace ChatService
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly IMapper _mapper;
        private readonly Repository _repository;

        public GreeterService(ILogger<GreeterService> logger, Repository repository, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        public override async Task<PickUpUserReply> TakeUser(PickUpUser request, ServerCallContext context)
        {
            var users = _repository.GetCollection<UserDocument>("Users");
            var user = users.Find(Builders<UserDocument>.Filter.Eq(x => x.Login, "kirill")).FirstOrDefault();
            
            return new PickUpUserReply { User = _mapper.Map<Messenger.ChatService.Protos.User>(user) };
        }

        
    }
}