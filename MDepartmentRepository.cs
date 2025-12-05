using EmpList.Data;
using EmpList.Model;
using EmpList.Services;
using Microsoft.EntityFrameworkCore;

namespace EmpList.Repository
{
    public class MDepartmentRepository : IMDepartment
    {
        private readonly AppDbContext _context;

        public MDepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MDepartment>> GetAllAsync()
        {
            return await _context.MDepartment
                .Where(d => !d.IsDeleted)
                .ToListAsync();
        }

        // ✅ Get department by Id
        public async Task<MDepartment?> GetByIdAsync(int id)
        {
            return await _context.MDepartment
                .FirstOrDefaultAsync(d => d.MDepartmentId == id && !d.IsDeleted);
        }

        // ✅ Add new department
        public async Task<MDepartment> AddAsync(MDepartment department)
        {
            await _context.MDepartment.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }

        // ✅ Update existing department
        public async Task<MDepartment?> UpdateAsync(MDepartment department)
        {
            var existing = await _context.MDepartment
                .FirstOrDefaultAsync(d => d.MDepartmentId == department.MDepartmentId && !d.IsDeleted);

            if (existing == null)
                return null;



            _context.MDepartment.Update(existing);
            await _context.SaveChangesAsync();

            return existing;
        }

        // ✅ Soft delete department
        public async Task<bool> DeleteAsync(int id)
        {
            var department = await _context.MDepartment.FirstOrDefaultAsync(d => d.MDepartmentId == id && !d.IsDeleted);

            if (department == null)
                return false;

            department.IsDeleted = true;
            department.ModifiedOn = DateTime.Now;

            _context.MDepartment.Update(department);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}