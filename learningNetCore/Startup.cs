﻿using learningNetCore.Data;
using learningNetCore.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace learningNetCore
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                //Auth Scheme is how do we identify - this uses cookie scheme - token becomes a cookie
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //Default challenege scheme: forces them to authenticate
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

            }).AddOpenIdConnect(options =>
            {
                //Framework is intelgent to bind these 
                _configuration.Bind("AzureID", options);
                //options.Authority; (AppSettings.Json has this information)
                //options.ClientId;
            }).AddCookie();

            services.AddSingleton<IGreeter, Greeter>();
            services.AddDbContext<learningNetCoreDbContext>(
                options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IRestuarantData, SqlRestaurantData>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              IGreeter greeter, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();         
            }

            app.UseRewriter(new RewriteOptions().AddRedirectToHttpsPermanent());

            //app.UseFileServer();
            //app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseNodeModules(env.ContentRootPath);

            app.UseAuthentication();
            //Will find the users ID for any middleware afterthis is called

            app.UseMvc(ConfigureRoutes);

            /*
            app.Run(async (context) =>
            {
                //var mGreeting = greeter.GetMessageOfTheDay();
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync($"Not Found Here");
            
                //await context.Response.WriteAsync($"{mGreeting} : {env.EnvironmentName}");
            });
            */
        }

        //Conventional based routing
        private static void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            // /Home/Index = HomeController/Index

            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}


//Place in Configure
//Custom piece of middleware
//app.Use(next =>
//{
//    return async context =>
//    {
//        logger.LogInformation("Request Incoming");
//        if (context.Request.Path.StartsWithSegments("/mym"))
//        {
//            await context.Response.WriteAsync("Hit!!");
//            logger.LogInformation("Request Handled");
//        }
//        else
//        {
//            await next(context);
//            logger.LogInformation("Response Outgoing");
//        }
//    };
//});

//app.UseWelcomePage(new WelcomePageOptions
//{
//    Path = "/wp"
//});
