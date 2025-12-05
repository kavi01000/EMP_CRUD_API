using EmpList.ModelDTO;

namespace EmpList.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeResponseDto> AddEmployee(CreateEmployeeDto dto);
        Task<IEnumerable<EmployeeResponseDto>> GetAll();
        Task<EmployeeResponseDto> GetById(long id);

        Task<EmployeeResponseDto> UpdateEmployee(UpdateEmployeeDto dto);

        Task<bool> DeleteEmployee(long id);
    }


    public interface IContactService
    {
        Task<ContactDto> CreateAsync(ContactDto dto);
        Task<ContactDto> UpdateAsync(long id, ContactDto dto);
        Task<bool> DeleteAsync(long id);
        Task<ContactDto?> GetByIdAsync(long id);
        Task<IEnumerable<ContactDto>> GetAllAsync();
    }


    public interface IMdepartmentService
    {
        Task<IEnumerable<MDepartmentDto>> GetAllAsync();
        Task<MDepartmentDto?> GetByIdAsync(int id);
        Task<MDepartmentDto> CreateAsync(MDepartmentDto dto);
        Task<MDepartmentDto?> UpdateAsync(int id, MDepartmentDto dto);
        Task<bool> DeleteAsync(int id);
    }
    public interface IMqualificationService
    {
        Task<IEnumerable<MQualificationDto>> GetAllAsync();
        Task<MQualificationDto?> GetByIdAsync(int id);
        Task<MQualificationDto> CreateAsync(MQualificationDto dto);
        Task<MQualificationDto?> UpdateAsync(int id, MQualificationDto dto);
        Task<bool> DeleteAsync(int id);
    }

}
