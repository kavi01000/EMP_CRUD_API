using EmpList.Data;
using EmpList.Model;
using EmpList.ModelDTO;
using EmpList.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmpList.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IUnitOfWork uow, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _uow = uow;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        // ✅ Get all users
        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await _uow.Users.GetAllUsers();
            return users ?? new List<User>();
        }

        // ✅ Get user by ID
        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _uow.Users.GetById(id);
            return user;
        }

        // ✅ Login and generate JWT
        public async Task<LoginResponseDto?> Login(LoginDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.EmailOrPhoneOrName) ||
                string.IsNullOrWhiteSpace(dto.Password))
            {
                return null;
            }

            // Get user from database
            var user = await _uow.Users.GetForLogin(dto.EmailOrPhoneOrName);
            if (user == null) return null;

            // Verify password
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;

            // ✅ Generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();

            // Use a strong key (at least 32 characters)
            var secretKey = _config["Jwt:Key"];
            if (string.IsNullOrEmpty(secretKey) || secretKey.Length < 32)
            {
                throw new Exception("JWT key must be at least 32 characters long");
            }

            var key = Encoding.UTF8.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim("FullName", user.FullName), // custom claim
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserId", user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            // ✅ Return full response
            return new LoginResponseDto
            {
                Token = tokenHandler.WriteToken(token),
                FullName = user.FullName,
                Email = user.Email,
                PhoneNo = user.PhoneNo
            };
        }


        // ✅ Change password with ModifiedBy from token
        public async Task<bool> ChangePassword(ChangePasswordDto dto)
        {
            var user = await _uow.Users.FindUser(dto.EmailOrPhoneOrName);
            if (user == null) return false;

            var loggedInUserId = ClaimsHelper.GetUserId(_httpContextAccessor.HttpContext?.User);
            user.ModifiedBy = loggedInUserId;
            user.ModifiedOn = DateTime.UtcNow;

            return await _uow.Users.ChangePassword(dto.OldPassword, dto.NewPassword, user);
        }

        // ✅ Register with CreatedBy from token
        public async Task<bool> Register(registerDto dto)
        {
            var exists = await _uow.Users.GetByEmail(dto.Email);
            if (exists != null) return false;

            var loggedInUserId = ClaimsHelper.GetUserId(_httpContextAccessor.HttpContext?.User);

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNo = dto.PhoneNo,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                IsActive = true,
                IsDeleted = false,
                CreatedBy = loggedInUserId,
                ModifiedBy = loggedInUserId,
                CreatedOn = DateTime.UtcNow
            };

            await _uow.Users.Add(user);
            await _uow.CommitAsync();
            return true;
        }

        // ✅ Update user with ModifiedBy from token
        public async Task<bool> UpdateUser(int id, UserUpdateDto dto)
        {
            var user = await _uow.Users.GetById(id);
            if (user == null) return false;

            var loggedInUserId = ClaimsHelper.GetUserId(_httpContextAccessor.HttpContext?.User);

            user.FullName = dto.FullName;
            user.PhoneNo = dto.PhoneNo;
            user.ModifiedBy = loggedInUserId;
            user.ModifiedOn = DateTime.UtcNow;

            await _uow.Users.Update(user);
            await _uow.CommitAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            await _uow.Users.SoftDelete(id);
            await _uow.CommitAsync();
            return true;
        }

        // ✅ Forgot Password
        public async Task<string> ForgotPassword(ForgotPasswordDto dto)
        {
            var user = await _uow.Users.GetByEmail(dto.Email);
            if (user == null) return null;

            var token = Guid.NewGuid().ToString();

            var reset = new PasswordResetToken
            {
                UserId = user.UserId,
                Token = token,
                CreatedOn = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMinutes(2),
                ModifiedOn = DateTime.UtcNow,
                IsUsed = false,
                IsActive = true
            };

            await _uow.PasswordResetTokens.Add(reset);
            await _uow.CommitAsync();

            return token;
        }

        // ✅ Reset Password with ModifiedBy
        public async Task<bool> ResetPassword(ResetPasswordDto dto)
        {
            var reset = await _uow.PasswordResetTokens.GetValidToken(dto.Token);
            if (reset == null) return false;

            var loggedInUserId = ClaimsHelper.GetUserId(_httpContextAccessor.HttpContext?.User);

            var user = reset.User;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            user.ModifiedBy = loggedInUserId;
            user.ModifiedOn = DateTime.UtcNow;

            reset.IsUsed = true;
            reset.ModifiedOn = DateTime.UtcNow;

            await _uow.Users.Update(user);
            await _uow.PasswordResetTokens.Update(reset);
            await _uow.CommitAsync();

            return true;
        }

    }
}
