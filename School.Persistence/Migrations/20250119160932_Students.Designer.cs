﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using School.Persistence;

#nullable disable

namespace School.Persistence.Migrations
{
    [DbContext(typeof(SchoolDbContext))]
    [Migration("20250119160932_Students")]
    partial class Students
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("School.Domain.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BeginQuestionnaire")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoachGuid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndQuestionnaire")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PublicDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CoachGuid = "3a3b611c-1185-445d-99c5-7f347675ec6e",
                            CreatedDate = new DateTime(2025, 1, 19, 21, 9, 29, 383, DateTimeKind.Local).AddTicks(7282),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                            PublicDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                            ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                            Title = "Гимнастика на шею"
                        },
                        new
                        {
                            Id = 2,
                            CoachGuid = "0b869d28-ab51-4310-ba0c-a3934e1de6de",
                            CreatedDate = new DateTime(2025, 1, 19, 21, 9, 29, 390, DateTimeKind.Local).AddTicks(5056),
                            Description = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem",
                            PublicDescription = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem",
                            ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                            Title = "Йога кундалини"
                        },
                        new
                        {
                            Id = 3,
                            CoachGuid = "3a3b611c-1185-445d-99c5-7f347675ec6e",
                            CreatedDate = new DateTime(2025, 1, 19, 21, 9, 29, 390, DateTimeKind.Local).AddTicks(5136),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                            PublicDescription = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem",
                            ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                            Title = "Гимнастика на стопы"
                        },
                        new
                        {
                            Id = 4,
                            CoachGuid = "3a3b611c-1185-445d-99c5-7f347675ec6e",
                            CreatedDate = new DateTime(2025, 1, 19, 21, 9, 29, 390, DateTimeKind.Local).AddTicks(5140),
                            Description = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem",
                            PublicDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                            ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                            Title = "Нейрогимнастика"
                        });
                });

            modelBuilder.Entity("School.Domain.FileObject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileNameExt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FileOwner")
                        .HasColumnType("int");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<int>("FileType")
                        .HasColumnType("int");

                    b.Property<string>("UniqueFileName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId")
                        .IsUnique();

                    b.ToTable("Files");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CourseId = 1,
                            CreatedAt = new DateTime(2025, 1, 19, 21, 9, 29, 392, DateTimeKind.Local).AddTicks(5795),
                            FileNameExt = ".jpg",
                            FileOwner = 0,
                            FileSize = 1000L,
                            FileType = 0,
                            UniqueFileName = "Гимнастика на шею.jpg"
                        },
                        new
                        {
                            Id = 2,
                            CourseId = 2,
                            CreatedAt = new DateTime(2025, 1, 19, 21, 9, 29, 392, DateTimeKind.Local).AddTicks(9676),
                            FileNameExt = ".jpg",
                            FileOwner = 0,
                            FileSize = 1000L,
                            FileType = 0,
                            UniqueFileName = "Йога кундалини.jpg"
                        },
                        new
                        {
                            Id = 3,
                            CourseId = 3,
                            CreatedAt = new DateTime(2025, 1, 19, 21, 9, 29, 392, DateTimeKind.Local).AddTicks(9688),
                            FileNameExt = ".jpg",
                            FileOwner = 0,
                            FileSize = 1000L,
                            FileType = 0,
                            UniqueFileName = "Гимнастика на стопы.jpg"
                        },
                        new
                        {
                            Id = 4,
                            CourseId = 4,
                            CreatedAt = new DateTime(2025, 1, 19, 21, 9, 29, 392, DateTimeKind.Local).AddTicks(9691),
                            FileNameExt = ".jpg",
                            FileOwner = 0,
                            FileSize = 1000L,
                            FileType = 0,
                            UniqueFileName = "Нейрогимнастика.jpg"
                        });
                });

            modelBuilder.Entity("School.Domain.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Number")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("VideoLink")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Lessons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CourseId = 1,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                            Number = 1,
                            Title = "Первый урок",
                            VideoLink = "https://vk.com/video_ext.php?oid=54023064&id=456239088&hd=2&hash=ec3da24f3c3555ea"
                        },
                        new
                        {
                            Id = 2,
                            CourseId = 1,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                            Number = 2,
                            Title = "Второй урок",
                            VideoLink = "https://vk.com/video_ext.php?oid=54023064&id=456239088&hd=2&hash=ec3da24f3c3555ea"
                        },
                        new
                        {
                            Id = 3,
                            CourseId = 1,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                            Number = 3,
                            Title = "Третий урок",
                            VideoLink = "https://vk.com/video_ext.php?oid=54023064&id=456239088&hd=2&hash=ec3da24f3c3555ea"
                        },
                        new
                        {
                            Id = 4,
                            CourseId = 2,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                            Number = 1,
                            Title = "Первый урок",
                            VideoLink = "https://vk.com/video_ext.php?oid=54023064&id=456239088&hd=2&hash=ec3da24f3c3555ea"
                        },
                        new
                        {
                            Id = 5,
                            CourseId = 2,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                            Number = 2,
                            Title = "Второй урок",
                            VideoLink = "https://vk.com/video_ext.php?oid=54023064&id=456239088&hd=2&hash=ec3da24f3c3555ea"
                        },
                        new
                        {
                            Id = 6,
                            CourseId = 2,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                            Number = 3,
                            Title = "Третий урок",
                            VideoLink = "https://vk.com/video_ext.php?oid=54023064&id=456239088&hd=2&hash=ec3da24f3c3555ea"
                        },
                        new
                        {
                            Id = 7,
                            CourseId = 3,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                            Number = 1,
                            Title = "Первый урок",
                            VideoLink = "https://vk.com/video_ext.php?oid=54023064&id=456239088&hd=2&hash=ec3da24f3c3555ea"
                        },
                        new
                        {
                            Id = 8,
                            CourseId = 3,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                            Number = 2,
                            Title = "Второй урок",
                            VideoLink = "https://vk.com/video_ext.php?oid=54023064&id=456239088&hd=2&hash=ec3da24f3c3555ea"
                        },
                        new
                        {
                            Id = 9,
                            CourseId = 3,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                            Number = 3,
                            Title = "Третий урок",
                            VideoLink = "https://vk.com/video_ext.php?oid=54023064&id=456239088&hd=2&hash=ec3da24f3c3555ea"
                        },
                        new
                        {
                            Id = 10,
                            CourseId = 4,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                            Number = 1,
                            Title = "Первый урок",
                            VideoLink = "https://vk.com/video_ext.php?oid=54023064&id=456239088&hd=2&hash=ec3da24f3c3555ea"
                        },
                        new
                        {
                            Id = 11,
                            CourseId = 4,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                            Number = 2,
                            Title = "Второй урок",
                            VideoLink = "https://vk.com/video_ext.php?oid=54023064&id=456239088&hd=2&hash=ec3da24f3c3555ea"
                        },
                        new
                        {
                            Id = 12,
                            CourseId = 4,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                            Number = 3,
                            Title = "Третий урок",
                            VideoLink = "https://vk.com/video_ext.php?oid=54023064&id=456239088&hd=2&hash=ec3da24f3c3555ea"
                        });
                });

            modelBuilder.Entity("School.Domain.StudentOfCourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("StudentGuid")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CourseId = 2,
                            StudentGuid = "77be0187-1d57-42dd-8d76-145c36c51bed"
                        },
                        new
                        {
                            Id = 2,
                            CourseId = 3,
                            StudentGuid = "77be0187-1d57-42dd-8d76-145c36c51bed"
                        },
                        new
                        {
                            Id = 3,
                            CourseId = 4,
                            StudentGuid = "77be0187-1d57-42dd-8d76-145c36c51bed"
                        },
                        new
                        {
                            Id = 4,
                            CourseId = 3,
                            StudentGuid = "acc53bf2-c3f6-442b-99c0-da2cf971516e"
                        });
                });

            modelBuilder.Entity("School.Domain.FileObject", b =>
                {
                    b.HasOne("School.Domain.Course", "Course")
                        .WithOne("Photo")
                        .HasForeignKey("School.Domain.FileObject", "CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("School.Domain.Lesson", b =>
                {
                    b.HasOne("School.Domain.Course", "Course")
                        .WithMany("Lessons")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("School.Domain.StudentOfCourse", b =>
                {
                    b.HasOne("School.Domain.Course", "Course")
                        .WithMany("Students")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("School.Domain.Course", b =>
                {
                    b.Navigation("Lessons");

                    b.Navigation("Photo");

                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
