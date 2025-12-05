using EmpList.Data;
using EmpList.Model;
using Microsoft.EntityFrameworkCore;

namespace EmpList.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetById(int id)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.UserId == id && !x.IsDeleted);
        }
        public async Task<List<User>> GetAllUsers()
        {
            return await _context.User
                .Where(u => !u.IsDeleted)
                .ToListAsync();
        }

        public async Task<User> FindUser(string emailOrPhoneOrName)
        {
            return await _context.User
                .FirstOrDefaultAsync(x =>
                    !x.IsDeleted &&
                    (x.Email == emailOrPhoneOrName ||
                     x.PhoneNo == emailOrPhoneOrName ||
                     x.FullName == emailOrPhoneOrName));
        }

        // 2️⃣ Implement ChangePassword exactly as in the interface
        public async Task<bool> ChangePassword(string oldPassword, string newPassword, User user)
        {
            if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.PasswordHash))
                return false;

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.ModifiedOn = DateTime.UtcNow;

            _context.User.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<User> GetByEmail(string email)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Email == email && !x.IsDeleted);
        }
        public async Task<User> GetByPhoneNo(string phoneNo)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.PhoneNo == phoneNo && !x.IsDeleted);
        }
        public async Task<User> GetByFullName(string fullName)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.FullName == fullName && !x.IsDeleted);
        }
        public async Task<List<User>> GetAllActive()
        {
            return await _context.User.Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<User> GetForLogin(string emailOrPhoneOrName)
        {
            return await _context.User
                .FirstOrDefaultAsync(x =>
                       !x.IsDeleted &&
                       (x.Email == emailOrPhoneOrName ||
                        x.PhoneNo == emailOrPhoneOrName ||
                        x.FullName == emailOrPhoneOrName));
        }
        public async Task Add(User user)
        {
            await _context.User.AddAsync(user);
        }
        


        public async Task Update(User user)
        {
            _context.User.Update(user);
        }

        public async Task SoftDelete(int id)
        {
            var user = await GetById(id);
            if (user != null)
            {
                user.IsDeleted = true;
                user.IsActive = false;
                user.ModifiedOn = DateTime.UtcNow;

                _context.User.Update(user);
            }
        }

        public async Task<bool> Exists(string email)
        {
            return await _context.User.AnyAsync(x => x.Email == email && !x.IsDeleted);
        }
    }
}
