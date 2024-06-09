using Microsoft.EntityFrameworkCore;
using MVC_FinalProject.Data;
using MVC_FinalProject.Models;
using MVC_FinalProject.Services.Interfaces;

namespace MVC_FinalProject.Services
{
    public class SocialService : ISocialService
    {
        private readonly AppDbContext _appDbContext;
        public SocialService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddSocialLink(Instructor instructor, string socialName, string socialUrl)
        {
            if (instructor == null || string.IsNullOrWhiteSpace(socialUrl))
            {
                return;
            }

            Social social = await GetOrCreateSocialByName(socialName);

            InstructorSocial instructorSocial = new InstructorSocial
            {
                Social = social,
                SocialURL = socialUrl,
                Instructor = instructor
            };

            instructor.InstructorSocials ??= new List<InstructorSocial>(); // Ensure the collection is initialized
            instructor.InstructorSocials.Add(instructorSocial);
        }

        public async Task UpdateSocialLink(Instructor instructor, string socialName, string socialUrl)
        {
            if (instructor == null)
            {
                return;
            }

            var instructorSocial = instructor.InstructorSocials
                ?.FirstOrDefault(isocial => isocial.Social.Name == socialName);

            if (string.IsNullOrWhiteSpace(socialUrl))
            {
                if (instructorSocial != null)
                {
                    _appDbContext.InstructorSocials.Remove(instructorSocial);
                }
            }
            else
            {
                if (instructorSocial != null)
                {
                    instructorSocial.SocialURL = socialUrl;
                }
                else
                {
                    Social social = await GetOrCreateSocialByName(socialName);
                    instructorSocial = new InstructorSocial
                    {
                        Social = social,
                        SocialURL = socialUrl,
                        Instructor = instructor
                    };
                    instructor.InstructorSocials ??= new List<InstructorSocial>(); // Ensure the collection is initialized
                    instructor.InstructorSocials.Add(instructorSocial);
                }
            }
        }

        public async Task<Social> GetOrCreateSocialByName(string name)
        {
            Social social = await _appDbContext.Socials.FirstOrDefaultAsync(s => s.Name == name);
            if (social == null)
            {
                social = new Social { Name = name };
                await _appDbContext.Socials.AddAsync(social);
                await _appDbContext.SaveChangesAsync();
            }
            return social;
        }
    }
}
