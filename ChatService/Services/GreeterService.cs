using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Protos;
using Domain.Repositories;
using Google.Protobuf.Collections;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace ChatService.Services
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
            if (request is null)
            {
                _logger.LogError("Request is null");
                return new GetUserReply
                {
                    Response = Response.Error
                };
            }
            
            _logger.LogInformation($"Started search User: [{request.Login}]");
            var user = await _userRepository.GetUserValidationAsync(request.Login, request.Password);
            
            if (user is null)
            {
                _logger.LogError($"There is no user with this Login: [{request.Login}], or you made a mistake when entering the data");
                return new GetUserReply { Response = Response.Error };
            }
            
            _logger.LogInformation($"User: [{request.Login}] found, await response");
            return new GetUserReply
            {
                Response = Response.Ok,
                User = user
            };
        }

        public override async Task<GetChatReply> TakeChats(GetChats request, ServerCallContext context)
        {
            if (request?.UserId is null)
            {
                _logger.LogError("Request is null");
                return new GetChatReply
                {
                    Response = Response.Error
                };
            }
            
            _logger.LogInformation("Chats search started");
            
            var chats = await _chatRepository.GetChatsAsync(request.UserId);
            if (chats is null)
            {
                _logger.LogError("No chat found");
                return new GetChatReply
                {
                    Response = Response.Error
                };
            }
            
            _logger.LogInformation("Chat(s) found await reply");

            return new GetChatReply
            {
                Response = Response.Ok,
                Chats =
                {
                    _mapper.Map<RepeatedField<Chat>>(chats)
                }
            };
        }

        public override async Task<Reply> RegistrationUser(UserCreate request, ServerCallContext context)
        {
            if (request is null)
            {
                _logger.LogError("Request is null");
                return new Reply
                {
                    Response = Response.Error
                };
            }
            
            _logger.LogInformation($"Starting registration User [{request.UserName}]");
            
            await _userRepository.CreateUserAsync(request.UserName, request.Login, request.Password);
            return new Reply
            {
                Response = Response.Ok
            };
        }

        public override async Task<Reply> CreateChat(ChatCreate request, ServerCallContext context)
        {
            if (request?.Chat is null)
            {
                _logger.LogError("Request is null");
                return new Reply
                {
                    Response = Response.Error
                };
            }
            
            _logger.LogInformation("Starting creating Chat");
            
            await _chatRepository.CreateChatAsync(request.Chat);
            
            _logger.LogInformation("Chat created successfully ");
            
            return new Reply
            {
                Response = Response.Ok
            };
        }

        public override async Task<Reply> SendMessage(MessageCreate request, ServerCallContext context)
        {
            if (request?.ChatId is null)
            {
                _logger.LogError("Request is null");
                return new Reply
                {
                    Response = Response.Error
                };
            }
            
            await _chatRepository.AddMessageAsync(request.ChatId, request.Message);
            
            _logger.LogInformation("Message sent");

            return new Reply
            {
                Response = Response.Ok
            };
        }

        public override async Task JoinChat(GetChat request, IServerStreamWriter<Message> responseStream, ServerCallContext context)
        {
            if (request?.ChatId is null)
            {
                _logger.LogError("Request is null");
                return;
            }
            
            var prevMessages = new List<Message>();
            var listMessages = new List<Message>();

            while(!context.CancellationToken.IsCancellationRequested)
            {
                var chat = await _chatRepository.GetChatAsync(request.ChatId);

                listMessages = chat.History.Except(prevMessages).ToList();
                
                foreach (var message in listMessages)
                {
                    await responseStream.WriteAsync(message);
                }

                if(listMessages.Count != 0 || prevMessages.Count == 0)
                    prevMessages.AddRange(listMessages);
            }
        }
        

        public override async Task<GetUsersReply> GetUsers(GetUsersByName request, ServerCallContext context)
        {
            var user = _userRepository.SearchSync(request.Name);

            if (user is null)
            {
                return new GetUsersReply
                {
                    Response = Response.Error,
                    User = { new User() }
                };
            }

            return new GetUsersReply
            {
                Response = Response.Ok,
                User = { user }
            };
        }
    }
}