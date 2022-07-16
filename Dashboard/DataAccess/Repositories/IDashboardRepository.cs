using Core.Entities;

namespace DataAccess.Repositories
{
    public interface IDashboardRepository : IRepository<Dashboard>
    {
        /// <summary>
        /// Get Dashboards
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<Dashboard>> GetDashboards(Guid? userId);
    }
}