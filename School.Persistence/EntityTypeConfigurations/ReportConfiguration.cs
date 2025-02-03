using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Persistence.EntityTypeConfigurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasIndex(r => r.Id).IsUnique();

            builder.HasMany(r => r.Files)
                   .WithOne(f => f.Report)
                   .HasForeignKey(f => f.ReportId) // Файлы могут относиться не только к отчету                   
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.Feedback)
                   .WithOne(fb => fb.Report)
                   .HasForeignKey<Feedback>(fb => fb.ReportId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
