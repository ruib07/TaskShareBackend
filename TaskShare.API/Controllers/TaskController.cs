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
    public class TaskController : ControllerBase
    {
        private readonly ITasksService _tasksService;

        public TaskController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        // GET api/tasks
        [HttpGet]
        [ProducesResponseType(typeof(List<TaskEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<TaskEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<TaskEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<TaskEfo>>> GetAllTasksAsync()
        {
            List<TaskEfo> tasks = await _tasksService.GetAllTasksAsync();

            return Ok(tasks);
        }

        // GET api/tasks/{taskId}
        [Authorize]
        [HttpGet("{taskId}")]
        [ProducesResponseType(typeof(TaskEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TaskEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TaskEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(TaskEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetTaskByIdAsync(int taskId)
        {
            TaskEfo task = await _tasksService.GetTaskByIdAsync(taskId);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // POST api/tasks
        [HttpPost]
        [ProducesResponseType(typeof(TaskEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(TaskEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TaskEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(TaskEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<UserEfo>> SendTask([FromBody, Required] TaskEfo task)
        {
            if (ModelState.IsValid)
            {
                TaskEfo newUser = await _tasksService.SendTask(task);
                return StatusCode(StatusCodes.Status201Created, newUser);
            }

            return BadRequest(ModelState);
        }

        // PUT api/tasks/byid/{taskId}
        [HttpPut("{taskId}")]
        [ProducesResponseType(typeof(TaskEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(TaskEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TaskEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(TaskEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateTask(int taskId, TaskEfo updateTask)
        {
            try
            {
                TaskEfo task = await _tasksService.UpdateTask(taskId, updateTask);

                if (task == null)
                {
                    return NotFound();
                }

                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/tasks/{taskId}
        [HttpDelete("{taskId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteTaskAsync(int taskId)
        {
            try
            {
                await _tasksService.DeleteTaskAsync(taskId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
