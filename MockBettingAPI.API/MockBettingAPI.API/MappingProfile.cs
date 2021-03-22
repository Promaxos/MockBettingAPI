using AutoMapper;
using MockBettingAPI.API.Models;
using MockBettingAPI.Data;

namespace MockBettingAPI.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Match, MatchToReturnDto>()
                .ForMember(dest => dest.MatchDate, opt => opt.MapFrom(src =>
                src.MatchDate.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.MatchTime, opt => opt.MapFrom(src =>
                src.MatchDate.ToString("hh:mm")))
                .ForMember(dest => dest.Sport, opt => opt.MapFrom(src =>
                src.Sport.ToString()));

            CreateMap<MatchOddsToCreateDto, MatchOdds>()
                .ForMember(dest => dest.Specifier, opt => opt.MapFrom(src =>
                src.Specifier.ToUpper()));
        }
    }
}
