using EmpList.Model;

namespace EmpList.Repository
{
    public interface IUserRepository
    {
        Task<User> GetById(int id);
        Task<List<User>> GetAllUsers();
        Task<User> GetByEmail(string email);

        Task<User> GetByPhoneNo(string phoneNo);
        Task<User> GetForLogin(string emailOrPhoneOrName);
        Task<User> GetByFullName(string fullName);
        Task<List<User>> GetAllActive();
        Task Add(User user);
        Task Update(User user);
        Task SoftDelete(int id);
        Task<bool> Exists(string email);
        Task<User> FindUser(string emailOrPhoneOrName);
        Task<bool> ChangePassword(string oldPassword, string newPassword, User user);

    }
    public interface IPasswordResetRepository
    {
        Task Add(PasswordResetToken token);
        Task<PasswordResetToken> GetValidToken( string token);
        Task Update(PasswordResetToken token);
        Task<PasswordResetToken> GetById(int id);
    }
}
