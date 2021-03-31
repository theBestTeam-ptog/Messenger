using System;
using AutoMapper;
using Domain.Models;
using Google.Protobuf.WellKnownTypes;

namespace ChatService
{
    public static class MappConfig
    {
        public static MapperConfiguration Create()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, Messenger.ChatService.Protos.User>()
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
                    .ForMember(m => m.Chats, opt => opt.MapFrom(src => src.Chats))
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
                    .ForMember(m => m.ProfileImage, opt => opt.MapFrom(src => src.ProfileImage))
                    .ForMember(m => m.UserName, opt => opt.MapFrom(src => src.UserName));

                cfg.CreateMap<Messenger.ChatService.Protos.User, User>()
                    .ForMember(m => m.Authorize, op => op.MapFrom(src => src.Authorize.ToDateTime()))
                    .ForMember(m => m.Chats, opt => opt.MapFrom(src => src.Chats))
                    .ForMember(m => m.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(m => m.Login, opt => opt.MapFrom(src => src.Login))
                    .ForMember(m => m.Password, opt => opt.MapFrom(src => src.Password))
                    .ForMember(m => m.Private, opt => opt.MapFrom(src => src.Private))
                    .ForMember(m => m.Registration, opt => opt.MapFrom(src => src.Registration.ToDateTime()))
                    .ForMember(m => m.InNetwork, opt => opt.MapFrom(src => src.InNetwork))
                    .ForMember(m => m.ProfileImage, opt => opt.MapFrom(src => src.ProfileImage))
                    .ForMember(m => m.UserName, opt => opt.MapFrom(src => src.UserName));
            });
        }
    }
}