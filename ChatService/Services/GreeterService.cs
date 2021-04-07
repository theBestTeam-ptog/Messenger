using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Helpers;
using AutoMapper;
using ChatService.Data;
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
        private readonly Data.IUserRepository _userRepository;
        private readonly Data.IChatRepository _chatRepository;

        public GreeterService(ILogger<GreeterService> logger, Data.IUserRepository repository,
            IMapper mapper, IChatRepository chatRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = repository;
            _chatRepository = chatRepository;
        }

        public override async Task<PickUpUserReply> TakeUser(PickUpUser request, ServerCallContext context)
        {
            var user = await _userRepository.GetUserValidation(request.Login);

            return new PickUpUserReply {User = _mapper.Map<Messenger.ChatService.Protos.User>(user)};
        }

        public override async Task<ChatReply> TakeChats(ChatRequest request, ServerCallContext context)
        {
            var chats = _mapper.Map<Messenger.ChatService.Protos.Chat>(await _chatRepository.GetChats(request.Id));

            return new ChatReply {Chats = {chats}};
        }

        public override Task<Reply> CreateUser(UserCreate request, ServerCallContext context)
        {
            _userRepository.Create(_mapper.Map<UserDocument>(request.User));

            return Task.FromResult
            (
                new Reply {Reply_ = "Ok"}
            );
        }
    }
}