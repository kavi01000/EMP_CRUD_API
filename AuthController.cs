
using EmpList.Model;
using EmpList.ModelDTO;
using EmpList.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowCors")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpGet("claims")]
        [Authorize]
        public IActionResult GetClaims()
        {
            return Ok(User.Claims.Select(c => new { c.Type, c.Value }));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            // _auth.Login now returns LoginResponseDto
            var loginResponse = await _auth.Login(dto);

            if (loginResponse == null)
                return Unauthorized(new { message = "Invalid login credentials" });

            // Return token + user info
            return Ok(new
            {
                token = loginResponse.Token,
                fullName = loginResponse.FullName,
                email = loginResponse.Email,
                phoneNo = loginResponse.PhoneNo
            });
        }


        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var success = await _auth.ChangePassword(dto);

            if (!success)
                return BadRequest(new { message = "Incorrect username or old password." });

            return Ok(new { message = "Password changed successfully." });
        }




        [HttpPost("register")]
        public async Task<IActionResult> Register(registerDto dto)
        {
            // Call your auth service
            var result = await _auth.Register(dto);

            if (result)
            {
                return Ok(new { success = true, message = "Registration successful! Please login." });
            }
            else
            {
                return BadRequest(new { success = false, message = "Email already registered." });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _auth.Delete(id);

            if (!result)
                return BadRequest(new { message = "Delete failed." });

            return Ok(new { message = "User deleted successfully." });
        }




    }
}
