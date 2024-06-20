using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Interfaces;
using COMBUSTIBLEAESCORE.Services;
using COMBUSTIBLEAESCORE.Data;
using Microsoft.AspNetCore.Http;

namespace COMBUSTIBLEAESCORE
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Dapper.SqlMapper.Settings.CommandTimeout = 120;
            //services.AddSingleton<conexion>();
            services.AddScoped<ILogin,LoginService>();
            services.AddScoped<IadCerrarVales, adCerrarValesService>();
            services.AddScoped<IadRegistarCompany, adRegistarCompanyService>();
            services.AddScoped<IadVehiculosFlotas, adVehiculosFlotasService>();
            services.AddScoped<IadUsuariosVehiculos, adUsuariosVehiculosService>();
            services.AddScoped<IadProyecto, adProyectoService>();
            services.AddScoped<IrpVales,rpValesService>();
            services.AddScoped<IadCentroCosto, adCentroCostoService>();
            services.AddScoped<IadRazonesCancelacion,adRazonesCancelacionService>();
            services.AddScoped<IadAgregarUsuario,adAgregarUsuarioService>();
            services.AddScoped<IadConfiguracionCompany,adConfiguracionCompanyService>();
            services.AddScoped<IadAnulacionEliminacionVales,adAnulacionEliminacionValesService>();
            services.AddScoped<IadGasolinera,adGasolineraService>();
            services.AddScoped<IadAutorizarVales, adAutorizarValesService>();
            services.AddScoped<IadGenerarVales,adGenerarValesService>();   
            services.AddScoped<IadAsignacionCentrosCosto,adAsignacionCentrosCostoService>();

            services.AddSingleton(new conexion(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddControllersWithViews();

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromHours(8);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.Name = ".COMBUSTIBLE.Session";
            });
            /*services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromSeconds(15);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.Name = ".COMBUSTIBLE.Session";
            });*/

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSession();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
