using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dziennik_BSK.Models;

namespace Dziennik_BSK.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) :
            base(options)
        { }

        public DbSet<Grade> Grades { get; set; }
        public DbSet<Participation> Participations { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder mb) {
            mb.Entity<Grade>().ToTable("Grade");
            mb.Entity<Participation>().ToTable("Participation");
            mb.Entity<Lesson>().ToTable("Lesson");
            mb.Entity<Note>().ToTable("Note");
            mb.Entity<Parent>().ToTable("Parent");
            mb.Entity<Teacher>().ToTable("Teacher");
            mb.Entity<Student>().ToTable("Student");
        }
    }
}
