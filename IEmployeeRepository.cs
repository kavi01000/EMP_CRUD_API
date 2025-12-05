using EmpList.Model;
using EmpList.ModelDTO;

namespace EmpList.Repository
{
    public interface IEmployeeRepository
    {

        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<Employee> GetEmployeeByIdAsync(long id);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task UpdateEmployeeByIdAsync(long id, UpdateEmployeeDto dto);

        Task DeleteEmployeeAsync(long id); // soft delete
    }

    public interface IContactRepository
    {
        Task<Contact> AddAsync(Contact contact);
        Task<Contact?> GetByIdAsync(long id);
        Task<IEnumerable<Contact>> GetAllAsync();
        Task UpdateAsync(Contact contact);
        Task DeleteAsync(long id);   // soft delete
    }


    public interface IMDepartment
    {
        Task<IEnumerable<MDepartment>> GetAllAsync();
        Task<MDepartment?> GetByIdAsync(int id);
        Task<MDepartment> AddAsync(MDepartment department);
        Task<MDepartment?> UpdateAsync(MDepartment department);
        Task<bool> DeleteAsync(int id);

    }
    public interface IMQualificationRepository
    {
        Task<IEnumerable<MQualification>> GetAllAsync();
        Task<MQualification?> GetByIdAsync(int id);
        Task<MQualification> AddAsync(MQualification qualification);
        Task<MQualification?> UpdateAsync(MQualification qualification);
        Task<bool> DeleteAsync(int id);

    }

}
