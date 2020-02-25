using AutoMapper;
using GameAPI.Data.Request.PlayerRequest;
using GameAPI.Data.Response.PlayerResponse;
using GameAPI.Data.Response.QuestionResponse;
using GameAPI.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GameAPI.Data.Mappers.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<CreatePlayer, Player>();
            CreateMap<PlayerQuestion, AnswerResponse>()
                .ForMember(d => d.ParentId, s => s.MapFrom(src => src.Question.ParentId))
                .ForMember(d => d.ChildId, s => s.MapFrom(src => src.Question.Id));
            CreateMap<Player, PlayerResponse>().ForMember(d => d.PlayerQuestions, s => s.MapFrom(src => src.PlayerQuestions));
            CreateMap<Question, QuestionResponse>();
        }
    }
}
