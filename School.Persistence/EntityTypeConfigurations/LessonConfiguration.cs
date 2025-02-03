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
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasKey(les => les.Id);
            builder.HasIndex(les => les.Id).IsUnique(); //.IsClustered();
            // TODO: Покрывающие индексы, включающие информацию только для ...ListQuery
            //builder.HasIndex(les => les.Id).IsUnique().IsClustered().IncludeProperties(les => new { les.Number, les.Title });
            // TODO: Использовать fill_factor для снижения частоты реорганизации индексов

            builder.HasMany(les => les.Files)
                   .WithOne(f => f.Lesson)
                   .HasForeignKey(f => f.LessonId) // Файлы могут относиться не только к уроку
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(les => les.Reports)
                   .WithOne(r => r.Lesson)
                   .HasForeignKey(r => r.LessonId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            //builder.Property(les => les.Number).IsRequired();
            builder.Property(les => les.Title).HasMaxLength(200);   // .IsRequired()

            // TODO: Некластеризованные индексы для столбцов, которые часто используются в WHERE и JION
            // builder.HasIndex(les => les.CourseId);

            // TODO: Инициализация начальными значениями
            //builder.HasData();
        }
    }
}
