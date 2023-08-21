using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using TaskShare.Entities.Efos;
using TaskShare.Services.Services;

namespace TaskShare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UserController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        // GET api/users
        [HttpGet]
        [ProducesResponseType(typeof(List<UserEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<UserEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<UserEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<UserEfo>>> GetAllUsersAsync()
        {
            List<UserEfo> users = await _usersService.GetAllUsersAsync();

            return Ok(users);
        }

        // GET api/users/{userId}
        [Authorize]
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(UserEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetUserByIdAsync(int userId)
        {
            UserEfo user = await _usersService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [Obsolete]
        // GET api/users/byname/{username}
        [HttpGet("byname/{username}")]
        [ProducesResponseType(typeof(UserEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetUserByNameAsync(string username)
        {
            UserEfo user = await _usersService.GetUserByName(username);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT api/users/byname/{username}
        [HttpPut("{username}")]
        [ProducesResponseType(typeof(UserEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(UserEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateUserProfile(string username, [FromBody] UserEfo updateUser)
        {
            try
            {
                UserEfo user = await _usersService.UpdateUserProfile(username, updateUser);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/users/{userId}
        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteUserAync(int userId)
        {
            try
            {
                await _usersService.DeleteUserAync(userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
