using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Repositories.implementations;
using App.Repositories.interfaces;
using App.Services.implementations;
using App.Services.interfaces;
using config.implementations;
using config.interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace JWT_Login_System
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
            this.Register(services);
            // Get AppSettings Instance
            IAppSettings settings = services.BuildServiceProvider().GetService<IAppSettings>();

            services.AddCors(options => {
                options.AddPolicy("CORSPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);            
            // Add Swagger with Security Definition.
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info {
                    Version = "V1",
                    Title = "JWT Login"                    
                });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme {
                    Description = "JWT Authorization (Example: 'Bearer <token>')",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(security);
            });
            // Get the JWT Security Key and convert it to a byte array.
            byte[] key = Encoding.ASCII.GetBytes(settings.GetJwtSecurityKey());
            // Add Authentication
            services.AddAuthentication(a => 
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(j => 
            {
                j.RequireHttpsMetadata = false;
                j.SaveToken = true;
                j.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CORSPolicy");
            // Use Authentication
            app.UseAuthentication();            

            app.UseMvc();
            // Use Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "JWT Login");                
            });
        }

        public void Register(IServiceCollection services)
        {
            services.AddSingleton<IAppSettings, AppSettings>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
