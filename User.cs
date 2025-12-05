using System.ComponentModel.DataAnnotations;

namespace EmpList.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNo { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;



        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }




        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}

