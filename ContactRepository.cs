using EmpList.Data;
using EmpList.Model;
using Microsoft.EntityFrameworkCore;
using static EmpList.Repository.ContactRepository;

namespace EmpList.Repository
{

    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Contact> AddAsync(Contact contact)
        {
            _context.Contact.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<Contact?> GetByIdAsync(long id)
        {
            return await _context.Contact
                .Where(x => x.ContactId == id && !x.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _context.Contact
                .Where(x => x.IsActive && !x.IsDeleted)
                .ToListAsync();
        }

        public async Task UpdateAsync(Contact contact)
        {
            contact.ModifiedOn = DateTime.UtcNow;
            _context.Contact.Update(contact);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var contact = await GetByIdAsync(id);
            if (contact != null)
            {
                contact.IsDeleted = true;
                contact.IsActive = false;
                contact.ModifiedOn = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
        }
    }
}