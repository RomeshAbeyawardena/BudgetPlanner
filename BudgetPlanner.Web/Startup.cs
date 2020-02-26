using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetPlanner.Broker;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DNI.Shared.Services.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using BudgetPlanner.Domains.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using BudgetPlanner.Domains.Dto;
using BudgetPlanner.Domains.Data;
using BudgetPlanner.Domains;
using System.IO;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using BudgetPlanner.Web.Attributes;

namespace BudgetPlanner.Web
{
    public class Startup
    {
        private AuthorizationPolicyBuilder Policies => new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser();

        private IWebHostEnvironment _webHostEnvironment;

        public ApplicationSettings ApplicationSettings { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services
                .RegisterServiceBroker<ServiceBroker>(options =>
                {
                    options.RegisterAutoMappingProviders = true;
                    options.RegisterCacheProviders = true;
                    options.RegisterMessagePackSerialisers = true;
                    options.RegisterMediatorServices = true;
                    options.RegisterExceptionHandlers = true;
                }, out var serviceBroker);

            ServiceBroker.ConfigureIdentity(services
                .AddIdentity<Domains.Dto.Account, Role>());

            services
                .ConfigureApplicationCookie(ConfigureOptions)
                .ConfigureExternalCookie(ConfigureOptions);

            services
                .AddDistributedMemoryCache()
                .AddSession(ConfigureSession)
                .AddAuthentication();
            services
                .AddAuthorization();

            //;

            services
                .AddMvc(options => options.Filters.Add(new AuthorizeFilter(Policies.Build())))
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddSessionStateTempDataProvider()
                .AddFluentValidation(configuration => configuration
                .RegisterValidatorsFromAssemblies(serviceBroker.Assemblies));

        }

        private void ConfigureSession(SessionOptions sessionOptions)
        {
            ConfigureCookieOptions(sessionOptions.Cookie);
            sessionOptions.IdleTimeout = TimeSpan.FromMinutes(ApplicationSettings.SessionExpiryPeriodInMinutes);
            sessionOptions.IOTimeout = TimeSpan.FromMinutes(ApplicationSettings.SessionIOExpiryPeriodInMinutes);
        }

        private void ConfigureOptions(CookieAuthenticationOptions options)
        {
            options.LoginPath = new PathString("/Login");
            options.LogoutPath = new PathString("/Logout");
            options.Cookie.Name = DataConstants.AccountSessionCookie;
            options.SlidingExpiration = true;
            ConfigureCookieOptions(options.Cookie);
        }

        public void ConfigureCookieOptions(CookieBuilder cookieBuilder)
        {
            cookieBuilder.HttpOnly = true;
            cookieBuilder.IsEssential = true;
            cookieBuilder.MaxAge = TimeSpan.FromMinutes(ApplicationSettings.CookieExpiryPeriodInMinutes);
            cookieBuilder.SameSite = SameSiteMode.Strict;
            cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationSettings applicationSettings)
        {
            _webHostEnvironment = env;
            ApplicationSettings = applicationSettings;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<SantizeInputMiddleware>();
            app.UseSession();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseStatusCodePagesWithRedirects("/Default/Error/{0}");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/{fileName}",GetScripts);
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();

            });
        }

        private async Task GetScripts(HttpContext context)
        {
            if(!context.Request.RouteValues.TryGetValue("fileName", out var fileName))
                return;

            var strFileName = fileName.ToString();

            if(!strFileName.EndsWith(".js"))
                return;

            var requestFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "content", strFileName);
            context.Response.ContentType = "application/javascript";
            if (!File.Exists(requestFilePath))
                return;

            await context.Response.SendFileAsync(requestFilePath);
        }
    }
}
