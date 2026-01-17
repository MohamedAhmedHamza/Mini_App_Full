using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniApp.Core.Entities;

namespace MiniApp.Infrastructure.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(u => u.Id);

			builder.Property(u => u.Username)
				   .IsRequired()
				   .HasMaxLength(50);

			builder.Property(u => u.PasswordHash)
				   .IsRequired();
			builder.Property(u => u.Role)
				   .HasConversion<string>()
				   .IsRequired();
	
			builder.HasMany(u => u.Tickets)
				   .WithOne(t => t.User)
				   .HasForeignKey(t => t.UserId)
				   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}

