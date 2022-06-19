using DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Impl
{
    public class TaskRepository : Repository<Core.Entities.Task>, ITaskRepository
    {
        public TaskRepository(DashboardContext context) : base(context) { }

        /// <summary>
        /// Get Task By Keyword
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<List<Core.Entities.Task>> GetTasksByKeyword(Guid userId, string? keyword)
        {
            if (keyword != null)
                return (await _context.Tasks.Where(task =>
                        task.UserId.Equals(userId) && task.TaskName!.ToLower().Contains(keyword.ToLower()))
                        .ToListAsync())!;

            return (await _context.Tasks.Where(task => task.UserId.Equals(userId)).ToListAsync())!;
        }
    }
}