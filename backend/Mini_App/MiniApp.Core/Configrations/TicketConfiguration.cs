	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using MiniApp.Core.Entities;

	namespace MiniApp.Infrastructure.Configurations
	{
		public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
		{
			public void Configure(EntityTypeBuilder<Ticket> builder)
			{
				builder.HasKey(t => t.Id);

				builder.Property(t => t.Title)
					   .IsRequired()
					   .HasMaxLength(100);

				builder.Property(t => t.Description)
					   .IsRequired()
					   .HasMaxLength(1000);
				builder.Property(t => t.Status)
					   .HasConversion<string>()
					   .IsRequired();
			}
		}
	}
