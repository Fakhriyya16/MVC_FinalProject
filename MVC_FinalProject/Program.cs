using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC_FinalProject.Data;
using MVC_FinalProject.Helpers;
using MVC_FinalProject.Models;
using MVC_FinalProject.Services;
using MVC_FinalProject.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<AppUser, IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequiredLength = 8;
    opt.Password.RequiredUniqueChars = 2;
    opt.Password.RequireDigit = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequireNonAlphanumeric = true;
    opt.User.RequireUniqueEmail = true;

    opt.SignIn.RequireConfirmedEmail = true;
});

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Smtp"));

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IInformationService, InformationService>();
builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IInstructorService, InstructorService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISocialService, SocialService>();
builder.Services.AddScoped<ISettingService, SettingService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IContactService, ContactService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

