using Application.Models.Tasks;
using AutoMapper;
using Task = Core.Entities.Task;

namespace Application.MappingProfiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskRequest, Task>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Task, TaskResponse>();
        }
    }
}