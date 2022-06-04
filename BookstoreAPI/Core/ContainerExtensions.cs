using Application;
using Application.Commands;
using Application.Queries;
using DataAccess;
using Implementation.Commands;
using Implementation.Querys;
using Implementation.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreAPI.Core
{
    public static class ContainerExtensions
    {
        public static void AddApplicationActor(this IServiceCollection services)
        {
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
