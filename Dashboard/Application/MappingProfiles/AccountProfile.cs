using Application.Models.Accounts;
using AutoMapper;
using Core.Entities;

namespace Application.MappingProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<RegisterAccountRequest, Account>();
        }
    }
}