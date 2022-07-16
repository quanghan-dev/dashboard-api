using Core.Entities;
using DataAccess.Persistence;

namespace DataAccess.Repositories.Impl
{
    public class WidgetRepository : Repository<Widget>, IWidgetRepository
    {
        public WidgetRepository(DashboardContext context) : base(context) { }
    }
}