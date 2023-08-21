using Microsoft.EntityFrameworkCore;
using TaskShare.Entities.Efos;
using TaskShare.EntityFramework;

namespace TaskShare.Services.Services
{
    public interface IUsersService
    {
        Task<List<UserEfo>> GetAllUsersAsync();
        Task<UserEfo> GetUserByIdAsync(int userId);
        Task<UserEfo> GetUserByName(string username);
        Task<UserEfo> UpdateUserProfile(string username, UserEfo updateUser);
        Task DeleteUserAync(int userId);
    }

    public class UsersService : IUsersService
    {
        private readonly TaskShareDbContext _context;

        public UsersService(TaskShareDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserEfo>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<UserEfo> GetUserByIdAsync(int userId)
        {
            UserEfo? user = await _context.Users.AsNoTracking().
                FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            return user;
        }

        public async Task<UserEfo> GetUserByName(string username)
        {
            UserEfo? user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(
                c => c.UserName == username);

            if (user == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            return user;
        }

        public async Task<UserEfo> UpdateUserProfile(string username, UserEfo updateUser)
        {
            try
            {
                UserEfo? user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
                
                if (user == null)
                {
                    throw new Exception("Doesn´t exist in the database");
                }

                user.UserName = updateUser.UserName;
                user.Email = updateUser.Email;
                user.Password = updateUser.Password;
                user.ImageUrl = updateUser.ImageUrl;

                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating user: {ex.Message}");
            }
        }

        public async Task DeleteUserAync(int userId)
        {
            UserEfo? entity = await _context.Users.FirstOrDefaultAsync(
                e => e.UserId == userId);

            if (entity == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
