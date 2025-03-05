using AutoMapper;
using LaChozaComercial.Data;
using LaChozaComercial.Models;
using LaChozaComercial.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LaChozaComercial
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<NetBuyDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IPublicacionRepository, PublicacionRepository>();


            builder.Services.AddIdentity<Usuario, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            })
                .AddEntityFrameworkStores<NetBuyDbContext>()
                .AddDefaultTokenProviders();


            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Usuario/Login";
            });

            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

 
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
