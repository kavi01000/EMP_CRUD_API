using EmpList.Model;
using EmpList.Repository;
using Microsoft.AspNetCore.Identity;

namespace EmpList.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IPasswordResetRepository PasswordResetTokens { get; }
        IEmployeeRepository Employees { get; }

        IContactRepository Contacts { get; }
        IMQualificationRepository MQualifications { get; }
        IMDepartment Mdepartments { get; }

        Task<int> SaveAsync();

        Task<int> Save();
        Task<int> CommitAsync();


    }
}
