using Core.Entities;
using DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Impl
{
    public class DashboardRepository : Repository<Dashboard>, IDashboardRepository
    {
        public DashboardRepository(DashboardContext context) : base(context) { }

        /// <summary>
        /// Get Dashboards
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Dashboard>> GetDashboards(Guid? userId)
        {
            return await _context.Dashboards.Where(d => d.UserId.Equals(userId))
                    .Include(d => d.Widgets)
                    .ToListAsync();
        }
    }
}