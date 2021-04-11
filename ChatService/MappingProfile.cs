using System;
using AutoMapper;
using Domain.DbModels;
using Google.Protobuf.WellKnownTypes;
using Messenger.ChatService.Protos;

namespace ChatService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDocument, User>()
                .ForMember(m => m.Authorize, op => op.MapFrom(src => Timestamp.FromDateTime(new DateTime
                (
                    src.Authorize.Year,
                    src.Authorize.Month,
                    src.Authorize.Day,
                    src.Authorize.Hour,
                    src.Authorize.Minute,
                    src.Authorize.Second,
                    DateTimeKind.Utc
                ))))
                .ForMember(m => m.ChatsIds, opt => opt.MapFrom(src => src.ChatsIds))
                .ForMember(m => m.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(m => m.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(m => m.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(m => m.Private, opt => opt.MapFrom(src => src.Private))
                .ForMember(m => m.Registration, opt => opt.MapFrom(src => Timestamp.FromDateTime(new DateTime
                (
                    src.Registration.Year,
                    src.Registration.Month,
                    src.Registration.Day,
                    src.Registration.Hour,
                    src.Registration.Minute,
                    src.Registration.Second,
                    DateTimeKind.Utc
                ))))
                .ForMember(m => m.InNetwork, opt => opt.MapFrom(src => src.InNetwork))
                //.ForMember(m => m.ProfileImage, opt => opt.MapFrom(src => src.ProfileImage))
                .ForMember(m => m.UserName, opt => opt.MapFrom(src => src.UserName));

            CreateMap<User, UserDocument>()
                .ForMember(m => m.Authorize, op => op.MapFrom(src => src.Authorize.ToDateTime()))
                .ForMember(m => m.ChatsIds, opt => opt.MapFrom(src => src.ChatsIds))
                .ForMember(m => m.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(m => m.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(m => m.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(m => m.Private, opt => opt.MapFrom(src => src.Private))
                .ForMember(m => m.Registration, opt => opt.MapFrom(src => src.Registration.ToDateTime()))
                .ForMember(m => m.InNetwork, opt => opt.MapFrom(src => src.InNetwork))
                //.ForMember(m => m.ProfileImage, opt => opt.MapFrom(src => src.ProfileImage))
                .ForMember(m => m.UserName, opt => opt.MapFrom(src => src.UserName));

            CreateMap<ChatDocument, Chat>()
                .ForMember(c => c.History, op => op.MapFrom(src => src.History))
                .ForMember(c => c.Id, op => op.MapFrom(src => Guid.Parse(src.Id)))
                .ForMember(c => c.UserIds, op => op.MapFrom(src => src.UserIds));

            CreateMap<Chat, ChatDocument>()
                .ForMember(c => c.History, op => op.MapFrom(src => src.History))
                .ForMember(c => c.Id, op => op.MapFrom(src => Guid.Parse(src.Id)))
                .ForMember(c => c.UserIds, op => op.MapFrom(src => src.UserIds));
        }
    }
}