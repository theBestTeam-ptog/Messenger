using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Repositories;
using Google.Protobuf.Collections;
using Grpc.Core;
using Domain.Protos;
using Microsoft.Extensions.Logging;

namespace ChatService
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;

        public GreeterService(ILogger<GreeterService> logger, IUserRepository repository,
            IMapper mapper, IChatRepository chatRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = repository;
            _chatRepository = chatRepository;
        }

        public override async Task<GetUserReply> Authorization(GetUser request, ServerCallContext context)
        {
            _logger.LogInformation($"Started search User: [{request.Login}]");
            var user = await _userRepository.GetUserValidationAsync(request.Login, request.Password);
            
            if (user is null)
            {
                _logger.LogError($"There is no user with this Login: [{request.Login}], or you made a mistake when entering the data");
                return new GetUserReply { Response = Response.Error };
            }
            
            _logger.LogInformation($"User: [{request.Login}] found, await response");
            return new GetUserReply { User = _mapper.Map<User>(user) };
        }

        public override async Task<GetChatReply> TakeChats(GetChats request, ServerCallContext context)
        {
            _logger.LogInformation("Chats search started");
            var chats = await _chatRepository.GetChatsAsync(request.UserId);
            
            if (chats is null)
            {
                _logger.LogError("No chat found");
                return new GetChatReply { Response = Response.Error };
            }
            
            _logger.LogInformation("Chat(s) found await reply");
            return new GetChatReply {
                Response = Response.Ok,
                Chats = { _mapper.Map<RepeatedField<Chat>>(chats) }
            };
        }

        public override async Task<Reply> RegistrationUser(UserCreate request, ServerCallContext context)
        {
            if (request?.User is null)
            {
                _logger.LogError($"");
            }
            _logger.LogInformation($"Starting registration User [{request.User.UserName}]");
            await _userRepository.CreateUserAsync(request.User);
            return new Reply { Response = Response.Ok };
        }

        public override async Task<Reply> CreateChat(ChatCreate request, ServerCallContext context)
        {
            await _chatRepository.CreateChatAsync(new Chat
            {
                Id = Guid.NewGuid().ToString(),
                History = { request.History},
                UserIds = { request.UserIds}
            });
            
            return new Reply { Response = Response.Ok };
        }

        public override async Task<Reply> SendMessage(MessageCreate request, ServerCallContext context)
        {
            await _chatRepository.AddMessageAsync(request.ChatId, request.Message);

            return new Reply {Response = Response.Ok};
        }

        public override async Task JoinChat(GetChat request, IServerStreamWriter<Message> responseStream, ServerCallContext context)
        {
            var listMessages = new List<Message>();

            while(context.CancellationToken.IsCancellationRequested)
            {
                var chat = await _chatRepository.GetChatAsync(request.ChatId);
                listMessages = chat.History.Except(listMessages).ToList();
                foreach (var message in listMessages)
                {
                    await responseStream.WriteAsync(message);
                }
            }
        }

        public override async Task GetUsers(GetUsersByName request, IServerStreamWriter<GetUserReply> responseStream, ServerCallContext context)
        {
            // while (context.CancellationToken.IsCancellationRequested)
            // {
            //     foreach (var user in _userRepository.GetUsersByNameAsync(request.Name))
            //     {
            //         
            //     }
            // }
            // return 
        }
    }
}