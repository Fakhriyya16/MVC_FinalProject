using Microsoft.EntityFrameworkCore;
using MVC_FinalProject.Data;
using MVC_FinalProject.Models;
using MVC_FinalProject.Services.Interfaces;
using MVC_FinalProject.ViewModels.Instructors;

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

            instructor.InstructorSocials ??= new List<InstructorSocial>();
            instructor.InstructorSocials.Add(instructorSocial);
        }

        public async Task UpdateSocialLinks(Instructor instructor, InstructorEditVM request)
        {
            if (request == null || instructor == null)
            {
                return;
            }

            await UpdateOrAddSocialLink(instructor, "Instagram", request.Instagram);
            await UpdateOrAddSocialLink(instructor, "Facebook", request.Facebook);
            await UpdateOrAddSocialLink(instructor, "Twitter", request.Twitter);

            _appDbContext.Instructors.Update(instructor);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateOrAddSocialLink(Instructor instructor, string socialName, string socialUrl)
        {
            if (string.IsNullOrWhiteSpace(socialUrl))
            {
                var existingSocialLink = instructor.InstructorSocials
                    .FirstOrDefault(isocial => isocial.Social.Name == socialName);

                if (existingSocialLink != null)
                {
                    _appDbContext.InstructorSocials.Remove(existingSocialLink);
                }
            }
            else
            {
                var existingSocialLink = instructor.InstructorSocials
                    .FirstOrDefault(isocial => isocial.Social.Name == socialName);

                if (existingSocialLink != null)
                {
                    existingSocialLink.SocialURL = socialUrl;
                }
                else
                {
                    var social = await GetOrCreateSocialByName(socialName);
                    var newSocialLink = new InstructorSocial
                    {
                        Social = social,
                        SocialURL = socialUrl,
                        Instructor = instructor
                    };
                    instructor.InstructorSocials.Add(newSocialLink);
                }
            }
        }

        public async Task<Social> GetOrCreateSocialByName(string name)
        {
            var social = await _appDbContext.Socials.FirstOrDefaultAsync(s => s.Name == name);
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
