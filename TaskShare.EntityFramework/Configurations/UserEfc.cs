﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskShare.Entities.Efos;

namespace TaskShare.EntityFramework.Configurations
{
    public class UserEfc : IEntityTypeConfiguration<UserEfo>
    {
        public void Configure(EntityTypeBuilder<UserEfo> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(property => new { property.UserId });
            builder.Property(property => property.UserId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(property => property.UserName).IsRequired().HasMaxLength(25).IsUnicode(false);
            builder.Property(property => property.Email).IsRequired().HasMaxLength(40).IsUnicode(false);
            builder.Property(property => property.Password).IsRequired().HasMaxLength(25).IsUnicode(false);
            builder.Property(property => property.ImageUrl).IsRequired().HasMaxLength(60).IsUnicode(false);
            builder.Property(property => property.RegisterUserId).IsRequired().ValueGeneratedOnAdd();

            builder.HasOne(property => property.RegisterUser)
                .WithMany()
                .HasForeignKey(property => property.RegisterUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
