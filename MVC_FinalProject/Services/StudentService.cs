using Microsoft.EntityFrameworkCore;
using MVC_FinalProject.Data;
using MVC_FinalProject.Models;
using MVC_FinalProject.Services.Interfaces;
using MVC_FinalProject.ViewModels.Courses;
using MVC_FinalProject.ViewModels.Instructors;
using MVC_FinalProject.ViewModels.Students;

namespace MVC_FinalProject.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public StudentService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task Create(Student student, List<int> courses)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            int studentId = student.Id;

            List<CourseStudent> courseStudents = courses.Select(courseId => new CourseStudent
            {
                StudentId = studentId,
                CourseId = courseId
            }).ToList();

            _context.CourseStudents.AddRange(courseStudents);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Student student)
        {
            student.SoftDeleted = true;
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Student student, StudentEditVM request)
        {
            student.FullName = request.FullName;
            student.Bio = request.Bio;
            student.CourseStudents = request.CourseIds.Select(courseId => new CourseStudent
            {
                StudentId = student.Id,
                CourseId = courseId
            }).ToList();
            var existImage = student.Image;

            if (request.NewImage is not null)
            {
                string path = Path.Combine(_env.WebRootPath, "assets/img", existImage);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                string fileName = Guid.NewGuid().ToString() + request.NewImage.FileName;

                path = Path.Combine(_env.WebRootPath, "assets/img", fileName);

                using (FileStream stream = new(path, FileMode.Create))
                {
                    await request.NewImage.CopyToAsync(stream);
                }
                student.Image = fileName;
            }

            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task<List<StudentTableVM>> GetAllForTable()
        {
            var data = await _context.Students.Include(m=>m.CourseStudents).ThenInclude(m=>m.Course).ToListAsync();
            List<StudentTableVM> studentTableVM = data.Select(m=> new StudentTableVM
            {
                Id = m.Id,
                FullName = m.FullName,
                Bio = m.Bio,
                Image = m.Image,
                Courses = String.Join(",",m.CourseStudents.Select(m=>m.Course.Name))
            }).ToList();

            return studentTableVM;
        }

        public async Task<List<StudentVM>> GetAllVM()
        {
            var data = await _context.Students.ToListAsync();
            List<StudentVM> result = data.Select(m => new StudentVM
            {
                FullName = m.FullName,
                Bio = m.Bio,
                Image = m.Image,
            }).ToList();

            return result;
        }

        public async Task<Student> GetById(int id)
        {
            return await _context.Students.Include(m=>m.CourseStudents).ThenInclude(m=>m.Course).FirstOrDefaultAsync(m=>m.Id == id);
        }
    }
}
