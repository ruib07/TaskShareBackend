using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using TaskShare.Entities.Efos;
using TaskShare.Services.Services;

namespace TaskShare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUserController : ControllerBase
    {
        private readonly IRegisterUsersService _registerUsersService;

        public RegisterUserController(IRegisterUsersService registerUsersService)
        {
            _registerUsersService = registerUsersService;
        }

        // GET api/registerusers
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<RegisterUserEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<RegisterUserEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<RegisterUserEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<RegisterUserEfo>>> GetAllRegisterUsersAsync()
        {
            List<RegisterUserEfo> registerUsers = await _registerUsersService.GetAllRegisterUsersAsync();

            return Ok(registerUsers);
        }

        // GET api/registerusers/byid/{registerUserId}
        [Authorize]
        [HttpGet("byid/{registerUserId}")]
        [ProducesResponseType(typeof(RegisterUserEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegisterUserEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RegisterUserEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterUserEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetRegisterUserByIdAsync(int registerUserId)
        {
            RegisterUserEfo? registeruser = await _registerUsersService.GetRegisterUserByIdAsync(registerUserId);

            if (registeruser == null)
            {
                return NotFound();
            }

            return Ok(registeruser);
        }

        // POST api/registerusers
        [HttpPost]
        [ProducesResponseType(typeof(RegisterUserEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RegisterUserEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RegisterUserEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterUserEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<RegisterUserEfo>> SendRegisterUser([FromBody, Required] RegisterUserEfo registerUser)
        {
            if (ModelState.IsValid)
            {
                RegisterUserEfo newRegistoUser = await _registerUsersService.SendRegisterUser(registerUser);

                UserEfo newUserProfile = await _registerUsersService.SendNewUserProfile(registerUser.UserName, registerUser.Password, registerUser.RegisterUserId);

                return StatusCode(StatusCodes.Status201Created, newRegistoUser);
            }

            return BadRequest(ModelState);
        }

        // POST api/registerusers/sendlogin
        [HttpPost("sendlogin")]
        [ProducesResponseType(typeof(RegisterUserEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RegisterUserEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RegisterUserEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterUserEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<RegisterUserEfo>> SendLoginUser([FromBody, Required] RegisterUserEfo registerUser)
        {
            RegisterUserEfo loginUser = await _registerUsersService.SendLoginUser(registerUser.UserName, registerUser.Password);

            if (loginUser != null)
            {
                return Ok(loginUser);
            }

            return Unauthorized();
        }

        // DELETE api/registerusers/{registerUserId}
        [Authorize]
        [HttpDelete("{registerUserId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteRegisterUserAsync(int registerUserId)
        {
            try
            {
                await _registerUsersService.DeleteRegisterUserAsync(registerUserId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
