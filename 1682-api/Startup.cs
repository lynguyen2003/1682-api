using DataServices.Data;
using DataServices.Interfaces;
using DataServices.Mapping;
using DataServices.Repositories;
using DataServices.Services.EmailServices;
using DataServices.Services.JwtServices;
using DataServices.Services.PasswordServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models.DTO.Password;
using System.Text;

namespace _1682_api
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
            var jwtConfig = new JwtConfig();
            var secretKey = jwtConfig.Secret;

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("DataServices"));
            });

            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenValidationParameter = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false, // private
                ValidateAudience = false, // private too
                RequireExpirationTime = false,
                ValidateLifetime = true,
                ValidIssuer = Configuration.GetSection("JwtConfig:Issuer").Value,
                ValidAudience = Configuration.GetSection("JwtConfig:Audience").Value
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = tokenValidationParameter;
            });
            services.AddSingleton(tokenValidationParameter);

            services.Configure<DataProtectionTokenProviderOptions>(opts => opts.TokenLifespan = TimeSpan.FromHours(2));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "1682-api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            var emailSettings = Configuration.GetSection("EmailSettings").Get<EmailSettings>();
            services.AddSingleton(emailSettings);

            services.AddScoped<IUnitOfWorks, UnitOfWorks>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddAutoMapper(typeof(MappingProfile));
            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();
            services.AddEndpointsApiExplorer();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
