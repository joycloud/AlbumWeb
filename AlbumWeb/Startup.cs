using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //readonly string allowSpecificOrigins = "_allowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // �N Session �s�b ASP.NET Core �O���餤
            services.AddControllersWithViews();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });

            //services.AddControllersWithViews();
            ////�ŧi�W�[���Ҥ覡�A�ϥ� cookie ����
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
            //{
            //    //�s�����|����cookie �u��g��HTTP(S) ��w�Ӧs��
            //    option.Cookie.HttpOnly = true;
            //    //�n�J���A���n�J�ɷ|�۰ʾɨ�n�J��
            //    option.LoginPath = "/Home/Index";
            //    //�n�X����(�i�H�ٲ�)
            //    //option.LogoutPath = new PathString("/Index");
            //    //�n�J���Įɶ�
            //    option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //�ϥ��R�A�ɮ�
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();
            app.UseAuthentication();   // ����  ��ʥ[�J
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                    //pattern: "{controller=Home}/{action=CreateAlbum}");
            });



            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    //app.UseHsts();
            //}
            ////app.UseHttpsRedirection();
            //app.UseStaticFiles();
            //app.UseRouting();
            //app.UseAuthentication();   // ����  ��ʥ[�J
            //app.UseAuthorization();    // ���v

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
