using Application.Models.Dashboards;
using AutoMapper;
using Core.Entities;

namespace Application.MappingProfiles
{
    public class DashboardProfile : Profile
    {
        public DashboardProfile()
        {
            CreateMap<UpdateDashboardRequest, Dashboard>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null && src != default));
            CreateMap<Dashboard, DashboardResponse>();
        }
    }
}