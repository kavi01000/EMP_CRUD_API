namespace EmpList.Model
{
    public class MQualification
    {
        public long MQualificationId { get; set; }
        public string QualificationName { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
