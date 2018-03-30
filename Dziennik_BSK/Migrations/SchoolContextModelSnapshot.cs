﻿// <auto-generated />
using Dziennik_BSK.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Dziennik_BSK.Migrations
{
    [DbContext(typeof(SchoolContext))]
    partial class SchoolContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dziennik_BSK.Models.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddDate");

                    b.Property<string>("Comment")
                        .HasMaxLength(128);

                    b.Property<string>("Rate")
                        .IsRequired();

                    b.Property<int>("StudentId");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("TeacherId");

                    b.Property<int>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Grade");
                });

            modelBuilder.Entity("Dziennik_BSK.Models.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Class")
                        .IsRequired()
                        .HasMaxLength(3);

                    b.Property<DateTime>("LessonDate");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("TeacherId");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Lesson");
                });

            modelBuilder.Entity("Dziennik_BSK.Models.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddDate");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("IsNegative")
                        .IsRequired();

                    b.Property<int>("StudentId");

                    b.Property<int>("TeacherId");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Note");
                });

            modelBuilder.Entity("Dziennik_BSK.Models.Parent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(9);

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Parent");
                });

            modelBuilder.Entity("Dziennik_BSK.Models.Participation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("IsPresent")
                        .IsRequired();

                    b.Property<int>("LessonId");

                    b.Property<int>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.HasIndex("StudentId");

                    b.ToTable("Participation");
                });

            modelBuilder.Entity("Dziennik_BSK.Models.Responsibility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ParentId");

                    b.Property<int>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("StudentId");

                    b.ToTable("Responsibility");
                });

            modelBuilder.Entity("Dziennik_BSK.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("BuildingNumber")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Class")
                        .IsRequired()
                        .HasMaxLength(3);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("FlatNumber")
                        .HasMaxLength(4);

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.Property<string>("PostalCode")
                        .IsRequired();

                    b.Property<string>("SecendName")
                        .HasMaxLength(20);

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("Dziennik_BSK.Models.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(9);

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("Dziennik_BSK.Models.Grade", b =>
                {
                    b.HasOne("Dziennik_BSK.Models.Student", "Student")
                        .WithMany("Grades")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dziennik_BSK.Models.Teacher", "Teacher")
                        .WithMany("Grades")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dziennik_BSK.Models.Lesson", b =>
                {
                    b.HasOne("Dziennik_BSK.Models.Teacher", "Teacher")
                        .WithMany("Lessons")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dziennik_BSK.Models.Note", b =>
                {
                    b.HasOne("Dziennik_BSK.Models.Student", "Student")
                        .WithMany("Notes")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dziennik_BSK.Models.Teacher", "Teacher")
                        .WithMany("Notes")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dziennik_BSK.Models.Participation", b =>
                {
                    b.HasOne("Dziennik_BSK.Models.Lesson", "Lesson")
                        .WithMany("Participations")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dziennik_BSK.Models.Student", "Student")
                        .WithMany("Participations")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dziennik_BSK.Models.Responsibility", b =>
                {
                    b.HasOne("Dziennik_BSK.Models.Parent", "Parents")
                        .WithMany("Child")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dziennik_BSK.Models.Student", "Students")
                        .WithMany("Parent")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
