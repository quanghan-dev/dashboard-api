using Core.Entities;
using DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Impl
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(DashboardContext context) : base(context) { }

        /// <summary>
        /// Get Contacts By Keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<List<Contact>> GetContactsByKeyword(string? keyword)
        {
            if (keyword != null)
                return (await _context.Contacts.Where(contact =>
                            contact.FirstName!.ToLower().Contains(keyword.ToLower()) ||
                            contact.LastName!.ToLower().Contains(keyword.ToLower()) ||
                            contact.Title!.ToLower().Contains(keyword.ToLower()) ||
                            contact.Department!.ToLower().Contains(keyword.ToLower()) ||
                            contact.Project!.ToLower().Contains(keyword.ToLower())
                        )
                        .ToListAsync())!;

            return (await _context.Contacts.ToListAsync())!;
        }
    }
}