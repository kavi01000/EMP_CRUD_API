using AutoMapper;
using EmpList.Model;
using EmpList.ModelDTO;
using EmpList.UnitOfWork;

namespace EmpList.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<EmployeeResponseDto> AddEmployee(CreateEmployeeDto dto)
        {
            var emp = _mapper.Map<Employee>(dto);

            emp.Contact = new Contact
            {
                Email = dto.Email,
                PhoneNo = dto.PhoneNo,
                Age = dto.Age,
                PinCode = dto.PinCode,
                DOB = dto.DOB,
                
            };

            await _uow.Employees.AddEmployeeAsync(emp);
            await _uow.SaveAsync();

            return _mapper.Map<EmployeeResponseDto>(emp);
        }

        public async Task<IEnumerable<EmployeeResponseDto>> GetAll()
        {
            var data = await _uow.Employees.GetAllEmployeesAsync();
            return _mapper.Map<List<EmployeeResponseDto>>(data);
        }

        public async Task<EmployeeResponseDto> GetById(long id)
        {
            var emp = await _uow.Employees.GetEmployeeByIdAsync(id);
            return _mapper.Map<EmployeeResponseDto>(emp);
        }

        public async Task<EmployeeResponseDto> UpdateEmployee(UpdateEmployeeDto dto)
        {
            await _uow.Employees.UpdateEmployeeByIdAsync(dto.EmployeeId, dto);
            await _uow.SaveAsync();

            var updated = await _uow.Employees.GetEmployeeByIdAsync(dto.EmployeeId);
            return _mapper.Map<EmployeeResponseDto>(updated);
        }


        public async Task<bool> DeleteEmployee(long id)
        {
            await _uow.Employees.DeleteEmployeeAsync(id);
            await _uow.SaveAsync();
            return true;
        }
    }

}
