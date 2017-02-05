using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NeoMode.Core;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NeoMode.Service.Services;
using NeoMode.Services.Services;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NeoMode.Core.Options;
using NeoMode.Model;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using NeoMode.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace NeoMode
{
    public class Startup
    {
        private const string SecretKey = "neomode@!#@231546$@!#842016";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddTransient<ApplicationDbContext>(sp => new ApplicationDbContext());

            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<ISchoolService, SchoolService>();
            services.AddTransient<IExamService, ExamService>();
            services.AddTransient<IExamConfigService, ExamConfigService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEncryptionService, EncryptionService>();

            services.AddSession();
            //services.AddSession(options => {
            //    options.IdleTimeout = TimeSpan.FromMinutes(30);
            //    options.CookieName = ".MyApplication";
            //});

            // Add framework services.
            services.AddMvc().AddApplicationPart(typeof(NeoMode.API.Controllers.StudentController).GetTypeInfo().Assembly).AddControllersAsServices();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //SET IHttpContextAccessor
            var serviceProvider = services.BuildServiceProvider();            
            var accessor = serviceProvider.GetService<IHttpContextAccessor>();

            AuthenticationHelp.SetHttpContextAccessor(accessor);

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //----------------------------
            var tokenProviderOptions = new TokenProviderOptions
            {
                Path = Configuration.GetSection("TokenAuthentication:TokenPath").Value,
                Audience = Configuration.GetSection("TokenAuthentication:Audience").Value,
                Issuer = Configuration.GetSection("TokenAuthentication:Issuer").Value,
                SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256),
                IdentityResolver = GetIdentity
            };

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,
                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = Configuration.GetSection("TokenAuthentication:Issuer").Value,
                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = Configuration.GetSection("TokenAuthentication:Audience").Value,
                // Validate the token expiry
                ValidateLifetime = true,
                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                AuthenticationScheme = "Cookie",
                CookieName = Configuration.GetSection("TokenAuthentication:CookieName").Value,
                TicketDataFormat = new CustomJwtDataFormat(
                    SecurityAlgorithms.HmacSha256,
                    tokenValidationParameters)

            });

            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(tokenProviderOptions));
            app.UseMiddleware<UserProviderMiddleware>();
            //---------------------------

            app.UseStaticFiles();

            app.UseSession();
            app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");
                });
        }
        private Task<ClaimsIdentity> GetIdentity(string username, string password, int type, [FromServices] ISchoolService schoolService)
        {
            var result = schoolService.GetByPrimaryKey(username, password);
            if (result != null)
            {
                return Task.FromResult(new ClaimsIdentity(new GenericIdentity(username, "Token"), new Claim[] { }));
            }
            // Account doesn't exists
            return Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
