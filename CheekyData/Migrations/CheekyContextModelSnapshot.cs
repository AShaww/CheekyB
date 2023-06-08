﻿// <auto-generated />
using System;
using CheekyData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CheekyData.Migrations
{
    [DbContext(typeof(CheekyContext))]
    partial class CheekyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CheekyModels.Entities.Rating", b =>
                {
                    b.Property<int>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RatingId"));

                    b.Property<string>("RatingName")
                        .IsRequired()
                        .HasMaxLength(52)
                        .HasColumnType("nvarchar(52)");

                    b.HasKey("RatingId");

                    b.ToTable("Rating", (string)null);

                    b.HasData(
                        new
                        {
                            RatingId = 1,
                            RatingName = "1 - Awareness"
                        },
                        new
                        {
                            RatingId = 2,
                            RatingName = "2 - Novice"
                        },
                        new
                        {
                            RatingId = 3,
                            RatingName = "3 - Professional"
                        },
                        new
                        {
                            RatingId = 4,
                            RatingName = "4 - Expert"
                        },
                        new
                        {
                            RatingId = 5,
                            RatingName = "5 - Leading-edge expert"
                        },
                        new
                        {
                            RatingId = 6,
                            RatingName = "1 - Beginner"
                        },
                        new
                        {
                            RatingId = 7,
                            RatingName = "2 - Want to improve"
                        },
                        new
                        {
                            RatingId = 8,
                            RatingName = "3 - Proffesional"
                        },
                        new
                        {
                            RatingId = 9,
                            RatingName = "4 - Expert"
                        },
                        new
                        {
                            RatingId = 10,
                            RatingName = "5 - Leading-edge expert"
                        });
                });

            modelBuilder.Entity("CheekyModels.Entities.ScrapedNews", b =>
                {
                    b.Property<Guid>("NewsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NewsId");

                    b.ToTable("ScrapedNews", (string)null);

                    b.HasData(
                        new
                        {
                            NewsId = new Guid("fde34b5e-5b1a-46d6-9fe7-b6896cf86517"),
                            ImageUrl = "Pretend IMG URL",
                            PageUrl = "Pretend Page URL",
                            Title = "First Random Title for News"
                        });
                });

            modelBuilder.Entity("CheekyModels.Entities.Skill", b =>
                {
                    b.Property<Guid>("SkillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SkillName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SkillTypeId")
                        .HasColumnType("int");

                    b.HasKey("SkillId");

                    b.HasIndex("SkillTypeId");

                    b.ToTable("Skill", (string)null);

                    b.HasData(
                        new
                        {
                            SkillId = new Guid("ba5706bc-7e50-441d-93e1-8e14f7d09c76"),
                            SkillName = "Skill 1",
                            SkillTypeId = 1
                        },
                        new
                        {
                            SkillId = new Guid("ebdfc7bb-fba6-42a9-b51a-e04772449baa"),
                            SkillName = "Skill 2",
                            SkillTypeId = 2
                        },
                        new
                        {
                            SkillId = new Guid("fdf21334-a2e2-4d7d-9b53-9377cd648186"),
                            SkillName = "Skill 3",
                            SkillTypeId = 1
                        });
                });

            modelBuilder.Entity("CheekyModels.Entities.SkillType", b =>
                {
                    b.Property<int>("SkillTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SkillTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("SkillTypeId");

                    b.ToTable("SkillType", (string)null);

                    b.HasData(
                        new
                        {
                            SkillTypeId = 1,
                            Name = "Core"
                        },
                        new
                        {
                            SkillTypeId = 2,
                            Name = "Technical"
                        });
                });

            modelBuilder.Entity("CheekyModels.Entities.SkillTypeRating", b =>
                {
                    b.Property<int>("SkillTypeId")
                        .HasColumnType("int");

                    b.Property<int>("RatingId")
                        .HasColumnType("int");

                    b.HasKey("SkillTypeId", "RatingId");

                    b.HasIndex("RatingId");

                    b.ToTable("SkillTypeRating");
                });

            modelBuilder.Entity("CheekyModels.Entities.ToDo", b =>
                {
                    b.Property<Guid>("ToDoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ToDoDateModified")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("datetime2");

                    b.Property<string>("ToDoMessage")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("ToDoTitle")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ToDoId");

                    b.HasIndex("UserId");

                    b.ToTable("ToDo", (string)null);

                    b.HasData(
                        new
                        {
                            ToDoId = new Guid("fde34b5e-5b1a-46d6-9fe7-b6896cf86517"),
                            ToDoDateModified = new DateTime(2023, 6, 8, 20, 39, 1, 688, DateTimeKind.Utc).AddTicks(7760),
                            ToDoMessage = "Well there was one day the ended and the new day started",
                            ToDoTitle = "Something about something 1",
                            UserId = new Guid("830e9471-9d6e-4557-8bf5-ec89d375d933")
                        },
                        new
                        {
                            ToDoId = new Guid("59887cb4-62be-4d64-a7cf-70a608c84d7b"),
                            ToDoDateModified = new DateTime(2023, 6, 8, 20, 39, 1, 688, DateTimeKind.Utc).AddTicks(7765),
                            ToDoMessage = "Well there was one day the ended and the new day started",
                            ToDoTitle = "Something about something 2",
                            UserId = new Guid("830e9471-9d6e-4557-8bf5-ec89d375d933")
                        },
                        new
                        {
                            ToDoId = new Guid("f783e4e6-f492-4ecf-8362-fd4834ab37d7"),
                            ToDoDateModified = new DateTime(2023, 6, 8, 20, 39, 1, 688, DateTimeKind.Utc).AddTicks(7768),
                            ToDoMessage = "Well there was one day the ended and the new day started",
                            ToDoTitle = "Something about something 3",
                            UserId = new Guid("830e9471-9d6e-4557-8bf5-ec89d375d933")
                        },
                        new
                        {
                            ToDoId = new Guid("5fce3a3a-a421-4830-a49b-f8813d6d4fb9"),
                            ToDoDateModified = new DateTime(2023, 6, 8, 20, 39, 1, 688, DateTimeKind.Utc).AddTicks(7778),
                            ToDoMessage = "Well there was one day the ended and the new day started",
                            ToDoTitle = "Something about something",
                            UserId = new Guid("830e9471-9d6e-4557-8bf5-ec89d375d933")
                        });
                });

            modelBuilder.Entity("CheekyModels.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Archived")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("ArchivedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("GoogleUserId")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("LoginDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("830e9471-9d6e-4557-8bf5-ec89d375d933"),
                            Archived = false,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "AmirShaw@hotmail.co.uk",
                            FirstName = "Amir",
                            ModifiedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Surname = "Shaw"
                        });
                });

            modelBuilder.Entity("CheekyModels.Entities.UserSkill", b =>
                {
                    b.Property<Guid>("SkillId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastEvaluated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<int>("RatingId")
                        .HasColumnType("int");

                    b.HasKey("SkillId", "UserId");

                    b.HasIndex("RatingId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSkills");
                });

            modelBuilder.Entity("CheekyModels.Entities.Skill", b =>
                {
                    b.HasOne("CheekyModels.Entities.SkillType", "SkillType")
                        .WithMany("Skills")
                        .HasForeignKey("SkillTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SkillType");
                });

            modelBuilder.Entity("CheekyModels.Entities.SkillTypeRating", b =>
                {
                    b.HasOne("CheekyModels.Entities.Rating", "Ratings")
                        .WithMany("SkillTypeRating")
                        .HasForeignKey("RatingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CheekyModels.Entities.SkillType", "SkillType")
                        .WithMany("SkillTypeRating")
                        .HasForeignKey("SkillTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ratings");

                    b.Navigation("SkillType");
                });

            modelBuilder.Entity("CheekyModels.Entities.ToDo", b =>
                {
                    b.HasOne("CheekyModels.Entities.User", "User")
                        .WithMany("ToDos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CheekyModels.Entities.UserSkill", b =>
                {
                    b.HasOne("CheekyModels.Entities.Rating", "Rating")
                        .WithMany("UserSkills")
                        .HasForeignKey("RatingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CheekyModels.Entities.Skill", "Skill")
                        .WithMany("Users")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CheekyModels.Entities.User", "User")
                        .WithMany("UserSkills")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rating");

                    b.Navigation("Skill");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CheekyModels.Entities.Rating", b =>
                {
                    b.Navigation("SkillTypeRating");

                    b.Navigation("UserSkills");
                });

            modelBuilder.Entity("CheekyModels.Entities.Skill", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("CheekyModels.Entities.SkillType", b =>
                {
                    b.Navigation("SkillTypeRating");

                    b.Navigation("Skills");
                });

            modelBuilder.Entity("CheekyModels.Entities.User", b =>
                {
                    b.Navigation("ToDos");

                    b.Navigation("UserSkills");
                });
#pragma warning restore 612, 618
        }
    }
}
