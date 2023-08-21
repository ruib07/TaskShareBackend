using Microsoft.EntityFrameworkCore;
using TaskShare.Entities.Efos;
using TaskShare.EntityFramework.Configurations;

namespace TaskShare.EntityFramework
{
    public class TaskShareDbContext : DbContext
    {
        public TaskShareDbContext(DbContextOptions<TaskShareDbContext> options) : base(options) { }

        public DbSet<RegisterUserEfo> RegisterUsers { get; set; }
        public DbSet<UserEfo> Users { get; set; }
        public DbSet<TaskEfo> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RegisterUserEfc());
            modelBuilder.ApplyConfiguration(new  UserEfc());
            modelBuilder.ApplyConfiguration(new TaskEfc());
        }
    }
}
