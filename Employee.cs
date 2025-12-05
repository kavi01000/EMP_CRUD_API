using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpList.Model
{
    public class Employee
{
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfJoining { get; set; }
        public List<string> Skills { get; set; }
        public long MDepartmentId { get; set; }
        public long MQualificationId { get; set; }

        public MDepartment Department { get; set; }
        public MQualification Qualification { get; set; }
        public Contact Contact { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
