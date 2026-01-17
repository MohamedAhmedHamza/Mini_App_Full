
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MiniApp.Core.Entities;
using MiniApp.Core.Interfaces;
using MiniApp.Core.Mapping;
using MiniApp.Infrastructure.Data;
using MiniApp.Infrastructure.Data.Context;
using MiniApp.Infrastructure.Repositories;
using MiniApp.Infrastructure.UnitOfWork;
using MiniApp.Service.IServices;
using MiniApp.Service.Services;
using System.Text;
using System.Text.Json.Serialization;

namespace MiniApp.API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);



			var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

			// CORS
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", policy =>
				{
					policy.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500")
						  .AllowAnyHeader()
						  .AllowAnyMethod();
				});
			});





			// Add services to the container.

			builder.Services.AddControllers()
				.AddJsonOptions(options =>
				{
						options.JsonSerializerOptions.Converters.Add(
								new JsonStringEnumConverter()
						);
				});


			// Swagger
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
				{
					Title = "MiniApp API",
					Version = "v1"
				});

				c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = Microsoft.OpenApi.Models.ParameterLocation.Header,
					Description = "Enter: Bearer {your JWT token}"
				});

				c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
				{
				{
						new Microsoft.OpenApi.Models.OpenApiSecurityScheme
						{
				Reference = new Microsoft.OpenApi.Models.OpenApiReference
				{
					Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
						},
							new string[] {}
							}
				});
			});



			// DbContext
			builder.Services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
			builder.Services.AddAutoMapper(cfg =>
			{
				cfg.AddProfile<UserProfile>();
				cfg.AddProfile<TicketProfile>();
			});	


			// Services
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddScoped<ITicketService, TicketService>();
			builder.Services.AddScoped<IAuthService, AuthService>();
			builder.Services.AddScoped<IAdminService, AdminService>();

			// JWT Service
			builder.Services.AddScoped<IJwtService, JwtService>();





			// JWT Auth
			var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = builder.Configuration["Jwt:Issuer"],
					ValidAudience = builder.Configuration["Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(key)
				};
			});

			builder.Services.AddAuthorization();

			var app = builder.Build();

			using (var scope = app.Services.CreateScope())
			{
				var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
				await AppDbContextSeed.SeedAsync(context);
			}


			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI();
			}


			app.UseCors("CorsPolicy");
			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
