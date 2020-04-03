using System;
using System.Text;
using GaweNotesApi.Database;
using GaweNotesApi.Models;
using GaweNotesApi.Options;
using GaweNotesApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace GaweNotesApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NoteDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("NotesDb")), ServiceLifetime.Transient);
            services.Configure<AuthOptions>(Configuration.GetSection("Auth"));
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<NoteDbContext>()
                .AddDefaultTokenProviders();
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(config =>
                {
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["Auth:Issuer"],
                        ValidAudience = Configuration["Auth:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Auth:Key"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });
            services.AddScoped<AccountService>();
            services.AddScoped<NoteService>();
            services.AddControllers();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
