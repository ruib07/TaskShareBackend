using Microsoft.EntityFrameworkCore;
using TaskShare.Entities.Efos;
using TaskShare.EntityFramework;

namespace TaskShare.Services.Services
{
    public interface IRegisterUsersService
    {
        Task<List<RegisterUserEfo>> GetAllRegisterUsersAsync();
        Task<RegisterUserEfo> GetRegisterUserByIdAsync(int registerUserId);
        Task<RegisterUserEfo> SendRegisterUser(RegisterUserEfo registerUser);
        Task<RegisterUserEfo> SendLoginUser(string username, string password);
        Task<UserEfo> SendNewUserProfile(string username, string password, int registoUserId);
        Task DeleteRegisterUserAsync(int registerUserId);
    }

    public class RegisterUsersService : IRegisterUsersService
    {
        private readonly TaskShareDbContext _context;

        public RegisterUsersService(TaskShareDbContext context)
        {
            _context = context;
        }

        public async Task<List<RegisterUserEfo>> GetAllRegisterUsersAsync()
        {
            return await _context.RegisterUsers.ToListAsync();
        }

        public async Task<RegisterUserEfo> GetRegisterUserByIdAsync(int registerUserId)
        {
            RegisterUserEfo? registerUser = await _context.RegisterUsers.AsNoTracking().
                FirstOrDefaultAsync(ru => ru.RegisterUserId == registerUserId);

            if (registerUser == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            return registerUser;
        }

        public async Task<RegisterUserEfo> SendRegisterUser(RegisterUserEfo registerUser)
        {
            await _context.RegisterUsers.AddAsync(registerUser);
            await _context.SaveChangesAsync();

            return registerUser;
        }

        public async Task<RegisterUserEfo> SendLoginUser(string username, string password)
        {
           RegisterUserEfo? loginUser = await _context.RegisterUsers.FirstOrDefaultAsync(
               lu => lu.UserName == username && lu.Password == password);

            return loginUser;
        }

        public async Task<UserEfo> SendNewUserProfile(string username, string password, int registoUserId)
        {
            UserEfo newUserProfile = new UserEfo
            {
                UserName = username,
                Password = password,
                RegisterUserId = registoUserId
            };

            await _context.Users.AddAsync(newUserProfile);
            await _context.SaveChangesAsync();

            return newUserProfile;
        }

        public async Task DeleteRegisterUserAsync(int registerUserId)
        {
            RegisterUserEfo? entity = await _context.RegisterUsers.FirstOrDefaultAsync(
                e => e.RegisterUserId == registerUserId);

            if (entity == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            _context.RegisterUsers.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
