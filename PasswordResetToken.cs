using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpList.Model
{
    public class PasswordResetToken
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Token { get; set; }

        

        public bool IsUsed { get; set; } = false;



        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedOn { get; set; } = null;
        public DateTime ExpiryDate { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
