using Application;
using Application.Commands;
using Application.Email;
using Application.Queries;
using AutoMapper;
using BookstoreAPI.Core;
using DataAccess;
using Implementation.Commands;
using Implementation.Email;
using Implementation.Logging;
using Implementation.Querys;
using Implementation.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreAPI
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
            var apiSettings = new APISettings();

            Configuration.Bind(apiSettings);

            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddJWT(apiSettings); 
            services.AddTransient<BookstoreContext>();
            services.AddTransient<AddBookValidator>();
            services.AddTransient<UpdateBookValidator>();
            services.AddTransient<IGetBookQuery, GetBookQuery>();
            services.AddHttpContextAccessor();
            
            services.AddApplicationActor();

            services.AddTransient<IUseCaseLogger, DBUseCaseLog>();

            services.AddTransient<IRegistrationCommand, RegistrationCommand>();
            services.AddTransient<UseCaseExecutor>();
            services.AddTransient<RegistrationValidator>();
            services.AddAutoMapper(typeof(GetBookQuery).Assembly);
            services.AddTransient<ISendEmail, SMTP>(x => new SMTP(apiSettings.EmailFrom, apiSettings.EmailPassword));

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("V1", new OpenApiInfo { Title = "BookstoreAPI", Version = "V1" });
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 123456abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                          {
                            Reference = new OpenApiReference
                              {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                              },
                              Scheme = "oauth2",
                              Name = "Bearer",
                              In = ParameterLocation.Header,

                            },
                            new List<string>()
                          }
                    });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x =>
            {
                x.AllowAnyOrigin();
                x.AllowAnyMethod();
                x.AllowAnyHeader();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/V1/swagger.json", "Swagger");
            });

            app.UseRouting();
            app.UseStaticFiles();
            app.UseMiddleware<GlobalExceptionHandler>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
