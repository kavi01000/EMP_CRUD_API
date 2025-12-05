using EmpList.Data;
using EmpList.Model;
using Microsoft.EntityFrameworkCore;

namespace EmpList.Repository
{
    public class PasswordResetRepository : IPasswordResetRepository
    {
        private readonly AppDbContext _context;

        public PasswordResetRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(PasswordResetToken token)
        {
            await _context.PasswordResetToken.AddAsync(token);
        }

        public async Task<PasswordResetToken> GetValidToken( string token)
        {
            return await _context.PasswordResetToken
                .Include(x => x.User)
                .FirstOrDefaultAsync(x =>
                    x.Token == token &&
                    !x.IsUsed &&
                    x.ExpiryDate > DateTime.UtcNow);
        }

        public async Task Update(PasswordResetToken token)
        {
            _context.PasswordResetToken.Update(token);
        }

        public async Task<PasswordResetToken> GetById(int id)
        {
            return await _context.PasswordResetToken.FindAsync(id);
        }
    }
}
