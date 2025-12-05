namespace EmpList.ModelDTO
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string Qualification { get; set; } = string.Empty;
        public string? Gender { get; set; }
        public DateOnly DateOfJoining { get; set; }
        public List<string> Skills { get; set; }

        public int Id { get; internal set; }
    }
    public class CreateEmployeeDto
    {
        public string EmployeeName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfJoining { get; set; }
        public long MDepartmentId { get; set; }
        public long MQualificationId { get; set; }
        public List<string> Skills { get; set; }

        // Contact Info
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string? PinCode { get; set; }
        public int? Age { get; set; }
        public DateOnly? DOB { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
    public class UpdateEmployeeDto  
    {
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfJoining { get; set; }
        public List<string> Skills { get; set; }
        public long MDepartmentId { get; set; }
        public long MQualificationId { get; set; }

        // Contact Info
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string? PinCode { get; set; }
        public int? Age { get; set; }
        public DateOnly? DOB { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;


    }

    public class EmployeeResponseDto
    {
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfJoining { get; set; }

        public List<string> Skills { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public string QualificationName { get; set; }

        // Contact
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public int? Age { get; set; }
        public string? PinCode { get; set; }
        public DateOnly? DOB { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }

    public class MDepartmentDto
    {
        public long MDepartmentId { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }

    public class MQualificationDto
    {
        public int MQualificationId { get; set; }

        public string QualificationName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
