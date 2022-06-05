using Application;
using Application.Commands;
using Application.Email;
using Application.Queries;
using AutoMapper;
using DataAccess;
using Implementation.Commands;
using Implementation.Email;
using Implementation.Logging;
using Implementation.Profiles;
using Implementation.Querys;
using Implementation.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreAPI.Core
{
    public static class APIExtensions
    {
        public static void AddAllQueries(this IServiceCollection services)
        {
            services.AddTransient<IGetAuthorQuery, EfGetAuthorQuery>();
            services.AddTransient<IGetAuthorsQuery, EfGetAuthorsQuery>();

            services.AddTransient<IGetBookQuery, EfGetBookQuery>();
            services.AddTransient<IGetBooksQuery, EfGetBooksQuery>();

            services.AddTransient<IGetGenreQuery, EfGetGenreQuery>();
            services.AddTransient<IGetGenresQuery, EfGetGenresQuery>();

            services.AddTransient<IGetImageQuery, EfGetImageQuery>();
            services.AddTransient<IGetImagesQuery, EfGetImagesQuery>();

            services.AddTransient<IGetPublisherQuery, EfGetPublisherQuery>();
            services.AddTransient<IGetPublishersQuery, EfGetPublishersQuery>();

        }

        public static void AddSwaggerToAPI(this IServiceCollection services)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("V1", new OpenApiInfo { Title = "BookstoreAPI", Version = "V1" });
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
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
        }

        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddTransient<BookstoreContext>();
            services.AddTransient<ISendEmail, SMTP>();
            services.AddTransient<UseCaseExecutor>();
            services.AddTransient<IUseCaseLogger, DBUseCaseLog>();
            services.AddAutoMapper(typeof(BookProfile).Assembly);
            services.AddTransient<IFakeData, FakeData>();
            services.AddControllers();
        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<RegistrationValidator>();
            services.AddTransient<AddAuthorValidator>();
            services.AddTransient<AddBookValidator>();
            services.AddTransient<AddGenreValidator>();
            services.AddTransient<AddImageValidator>();
            services.AddTransient<AddPublisherValidator>();

            services.AddTransient<UpdateAuthorValidator>();
            services.AddTransient<UpdateBookValidator>();
            services.AddTransient<UpdateGenreValidator>();
            services.AddTransient<UpdateImageValidator>();
            services.AddTransient<UpdatePublisherValidator>();

        }

        public static void AddAllCommands(this IServiceCollection services)
        {
            services.AddTransient<ICreateAuthorCommand, EfCreateAuthorCommand>();
            services.AddTransient<ICreateBookCommand, EfCreateBookCommand>();
            services.AddTransient<ICreateGanreCommand, EfCreateGenreCommand>();
            services.AddTransient<ICreateImageCommand, EfCreateImageCommand>();
            services.AddTransient<ICreatePublisherCommand, EfCreatePublisherCommand>();

            services.AddTransient<IDeleteAuthorCommand, EfDeleteAuthorCommand>();
            services.AddTransient<IDeleteBookCommand, EfDeleteBookCommand>();
            services.AddTransient<IDeleteGenreCommand, EfDeleteGenreCommand>();
            services.AddTransient<IDeleteImageCommand, EfDeleteImageCommand>();
            services.AddTransient<IDeletePubliserCommand, EfDeletePublisherCommand>();
            
            services.AddTransient<IUpdateAuthorCommand, EfUpdateAuthorCommand>();
            services.AddTransient<IUpdateBookCommand, EfUpdateBookCommand>();
            services.AddTransient<IUpdateGenreCommand, EfUpdateGenreCommand>();
            services.AddTransient<IUpdateImageCommand, EfUpdateImageCommand>();
            services.AddTransient<IUPdatePublisherCommand, EfUpdatePublisherCommand>();

            services.AddTransient<IRegistrationCommand, EfRegistrationCommand>();
        }

        public static void AddApplicationActor(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<IApplicatioActor>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();

                var user = accessor.HttpContext.User;

                if (user.FindFirst("ActorData") == null)
                {
                    return new AnonActor();
                }

                var actorString = user.FindFirst("ActorData").Value;

                var actor = JsonConvert.DeserializeObject<JWTActor>(actorString);

                return actor;

            });
        }

        public static void AddJWT(this IServiceCollection services, APISettings apiSettings)
        {
            services.AddTransient<JWTManager>(x =>
            {
                var context = x.GetService<BookstoreContext>();

                return new JWTManager(context, apiSettings.JWTIssuer, apiSettings.JWTSecretKey);
            });

            _ = services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "bookstore",
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMyVerySecretKey")),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

    }
}
