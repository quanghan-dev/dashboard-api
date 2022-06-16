using Core.Entities;
using DataAccess.Persistence;

namespace DataAccess.Repositories.Impl
{
    public class DashboardRepository : Repository<Dashboard>, IDashboardRepository
    {
        public DashboardRepository(DashboardContext context) : base(context) { }
    }
}