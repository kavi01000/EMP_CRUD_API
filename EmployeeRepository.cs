using EmpList.Data;
using EmpList.Model;
using EmpList.ModelDTO;
using Microsoft.EntityFrameworkCore;

namespace EmpList.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            await _context.Employee.AddAsync(employee);
            return employee;
        }

        public async Task<Employee> GetEmployeeByIdAsync(long id)
        {
            return await _context.Employee
                .Include(e => e.Department)
                .Include(e => e.Qualification)
                .Include(e => e.Contact)
                .FirstOrDefaultAsync(e => e.EmployeeId == id && !e.IsDeleted);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employee
                .Include(e => e.Department)
                .Include(e => e.Qualification)
                .Include(e => e.Contact)
                .Where(e => !e.IsDeleted)
                .ToListAsync();
        }

        public async Task UpdateEmployeeByIdAsync(long id, UpdateEmployeeDto dto)
        {
            var emp = await _context.Employee
                .Include(x => x.Contact)
                .FirstOrDefaultAsync(x => x.EmployeeId == id);

            if (emp != null)
            {
                // Employee Table Update
                emp.EmployeeName = dto.EmployeeName;
                emp.DateOfJoining = dto.DateOfJoining;
                emp.Gender = dto.Gender;
                emp.MDepartmentId = dto.MDepartmentId;
                emp.MQualificationId = dto.MQualificationId;
                emp.IsActive = dto.IsActive;
                emp.ModifiedOn = dto.ModifiedOn;
                emp.Skills = dto.Skills;

                // Contact Table Update
                if (emp.Contact != null)
                {
                    emp.Contact.Email = dto.Email;
                    emp.Contact.PhoneNo = dto.PhoneNo;
                    emp.Contact.Age = dto.Age;
                    emp.Contact.PinCode = dto.PinCode;
                    emp.Contact.DOB = dto.DOB;
                }
            }
        }


        public async Task DeleteEmployeeAsync(long id)
        {
            var emp = await _context.Employee.FindAsync(id);
            if (emp != null)
            {
                emp.IsDeleted = true;
                emp.IsActive = false;
                emp.ModifiedOn= DateTime.UtcNow;
            }
        }
    }

}
