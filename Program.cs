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

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<LaChozaComercialDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IPublicacionRepository, PublicacionRepository>();


            builder.Services.AddIdentity<Usuario, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false; // No requerir dígitos
                options.Password.RequireLowercase = true; // No requerir minúsculas
                options.Password.RequireNonAlphanumeric = false; // No requerir caracteres no alfanuméricos
                options.Password.RequireUppercase = true; // No requerir mayúsculas
                options.Password.RequiredLength = 6; // Longitud mínima
                options.Password.RequiredUniqueChars = 0; // Cantidad de caracteres únicos requeridos
            })
                .AddEntityFrameworkStores<LaChozaComercialDbContext>()
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
                pattern: "{controller=Usuario}/{action=Registro}/{id?}");

            app.Run();
        }
    }
}
