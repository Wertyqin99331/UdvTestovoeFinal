using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VkPostAnalyzer.Data.Models;

namespace VkPostAnalyzer.Data.ModelConfigurations;

public class LetterStatisticConfiguration : IEntityTypeConfiguration<LetterStatistic>
{
	public void Configure(EntityTypeBuilder<LetterStatistic> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Statistics)
			.HasColumnType("jsonb");
		
		builder.Property(x => x.AnalysisDate)
			.IsRequired();
	}
}