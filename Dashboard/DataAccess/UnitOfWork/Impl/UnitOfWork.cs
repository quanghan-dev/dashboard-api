using DataAccess.Persistence;
using DataAccess.Repositories;
using DataAccess.Repositories.Impl;

namespace DataAccess.UnitOfWork.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private DashboardContext _context;
        private IAccountRepository? _accountRepository;
        private IConfigsRepository? _configsRepository;
        private IDashboardRepository? _dashboardRepository;
        private IContactRepository? _contactRepository;
        private ITaskRepository? _taskRepository;
        private IWidgetRepository? _widgetRepository;

        public IAccountRepository Accounts
        {
            get { _accountRepository ??= new AccountRepository(_context); return _accountRepository; }
        }

        public IConfigsRepository Configs
        {
            get { _configsRepository ??= new ConfigsRepository(_context); return _configsRepository; }
        }

        public IDashboardRepository Dashboards
        {
            get { _dashboardRepository ??= new DashboardRepository(_context); return _dashboardRepository; }
        }

        public IContactRepository Contacts
        {
            get { _contactRepository ??= new ContactRepository(_context); return _contactRepository; }
        }

        public ITaskRepository Tasks
        {
            get { _taskRepository ??= new TaskRepository(_context); return _taskRepository; }
        }

        public IWidgetRepository Widgets
        {
            get { _widgetRepository ??= new WidgetRepository(_context); return _widgetRepository; }
        }

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