using Core.Entities;
using DataAccess.Persistence;

namespace DataAccess.Repositories.Impl
{
    public class ConfigsRepository : Repository<Configs>, IConfigsRepository
    {
        public ConfigsRepository(DashboardContext context) : base(context) { }
    }
}