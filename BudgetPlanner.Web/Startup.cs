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
using DNI.Core.Services.Extensions;
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
using Microsoft.AspNetCore.HttpOverrides;

namespace BudgetPlanner.Web
{
    public class Startup
    {
        private AuthorizationPolicyBuilder Policies => new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser();

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
                    options.RegisterCryptographicProviders = true;
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
            ApplicationSettings = applicationSettings;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSession();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseStatusCodePagesWithRedirects("/Default/Error/{0}");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();

            });
        }

    }
}
