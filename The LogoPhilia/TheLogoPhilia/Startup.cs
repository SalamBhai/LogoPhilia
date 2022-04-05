using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TheLogoPhilia.ApplicationDbContext;
using TheLogoPhilia.Authentication;
using TheLogoPhilia.Implementations;
using TheLogoPhilia.Implementations.Repositories;
using TheLogoPhilia.Implementations.Services;
using TheLogoPhilia.Interfaces;
using TheLogoPhilia.Interfaces.IRepositories;
using TheLogoPhilia.Interfaces.IServices;

namespace TheLogoPhilia
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
            services.AddCors(cor => cor.AddPolicy("THELOGOPHILIA", builder=>
            {
               builder.AllowAnyHeader();
               builder.AllowAnyMethod();
               builder.AllowAnyOrigin();
            }));
            services.AddHttpContextAccessor();
                services.AddScoped<IRoleService, RoleService>();
                services.AddScoped<IOxfordService, OxfordService>();
                services.AddScoped<IRoleRepository, RoleRepository>();
                services.AddScoped<IUserRoleRepository, UserRoleRepository>();
                services.AddScoped<IApplicationUserService, ApplicationUserService>();
                services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IUserService, UserService>();
                services.AddScoped<IMessageRepository, MessageRepository>();
                services.AddScoped<IMessageService, MessageService>();
                services.AddScoped<ILanguageOfOriginService, LanguageOfOriginService>();
                services.AddScoped<ILanguageOfOriginRepository, LanguageOfOriginRepository>();
                services.AddScoped<IWordRepository, WordRepository>();
                services.AddScoped<IWordService, WordService>();
                services.AddScoped<INotesRepository, NotesRepository>();
                services.AddScoped<INotesService, NotesService>();
                services.AddScoped<IMessageSender, MessageSender>();
                services.AddScoped<IApplicationAdministratorRepository, ApplicationAdministratorRepository>();
                services.AddScoped<IApplicationAdministratorService, ApplicationAdministratorService>();
               services.AddScoped<IAdministratorMessageService,  AdministratorMessageService>();
               services.AddScoped<IAdministratorMessageRepository, AdministratorMessageRepository>();
               services.AddScoped<IPostLogRepository, PostLogRepository>();
                services.AddScoped<IApplicationUserAdminMessageRepository,ApplicationUserAdminMessageRepository>();
                services.AddScoped<IApplicationUserPostRepository, ApplicationUserPostRepository>();
                services.AddScoped<IApplicationUserPostService, ApplicationUserPostService>();
                services.AddScoped<IApplicationUserCommentRepository, ApplicationUserCommentRepository>();
                services.AddScoped<IApplicationUserCommentService, ApplicationUserCommentService>();

            services.AddControllers();
            services.AddDbContext<AppDbContext>(options=>
            options.UseMySQL(Configuration.GetConnectionString("AppDbContext")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TheLogoPhilia", Version = "v1" });
            });
            
            var key = "The LogoPhilia Security Key Accessible To No One";
            services.AddSingleton<IJWTTokenHandler>( new JWTTokenHandler(key));
            services.AddAuthentication(service=> 
            {
                     service.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                     service.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(service=> 
            {
              service.RequireHttpsMetadata = false;
              service.SaveToken = true;
              service.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                  ValidateIssuer = false,
                  ValidateAudience = false,
              };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TheLogoPhilia v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("THELOGOPHILIA");
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
