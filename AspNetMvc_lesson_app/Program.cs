using AspNetMvc_lesson_app.Models;
using AspNetMvc_lesson_app.Models.Services;

namespace AspNetMvc_lesson_app
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<UserInfoService>();
            builder.Services.AddScoped<SkillService>();
            builder.Services.AddScoped<UserSkill>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //app.MapGet("/Home/Index", async ctx =>
            //{ });            
            
            //app.MapGet("/", async ctx =>
            //{ });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
