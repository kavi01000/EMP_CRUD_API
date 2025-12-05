using EmpList.Model;
using EmpList.ModelDTO;

namespace EmpList.Services
{
    public interface IAuthService
    {



        Task<LoginResponseDto?> Login(LoginDto dto);
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);

        Task<bool> Register(registerDto dto);

        Task<bool> UpdateUser(int id, UserUpdateDto dto);
        Task<bool> Delete(int id);


        Task<string> ForgotPassword(ForgotPasswordDto dto);
        Task<bool> ResetPassword(ResetPasswordDto dto);
        Task<bool> ChangePassword(ChangePasswordDto dto);



    }
}
