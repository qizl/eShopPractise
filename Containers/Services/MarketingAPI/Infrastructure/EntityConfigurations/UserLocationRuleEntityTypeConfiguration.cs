using EnjoyCodes.eShopOnContainers.Services.MarketingAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnjoyCodes.eShopOnContainers.Services.MarketingAPI.Infrastructure.EntityConfigurations
{
    class UserLocationRuleEntityTypeConfiguration
       : IEntityTypeConfiguration<UserLocationRule>
    {
        public void Configure(EntityTypeBuilder<UserLocationRule> builder)
        {
            builder.Property(r => r.LocationId)
            .HasColumnName("LocationId")
            .IsRequired();
        }
    }
}
