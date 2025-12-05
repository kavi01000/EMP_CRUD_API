namespace EmpList.ModelDTO
{
    public class registerDto
    {

        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
   
        public bool IsDeleted { get; set; } = false;
    }


}
