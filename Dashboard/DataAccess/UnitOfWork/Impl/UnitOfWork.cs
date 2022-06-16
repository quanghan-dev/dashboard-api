using DataAccess.Persistence;
using DataAccess.Repositories;
using DataAccess.Repositories.Impl;

namespace DataAccess.UnitOfWork.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private DashboardContext _context;

        public IAccountRepository Accounts => Accounts ?? new AccountRepository(_context);

        public IConfigsRepository Configs => Configs ?? new ConfigsRepository(_context);

        public IDashboardRepository Dashboards => Dashboards ?? new DashboardRepository(_context);

        public IContactRepository Contacts => Contacts ?? new ContactRepository(_context);

        public ITaskRepository Tasks => Tasks ?? new TaskRepository(_context);

        public IWidgetRepository Widgets => Widgets ?? new WidgetRepository(_context);

        public UnitOfWork(DashboardContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}