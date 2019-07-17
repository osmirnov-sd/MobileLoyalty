using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using POCOModels.Inner;
using POCOModels.Postgresql;
using Swashbuckle.AspNetCore.Swagger;

namespace Loyality
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var cultureInfo = new CultureInfo("ru-RU");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            Configuration = configuration;
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => true;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Unspecified;
                options.SerializerSettings.DateFormatString = "s";
            });
            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<AddAuthorizationHeaderParameterOperationFilter>();
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Loyality",
                    Description = "API сервиса 'Loyality'",
                    Contact = new Contact
                    {
                        Name = "LoyalityTeam",
                        Email = "",
                        Url = ""
                    },
                });
            });
           

            services.Configure<PstgrSettings>(options =>
            {
                options.ConnectionString = Configuration.GetSection("PstgrConnection:ConnectionUri").Value;
            });

            

            //Database
            services.AddDbContext<PstgreAuthContext>();
            services.AddDbContext<PstgreTablesContext>();
            //Auth
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<PstgreAuthContext>()
                .AddDefaultTokenProviders();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.IncludeErrorDetails = true;
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 1;
                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });
           

            //Jsonizator
            services
                .AddMvcCore(options =>
                {
                    options.RequireHttpsPermanent = true;
                    options.RespectBrowserAcceptHeader = true;
                })
                .AddFormatterMappings()
                .AddJsonFormatters();

            services.AddMvc();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, PstgreAuthContext dbContext, PstgreTablesContext dbTablesContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
            );
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseAuthentication();

            app.UseMvc();
            var configs = new string[] { Configuration.GetSection("PstgrConnection:Database").Value,
                                                                              Configuration.GetSection("PstgrConnection:ConnectionUri").Value,
                                                                              Configuration["Environment"],
            };
        }
    }
}
