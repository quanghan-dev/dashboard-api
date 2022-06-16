using DataAccess.Persistence;

namespace DataAccess.Repositories.Impl
{
    public class TaskRepository : Repository<Core.Entities.Task>, ITaskRepository
    {
        public TaskRepository(DashboardContext context) : base(context) { }
    }
}