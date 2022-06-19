namespace DataAccess.Repositories
{
    public interface ITaskRepository : IRepository<Core.Entities.Task>
    {
        /// <summary>
        /// Get Tasks By Keyword
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<Core.Entities.Task>> GetTasksByKeyword(Guid userId, string? keyword);
    }
}