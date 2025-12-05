namespace EmpList.Model
{
    public class Contact
    {
        public long ContactId { get; set; }
        public long EmployeeId { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string? PinCode { get; set; }
        public int? Age { get; set; }
        public DateOnly? DOB { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        // Navigation
        public Employee Employee { get; set; }

    }
}
