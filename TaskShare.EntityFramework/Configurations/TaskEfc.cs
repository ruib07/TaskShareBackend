using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskShare.Entities.Efos;

namespace TaskShare.EntityFramework.Configurations
{
    public class TaskEfc : IEntityTypeConfiguration<TaskEfo>
    {
        public void Configure(EntityTypeBuilder<TaskEfo> builder)
        {
            builder.ToTable("Tasks");
            builder.HasKey(property => new { property.TaskId });
            builder.Property(property => property.TaskId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(property => property.Description).IsRequired().HasMaxLength(150).IsUnicode(false);
            builder.Property(property => property.CreatedAt).IsRequired();
            builder.Property(property => property.IsCompleted).IsRequired();
        }
    }
}
