﻿using AutoMapper;
using VibLink.Models.DTOs.Request;
using VibLink.Models.DTOs.Response;
using VibLink.Models.Entities;
using MongoDB.Bson;

namespace VibLink.Mappers
{
    public class MessageMappingProfile : Profile
    {
        public MessageMappingProfile()
        {
            CreateMap<MessageCreateRequest, Message>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.MessageType, opt => opt.MapFrom(src => src.MessageType))
                .ForMember(dest => dest.SenderId, opt => opt.MapFrom((src, dest, destMember, context) =>
                    context.Items.TryGetValue("SenderId", out object? value) ? (ObjectId)value : ObjectId.Empty
                ));

            CreateMap<Message, MessageDetailsResponse>()
                .ForMember(dest => dest.AuditMetadataResponse, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Sender, opt => opt.MapFrom(src => src.Sender))
                .ForMember(dest => dest.Recipients, opt => opt.MapFrom(src => src.Recipients))
                .ForMember(dest => dest.SeenBy, opt => opt.MapFrom((src, dest, destMember, context) =>
                    src.SeenBy?.Select(dict =>
                            dict.ToDictionary(
                                kvp => context.Mapper.Map<UserSummaryBaseResponse>(kvp.Key),
                                kvp => kvp.Value
                            )
                        ).ToList()
                ))
                .ForMember(dest => dest.Reactions, opt => opt.MapFrom((src, dest, destMember, context) =>
                    src.Reactions?.Select(dict =>
                            dict.ToDictionary(
                                kvp => context.Mapper.Map<UserSummaryBaseResponse>(kvp.Key),
                                kvp => kvp.Value
                            )
                        ).ToList()
                ));
        }
    }
}
