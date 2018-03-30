using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Dziennik_BSK.Migrations
{
    public partial class AddFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Student_StudentId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Teacher_TeacherId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Teacher_TeacherId",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "FK_Note_Student_StudentId",
                table: "Note");

            migrationBuilder.DropForeignKey(
                name: "FK_Note_Teacher_TeacherId",
                table: "Note");

            migrationBuilder.DropForeignKey(
                name: "FK_Parent_Responsibility_ChildId",
                table: "Parent");

            migrationBuilder.DropForeignKey(
                name: "FK_Participation_Lesson_LessonId",
                table: "Participation");

            migrationBuilder.DropForeignKey(
                name: "FK_Participation_Student_StudentId",
                table: "Participation");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Responsibility_ParentId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_ParentId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Parent_ChildId",
                table: "Parent");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "ChildId",
                table: "Parent");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Responsibility",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Responsibility",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Participation",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LessonId",
                table: "Participation",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "Note",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Note",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "Lesson",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "Grade",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Grade",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Responsibility_ParentId",
                table: "Responsibility",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Responsibility_StudentId",
                table: "Responsibility",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Student_StudentId",
                table: "Grade",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Teacher_TeacherId",
                table: "Grade",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Teacher_TeacherId",
                table: "Lesson",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Student_StudentId",
                table: "Note",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Teacher_TeacherId",
                table: "Note",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participation_Lesson_LessonId",
                table: "Participation",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participation_Student_StudentId",
                table: "Participation",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Responsibility_Parent_ParentId",
                table: "Responsibility",
                column: "ParentId",
                principalTable: "Parent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Responsibility_Student_StudentId",
                table: "Responsibility",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Student_StudentId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Teacher_TeacherId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Teacher_TeacherId",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "FK_Note_Student_StudentId",
                table: "Note");

            migrationBuilder.DropForeignKey(
                name: "FK_Note_Teacher_TeacherId",
                table: "Note");

            migrationBuilder.DropForeignKey(
                name: "FK_Participation_Lesson_LessonId",
                table: "Participation");

            migrationBuilder.DropForeignKey(
                name: "FK_Participation_Student_StudentId",
                table: "Participation");

            migrationBuilder.DropForeignKey(
                name: "FK_Responsibility_Parent_ParentId",
                table: "Responsibility");

            migrationBuilder.DropForeignKey(
                name: "FK_Responsibility_Student_StudentId",
                table: "Responsibility");

            migrationBuilder.DropIndex(
                name: "IX_Responsibility_ParentId",
                table: "Responsibility");

            migrationBuilder.DropIndex(
                name: "IX_Responsibility_StudentId",
                table: "Responsibility");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Responsibility");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Responsibility");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Student",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Participation",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "LessonId",
                table: "Participation",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ChildId",
                table: "Parent",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "Note",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Note",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "Lesson",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "Grade",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Grade",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Student_ParentId",
                table: "Student",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Parent_ChildId",
                table: "Parent",
                column: "ChildId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Student_StudentId",
                table: "Grade",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Teacher_TeacherId",
                table: "Grade",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Teacher_TeacherId",
                table: "Lesson",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Student_StudentId",
                table: "Note",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Teacher_TeacherId",
                table: "Note",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parent_Responsibility_ChildId",
                table: "Parent",
                column: "ChildId",
                principalTable: "Responsibility",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Participation_Lesson_LessonId",
                table: "Participation",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Participation_Student_StudentId",
                table: "Participation",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Responsibility_ParentId",
                table: "Student",
                column: "ParentId",
                principalTable: "Responsibility",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
