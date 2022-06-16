using Core.Entities;
using DataAccess.Persistence;

namespace DataAccess.Repositories.Impl
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(DashboardContext context) : base(context) { }
    }
}