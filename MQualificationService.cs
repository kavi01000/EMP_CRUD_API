using AutoMapper;
using EmpList.Model;
using EmpList.ModelDTO;
using EmpList.Repository;
using EmpList.UnitOfWork;

namespace EmpList.Services
{
    public class MQualificationService : IMqualificationService
    {
        private readonly IMQualificationRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MQualificationService(IMQualificationRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MQualificationDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<MQualificationDto>>(list);
        }

        public async Task<MQualificationDto?> GetByIdAsync(int id)
        {
            var qualification = await _repository.GetByIdAsync(id);
            return _mapper.Map<MQualificationDto?>(qualification);
        }

        public async Task<MQualificationDto> CreateAsync(MQualificationDto dto)
        {
            var entity = _mapper.Map<MQualification>(dto);
            entity.CreatedOn = DateTime.UtcNow;
            entity.IsDeleted = false;

            var created = await _repository.AddAsync(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<MQualificationDto>(created);
        }

        public async Task<MQualificationDto?> UpdateAsync(int id, MQualificationDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            existing.ModifiedOn = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(existing);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<MQualificationDto>(updated);
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
