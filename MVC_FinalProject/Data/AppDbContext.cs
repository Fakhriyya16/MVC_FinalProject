using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_FinalProject.Models;

namespace MVC_FinalProject.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Icon> Icons { get; set; }
        public DbSet<Information> Information { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseImage> CourseImages { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<InstructorSocial> InstructorSocials { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Social> Socials { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Slider>().HasQueryFilter(m => !m.SoftDeleted);
            builder.Entity<Information>().HasQueryFilter(m => !m.SoftDeleted);
            builder.Entity<Category>().HasQueryFilter(m => !m.SoftDeleted);
            builder.Entity<Course>().HasQueryFilter(m => !m.SoftDeleted);
            builder.Entity<Instructor>().HasQueryFilter(m => !m.SoftDeleted);
            builder.Entity<Setting>().HasQueryFilter(m => !m.SoftDeleted);
            builder.Entity<Social>().HasQueryFilter(m => !m.SoftDeleted);
            builder.Entity<Student>().HasQueryFilter(m => !m.SoftDeleted);
            base.OnModelCreating(builder);
        }
    }
}
