using backendAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace backendAPI.Database
{
	public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
	{
		public DbSet<PromptModel> Prompts { get; set; }
		public DbSet<PromptUrlModel> PromptUrls { get; set; }

	}
}
