using Microsoft.Extensions.Options;
using MiniApp.Core.Entities;
using MiniApp.Infrastructure.Data.Context;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiniApp.Infrastructure.Data
{
	public class AppDbContextSeed
	{
		public static async Task SeedAsync(AppDbContext context)
		{
			
			if (context.Users.Count() == 0)
			{
				// Read Data From Json File
				var usersData = File.ReadAllText(@"..\\MiniApp.Infrastructure\\DataSeed\\Users.json");

				


				var options = new JsonSerializerOptions
				{
					Converters =
					{
						new JsonStringEnumConverter()
					}
				};

				var users = JsonSerializer.Deserialize<List<User>>(usersData, options);
				// Password Hashing
				if (users is not null && users.Count > 0)
				{
					foreach (var user in users)
					{
						user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
					}

					await context.Users.AddRangeAsync(users);
					await context.SaveChangesAsync();
				}
			}

			// ======= Tickets Seed =======
			if (context.Tickets.Count() == 0)
			{
				var ticketsData = File.ReadAllText(@"..\\MiniApp.Infrastructure\\DataSeed\\Tickets.json");

				var options = new JsonSerializerOptions
				{
					Converters =
					{
						new JsonStringEnumConverter()
					}
				};

				var tickets = JsonSerializer.Deserialize<List<Ticket>>(ticketsData, options);

				if (tickets is not null && tickets.Count > 0)
				{
					await context.Tickets.AddRangeAsync(tickets);
					await context.SaveChangesAsync();
				}
			}
		}
	}
}