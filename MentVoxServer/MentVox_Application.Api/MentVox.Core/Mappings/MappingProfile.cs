using AutoMapper;
using MentVox.Core.Models;
using MentVox.Core.Models.ConversationModels;
using MentVox.Core.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MentVox.Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<User, UserDto>().ReverseMap();
            //CreateMap<Conversation, ConversationDto>().ReverseMap();

            CreateMap<ChatGptResponseDto, ChatGptResponseDto>().ReverseMap();
            CreateMap<ElevenLabsResponseDto, ElevenLabsResponseDto>().ReverseMap();
            CreateMap<WhisperResponseDTO, WhisperResponseDTO>().ReverseMap();
        }
    }
}
