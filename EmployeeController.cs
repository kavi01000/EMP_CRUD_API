using EmpList.ModelDTO;
using EmpList.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmpList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(CreateEmployeeDto dto)
        {
            var result = await _service.AddEmployee(dto);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            return Ok(await _service.GetById(id));
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(long id, UpdateEmployeeDto dto)
        {
            dto.EmployeeId = id;

            var result = await _service.UpdateEmployee(dto);

            if (result == null)
                return NotFound($"Employee with ID {id} not found");

            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            return Ok(await _service.DeleteEmployee(id));
        }
    }
}
