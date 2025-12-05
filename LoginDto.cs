using System.ComponentModel.DataAnnotations;

namespace EmpList.ModelDTO
{
   
    public class LoginDto
    {
        public string? EmailOrPhoneOrName { get; set; }  // single field
        public string Password { get; set; }
    }

    public class UserUpdateDto
    {
        public int UserId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string PhoneNo { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; } = false;
    }

    public class LoginResponseDto
    {
        public string FullName { get; set; }

        public string Token { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
    }


    public class UserDto
    {
        public int UserId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNo { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
