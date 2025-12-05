namespace EmpList.Model
{
    public class MDepartment
    {
        public long MDepartmentId { get; set; }              
        public string DepartmentName { get; set; } = string.Empty;

        public string DepartmentCode { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

    }
}
