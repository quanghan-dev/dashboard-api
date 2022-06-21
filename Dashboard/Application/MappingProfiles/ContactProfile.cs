using Application.Models.Contacts;
using AutoMapper;
using Core.Entities;

namespace Application.MappingProfiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<CreateContactRequest, Contact>();
            CreateMap<UpdateContactRequest, Contact>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Contact, ContactResponse>();
        }
    }
}