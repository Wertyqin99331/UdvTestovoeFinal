using Microsoft.EntityFrameworkCore;
using VkPostAnalyzer.Data.Models;

namespace VkPostAnalyzer.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	public DbSet<LetterStatistic> LetterStatistics { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
	}
}