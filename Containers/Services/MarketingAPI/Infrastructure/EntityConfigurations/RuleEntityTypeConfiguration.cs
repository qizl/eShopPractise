﻿using EnjoyCodes.eShopOnContainers.Services.MarketingAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnjoyCodes.eShopOnContainers.Services.MarketingAPI.Infrastructure.EntityConfigurations
{
    class RuleEntityTypeConfiguration
       : IEntityTypeConfiguration<Rule>
    {
        public void Configure(EntityTypeBuilder<Rule> builder)
        {
            builder.ToTable("Rule");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
               .ForSqlServerUseSequenceHiLo("rule_hilo")
               .IsRequired();

            builder.HasDiscriminator<int>("RuleTypeId")
                .HasValue<UserProfileRule>(RuleType.UserProfileRule.Id)
                .HasValue<PurchaseHistoryRule>(RuleType.PurchaseHistoryRule.Id)
                .HasValue<UserLocationRule>(RuleType.UserLocationRule.Id);

            builder.Property(r => r.Description)
                .HasColumnName("Description")
                .IsRequired();
        }
    }
}
