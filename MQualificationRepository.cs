using EmpList.Data;
using EmpList.Model;
using Microsoft.EntityFrameworkCore;

namespace EmpList.Repository
{
    public class MQualificationRepository : IMQualificationRepository
    {
        private readonly AppDbContext _context;

        public MQualificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MQualification>> GetAllAsync()
        {
            return await _context.MQualification
                .Where(d => !d.IsDeleted)
                .ToListAsync();
        }

        // ✅ Get department by Id
        public async Task<MQualification?> GetByIdAsync(int id)
        {
            return await _context.MQualification
                .FirstOrDefaultAsync(d => d.MQualificationId == id && !d.IsDeleted);
        }

        // ✅ Add new department
        public async Task<MQualification> AddAsync(MQualification department)
        {
            await _context.MQualification.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }

        // ✅ Update existing department
        public async Task<MQualification?> UpdateAsync(MQualification department)
        {
            var existing = await _context.MQualification
                .FirstOrDefaultAsync(d => d.MQualificationId == department.MQualificationId && !d.IsDeleted);

            if (existing == null)
                return null;



            _context.MQualification.Update(existing);
            await _context.SaveChangesAsync();

            return existing;
        }

        // ✅ Soft delete department
        public async Task<bool> DeleteAsync(int id)
        {
            var department = await _context.MQualification.FirstOrDefaultAsync(d => d.MQualificationId == id && !d.IsDeleted);

            if (department == null)
                return false;

            department.IsDeleted = true;
            department.ModifiedOn = DateTime.Now;

            _context.MQualification.Update(department);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
