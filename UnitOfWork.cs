using EmpList.Data;
using EmpList.Model;
using EmpList.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmpList.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _ctx;

        public IUserRepository Users { get; }
        public IPasswordResetRepository PasswordResetTokens { get; }
        public IEmployeeRepository Employees { get; }
        public IContactRepository Contacts {  get; }

        public IMQualificationRepository MQualifications { get; }

        public IMDepartment Mdepartments { get; }



        public UnitOfWork(
            AppDbContext ctx,
            IUserRepository users,
            IPasswordResetRepository passwordReset,
            IEmployeeRepository employee,
   
            IContactRepository contact,
            IMDepartment mdepartment,
            IMQualificationRepository mqualification


        )
        {
            _ctx = ctx;
            Users = users;
            PasswordResetTokens = passwordReset;
            Employees = employee;

            Contacts = contact;
            Mdepartments = mdepartment;
            MQualifications = mqualification;
        }


        public async Task<int> SaveAsync()
        {
            return await _ctx.SaveChangesAsync();
        }
        public async Task<int> Save()
        {
            return await _ctx.SaveChangesAsync();
        }

        public async Task<int> CommitAsync()
        {
            return await _ctx.SaveChangesAsync();
        }
        


    }
}

