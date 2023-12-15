﻿// <auto-generated />
using System;
using Kursach.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Kursach.Migrations
{
    [DbContext(typeof(KursachContext))]
    [Migration("20231215120807_JournalFix")]
    partial class JournalFix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GroupSubject", b =>
                {
                    b.Property<int>("GroupsgroupID")
                        .HasColumnType("int");

                    b.Property<int>("SubjectssubjectID")
                        .HasColumnType("int");

                    b.HasKey("GroupsgroupID", "SubjectssubjectID");

                    b.HasIndex("SubjectssubjectID");

                    b.ToTable("GroupSubject");
                });

            modelBuilder.Entity("GroupTeacher", b =>
                {
                    b.Property<int>("GroupsgroupID")
                        .HasColumnType("int");

                    b.Property<int>("TeachersteacherID")
                        .HasColumnType("int");

                    b.HasKey("GroupsgroupID", "TeachersteacherID");

                    b.HasIndex("TeachersteacherID");

                    b.ToTable("GroupTeacher");
                });

            modelBuilder.Entity("Kursach.Models.Addon", b =>
                {
                    b.Property<int>("addonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("addonID"));

                    b.Property<string>("addonDescription")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("addonHeader")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("lessonID")
                        .HasColumnType("int");

                    b.HasKey("addonID");

                    b.HasIndex("lessonID");

                    b.ToTable("Addons");
                });

            modelBuilder.Entity("Kursach.Models.Authorization", b =>
                {
                    b.Property<int>("authToken")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("authToken"));

                    b.Property<string>("login")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.HasKey("authToken");

                    b.ToTable("Authorizations");
                });

            modelBuilder.Entity("Kursach.Models.Employee", b =>
                {
                    b.Property<int>("employeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("employeeID"));

                    b.Property<int?>("authToken")
                        .HasColumnType("int");

                    b.Property<string>("contactMailEmployee")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("contactPhoneEmployee")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fullNameEmployee")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("jobID")
                        .HasColumnType("int");

                    b.HasKey("employeeID");

                    b.HasIndex("authToken");

                    b.HasIndex("jobID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Kursach.Models.Event", b =>
                {
                    b.Property<int>("eventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("eventID"));

                    b.Property<DateTime>("eventDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("eventDescription")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("eventHeader")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("eventID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Kursach.Models.Group", b =>
                {
                    b.Property<int>("groupID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("groupID"));

                    b.Property<string>("groupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("groupID");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Kursach.Models.JobTitle", b =>
                {
                    b.Property<int>("jobID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("jobID"));

                    b.Property<string>("jobName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("salary")
                        .HasColumnType("float");

                    b.HasKey("jobID");

                    b.ToTable("JobTitles");
                });

            modelBuilder.Entity("Kursach.Models.Journal", b =>
                {
                    b.Property<int>("groupID")
                        .HasColumnType("int");

                    b.Property<int>("studentID")
                        .HasColumnType("int");

                    b.Property<int>("lessonID")
                        .HasColumnType("int");

                    b.Property<string>("mark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("rating")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("teacherID")
                        .HasColumnType("int");

                    b.HasKey("groupID", "studentID");

                    b.HasIndex("lessonID");

                    b.HasIndex("studentID");

                    b.HasIndex("teacherID");

                    b.ToTable("Journal");
                });

            modelBuilder.Entity("Kursach.Models.Lesson", b =>
                {
                    b.Property<int>("lessonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("lessonID"));

                    b.Property<string>("classroom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("groupID")
                        .HasColumnType("int");

                    b.Property<int?>("subjectID")
                        .HasColumnType("int");

                    b.Property<int?>("teacherID")
                        .HasColumnType("int");

                    b.HasKey("lessonID");

                    b.HasIndex("groupID");

                    b.HasIndex("subjectID");

                    b.HasIndex("teacherID");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("Kursach.Models.Payment", b =>
                {
                    b.Property<int>("paymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("paymentID"));

                    b.Property<double>("paymentCost")
                        .HasColumnType("float");

                    b.Property<DateTime>("paymentDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("paymentDirection")
                        .HasColumnType("bit");

                    b.Property<string>("paymentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("studentID")
                        .HasColumnType("int");

                    b.HasKey("paymentID");

                    b.HasIndex("studentID");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Kursach.Models.Student", b =>
                {
                    b.Property<int>("studentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("studentID"));

                    b.Property<DateTime>("age")
                        .HasColumnType("datetime2");

                    b.Property<int?>("authToken")
                        .HasColumnType("int");

                    b.Property<bool>("budget")
                        .HasColumnType("bit");

                    b.Property<string>("contactMailStudent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("contactPhoneStudent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("enrollmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("fullNameStudent")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("gender")
                        .HasColumnType("bit");

                    b.Property<int?>("groupID")
                        .HasColumnType("int");

                    b.HasKey("studentID");

                    b.HasIndex("authToken");

                    b.HasIndex("groupID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Kursach.Models.Subject", b =>
                {
                    b.Property<int>("subjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("subjectID"));

                    b.Property<string>("subjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("subjectID");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Kursach.Models.Teacher", b =>
                {
                    b.Property<int>("teacherID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("teacherID"));

                    b.Property<int?>("authToken")
                        .HasColumnType("int");

                    b.Property<string>("contactMailTeacher")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("contactPhoneTeacher")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fullNameTeacher")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("jobID")
                        .HasColumnType("int");

                    b.HasKey("teacherID");

                    b.HasIndex("authToken");

                    b.HasIndex("jobID");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("SubjectTeacher", b =>
                {
                    b.Property<int>("SubjectssubjectID")
                        .HasColumnType("int");

                    b.Property<int>("TeachersteacherID")
                        .HasColumnType("int");

                    b.HasKey("SubjectssubjectID", "TeachersteacherID");

                    b.HasIndex("TeachersteacherID");

                    b.ToTable("SubjectTeacher");
                });

            modelBuilder.Entity("GroupSubject", b =>
                {
                    b.HasOne("Kursach.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsgroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kursach.Models.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectssubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GroupTeacher", b =>
                {
                    b.HasOne("Kursach.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsgroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kursach.Models.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeachersteacherID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Kursach.Models.Addon", b =>
                {
                    b.HasOne("Kursach.Models.Lesson", "Lesson")
                        .WithMany()
                        .HasForeignKey("lessonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("Kursach.Models.Employee", b =>
                {
                    b.HasOne("Kursach.Models.Authorization", "Authorizations")
                        .WithMany()
                        .HasForeignKey("authToken");

                    b.HasOne("Kursach.Models.JobTitle", "JobTitles")
                        .WithMany()
                        .HasForeignKey("jobID");

                    b.Navigation("Authorizations");

                    b.Navigation("JobTitles");
                });

            modelBuilder.Entity("Kursach.Models.Journal", b =>
                {
                    b.HasOne("Kursach.Models.Group", "Group")
                        .WithMany()
                        .HasForeignKey("groupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kursach.Models.Lesson", "Lessons")
                        .WithMany()
                        .HasForeignKey("lessonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kursach.Models.Student", "Students")
                        .WithMany("Journal")
                        .HasForeignKey("studentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kursach.Models.Teacher", null)
                        .WithMany("Journal")
                        .HasForeignKey("teacherID");

                    b.Navigation("Group");

                    b.Navigation("Lessons");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("Kursach.Models.Lesson", b =>
                {
                    b.HasOne("Kursach.Models.Group", "Group")
                        .WithMany("Lessons")
                        .HasForeignKey("groupID");

                    b.HasOne("Kursach.Models.Subject", "Subject")
                        .WithMany("Lessons")
                        .HasForeignKey("subjectID");

                    b.HasOne("Kursach.Models.Teacher", "Teacher")
                        .WithMany("Lessons")
                        .HasForeignKey("teacherID");

                    b.Navigation("Group");

                    b.Navigation("Subject");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Kursach.Models.Payment", b =>
                {
                    b.HasOne("Kursach.Models.Student", "Student")
                        .WithMany("Payments")
                        .HasForeignKey("studentID");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Kursach.Models.Student", b =>
                {
                    b.HasOne("Kursach.Models.Authorization", "Authorizations")
                        .WithMany()
                        .HasForeignKey("authToken");

                    b.HasOne("Kursach.Models.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("groupID");

                    b.Navigation("Authorizations");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Kursach.Models.Teacher", b =>
                {
                    b.HasOne("Kursach.Models.Authorization", "Authorizations")
                        .WithMany()
                        .HasForeignKey("authToken");

                    b.HasOne("Kursach.Models.JobTitle", "JobTitles")
                        .WithMany()
                        .HasForeignKey("jobID");

                    b.Navigation("Authorizations");

                    b.Navigation("JobTitles");
                });

            modelBuilder.Entity("SubjectTeacher", b =>
                {
                    b.HasOne("Kursach.Models.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectssubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kursach.Models.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeachersteacherID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Kursach.Models.Group", b =>
                {
                    b.Navigation("Lessons");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("Kursach.Models.Student", b =>
                {
                    b.Navigation("Journal");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Kursach.Models.Subject", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("Kursach.Models.Teacher", b =>
                {
                    b.Navigation("Journal");

                    b.Navigation("Lessons");
                });
#pragma warning restore 612, 618
        }
    }
}
