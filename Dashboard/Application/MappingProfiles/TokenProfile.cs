using Application.Models.Tokens;
using AutoMapper;
using Core.Entities;

namespace Application.MappingProfiles
{
    public class TokenProfile : Profile
    {
        public TokenProfile()
        {
            CreateMap<Token, CreateTokenResponse>();
        }
    }
}