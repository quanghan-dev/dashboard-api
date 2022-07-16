using Application.Models.Widgets;
using AutoMapper;
using Core.Entities;

namespace Application.MappingProfiles
{
    public class WidgetProfile : Profile
    {
        public WidgetProfile()
        {
            CreateMap<Widget, WidgetDto>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}