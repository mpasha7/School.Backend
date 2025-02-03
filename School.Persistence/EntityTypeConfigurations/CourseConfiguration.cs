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
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Id).IsUnique(); //.IsClustered();
            // TODO: Покрывающие индексы, включающие информацию только для ...ListQuery
            //builder.HasIndex(c => c.Id).IsUnique().IsClustered().IncludeProperties(c => new { c.Title, c.Description, c.PhotoPath });
            
            // TODO: Использовать fill_factor для снижения частоты реорганизации индексов

            builder.HasMany(c => c.Lessons)
                   .WithOne(les => les.Course)
                   .HasForeignKey(les => les.CourseId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Photo)
                   .WithOne(p => p.Course)
                   .HasForeignKey<FileObject>(p => p.CourseId) // Файл может относиться не только к курсу
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(c => c.Students)
                   .WithOne(s => s.Course)
                   .HasForeignKey(s => s.CourseId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Messages)
                   .WithOne(m => m.Course)
                   .HasForeignKey(m => m.CourseId)            // Сообщение может быть не связано с курсом
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(c => c.Applies)
                   .WithOne(a => a.Course)
                   .HasForeignKey(a => a.CourseId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Assessments)
                   .WithOne(a => a.Course)
                   .HasForeignKey(a => a.CourseId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Comments)
                   .WithOne(a => a.Course)
                   .HasForeignKey(a => a.CourseId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.Title).HasMaxLength(200);  // .IsRequired()
            //builder.Property(c => c.Description).IsRequired();
            //builder.Property(c => c.CreatedDate).HasDefaultValueSql("GETDATE()"); // Реализовано в ...Handler

            // TODO: Некластеризованные индексы для столбцов, которые часто используются в WHERE и JION
            //builder.HasIndex(c => c.CoachGuid);

            // TODO: Инициализация начальными значениями
            //builder.HasData();
        }
    }
}
