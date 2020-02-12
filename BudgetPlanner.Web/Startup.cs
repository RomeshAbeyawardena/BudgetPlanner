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

namespace BudgetPlanner.Web
{
    public class Startup
    {
        private AuthorizationPolicyBuilder Policies => new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser();
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
                }, out var serviceBroker).ConfigureApplicationCookie(ConfigureOptions);
            
            ServiceBroker.ConfigureIdentity(services
                .AddIdentity<Domains.Dto.Account, Role>());

            services
                .AddDistributedMemoryCache()
                .AddSession()
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

        private void ConfigureOptions(CookieAuthenticationOptions options)
        {
            options.LoginPath = new PathString("/Login");
            options.LogoutPath = new PathString("/Logout");
            options.SlidingExpiration = true;
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
            options.Cookie.MaxAge = TimeSpan.FromMinutes(25);
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        }

        private void ConfigureCookies(IdentityCookiesBuilder options)
        {
            options.ExternalCookie.Configure(ConfigureOptions);
            options.ApplicationCookie.Configure(ConfigureOptions);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSession();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseStatusCodePagesWithRedirects("/Default/Error/{0}");
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
