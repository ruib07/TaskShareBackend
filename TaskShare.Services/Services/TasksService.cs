using Microsoft.EntityFrameworkCore;
using TaskShare.Entities.Efos;
using TaskShare.EntityFramework;

namespace TaskShare.Services.Services
{
    public interface ITasksService
    {
        Task<List<TaskEfo>> GetAllTasksAsync();
        Task<TaskEfo> GetTaskByIdAsync(int taskId);
        Task<TaskEfo> SendTask(TaskEfo task);
        Task<TaskEfo> UpdateTask(int taskId, TaskEfo updateTask);
        Task DeleteTaskAsync(int taskId);
    }

    public class TasksService : ITasksService
    {
        private readonly TaskShareDbContext _context;

        public TasksService(TaskShareDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskEfo>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskEfo> GetTaskByIdAsync(int taskId)
        {
            TaskEfo? task = await _context.Tasks.AsNoTracking()
                .FirstOrDefaultAsync(t => t.TaskId == taskId);

            if (task == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            return task;
        }

        public async Task<TaskEfo> SendTask(TaskEfo task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<TaskEfo> UpdateTask(int taskId, TaskEfo updateTask)
        {
            try
            {
                TaskEfo? task = await _context.Tasks.FindAsync(taskId);

                if (task == null)
                {
                    throw new Exception("Doesn´t exist in the database");
                }

                task.Description = updateTask.Description;
                task.CreatedAt = updateTask.CreatedAt;
                task.IsCompleted = updateTask.IsCompleted;

                await _context.SaveChangesAsync();

                return task;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task DeleteTaskAsync(int taskId)
        {
            TaskEfo? entity = await _context.Tasks.FirstOrDefaultAsync(
                e => e.TaskId == taskId);

            if (entity == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            _context.Tasks.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
