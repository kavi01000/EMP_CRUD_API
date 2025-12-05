namespace EmpList.ModelDTO
{
    public class ChangePasswordDto
    {

       
        public string EmailOrPhoneOrName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

     
    }
}
