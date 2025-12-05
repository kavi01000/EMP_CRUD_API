using AutoMapper;
using EmpList.Model;
using EmpList.ModelDTO;
using EmpList.Repository;
using EmpList.UnitOfWork;

namespace EmpList.Services
{
    public class MDepartmentService : IMdepartmentService
    {
        private readonly IMDepartment _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MDepartmentService(IMDepartment repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MDepartmentDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<MDepartmentDto>>(list);
        }

        public async Task<MDepartmentDto?> GetByIdAsync(int id)
        {
            var department = await _repository.GetByIdAsync(id);
            return _mapper.Map<MDepartmentDto?>(department);
        }

        public async Task<MDepartmentDto> CreateAsync(MDepartmentDto dto)
        {
            var entity = _mapper.Map<MDepartment>(dto);
            entity.CreatedOn = DateTime.UtcNow;
            entity.IsDeleted = false;

            var created = await _repository.AddAsync(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<MDepartmentDto>(created);
        }

        public async Task<MDepartmentDto?> UpdateAsync(int id, MDepartmentDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            existing.ModifiedOn = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(existing);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<MDepartmentDto>(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (deleted)
                await _unitOfWork.SaveAsync();

            return deleted;
        }
    }
}
