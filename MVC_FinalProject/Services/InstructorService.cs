﻿using Microsoft.EntityFrameworkCore;
using MVC_FinalProject.Data;
using MVC_FinalProject.Models;
using MVC_FinalProject.Services.Interfaces;
using MVC_FinalProject.ViewModels.Categories;
using MVC_FinalProject.ViewModels.Courses;
using MVC_FinalProject.ViewModels.Instructors;

namespace MVC_FinalProject.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ISocialService _socialService;
        private readonly ICourseService _courseService;
        public InstructorService(AppDbContext context, IWebHostEnvironment env, ISocialService socialService)
        {
            _context = context;
            _env = env;
            _socialService = socialService;
        }

        public async Task Create(Instructor instructor)
        {
            await _context.Instructors.AddAsync(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Instructor instructor)
        {
            instructor.SoftDeleted = true;
            _context.Instructors.Update(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Instructor instructor, InstructorEditVM request)
        {
            instructor.FullName = request.FullName;
            instructor.Email = request.Email;
            instructor.Position = request.Position;
            var existImage = instructor.Image;

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
                instructor.Image = fileName;
            }

            await _socialService.UpdateSocialLinks(instructor, request);
            await _socialService.UpdateSocialLinks(instructor, request);
            await _socialService.UpdateSocialLinks(instructor, request);

            _context.Instructors.Update(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistEmail(string email)
        {
            return await _context.Instructors.AnyAsync(x => x.Email == email);
        }

        public async Task<List<InstructorTableVM>> GetAllForTable()
        {
            var data = await _context.Instructors.Include(m => m.Courses).ToListAsync();
            List<InstructorTableVM> vm = data.Select(m=> new InstructorTableVM()
            {
                Id = m.Id,
                Image = m.Image,
                FullName = m.FullName,
                Email = m.Email,                
                Courses = String.Join(", ", m.Courses.Select(c => c.Name))
            }).ToList();

            return vm;
        }

        public async Task<List<InstructorVM>> GetAllVM()
        {
            var data = await _context.Instructors
                .Include(m => m.InstructorSocials)
                .ThenInclude(m => m.Social)
                .ToListAsync();

            List<InstructorVM> result = data.Select(m =>
            {
                var instructorSocials = m.InstructorSocials
                    .GroupBy(isocial => isocial.Social.Name)
                    .Select(group => group.First())
                    .ToDictionary(isocial => isocial.Social.Name, isocial => isocial.SocialURL);

                return new InstructorVM
                {
                    Name = m.FullName,
                    Image = m.Image,
                    Position = m.Position,
                    Instagram = instructorSocials.ContainsKey("Instagram") ? instructorSocials["Instagram"] : null,
                    Facebook = instructorSocials.ContainsKey("Facebook") ? instructorSocials["Facebook"] : null,
                    Twitter = instructorSocials.ContainsKey("Twitter") ? instructorSocials["Twitter"] : null
                };
            }).ToList();

            return result;
        }

        public async Task<Instructor> GetById(int id)
        {
            return await _context.Instructors.Include(m=>m.Courses).Include(m=>m.InstructorSocials).ThenInclude(m=>m.Social).FirstOrDefaultAsync(m=>m.Id == id);
        }

        public async Task<InstructorDetailVM> GetInstructorDetailVM(int id)
        {
            var data = await _context.Instructors.Include(m => m.Courses).FirstOrDefaultAsync(m => m.Id == id);
            var courses = data.Courses.Select(m=>m.Name).ToList();
            string joinedCourses = String.Join(", ", courses);
            InstructorDetailVM vm = new()
            {
                Image = data.Image,
                FullName = data.FullName,
                Email = data.Email,
                Courses = joinedCourses,
                Position = data.Position,
                CreatedDate = data.CreatedDate.ToString("dd.MM.yyyy")
            };
            return vm;
        }
    }
}
