﻿using KH.Pepper.Core.AppServices.JwtTokenManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace KH.Pepper.Web.Config
{
    public static class JwtTokenExtension
    {
        public static IServiceCollection AddJwtTokenBindings(this IServiceCollection services, IConfiguration configuration)
        {

            var jwtKey = configuration.GetValue<string>("JwtSettings:Key");
            var keyBytes = Encoding.ASCII.GetBytes(jwtKey);

            TokenValidationParameters tokenValidation = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                ClockSkew = TimeSpan.Zero
            };

            services.AddSingleton(tokenValidation);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwtOptions =>
            {
                jwtOptions.TokenValidationParameters = tokenValidation;
                jwtOptions.Events = new JwtBearerEvents();
                jwtOptions.Events.OnTokenValidated = async (context) =>
                {
                    var ipAddress = context.Request.HttpContext.Connection.RemoteIpAddress.ToString();
                    var jwtService = context.Request.HttpContext.RequestServices.GetService<IJwtService>();
                    var jwtToken = context.SecurityToken as JwtSecurityToken;

                    if (!await jwtService.IsTokenValid(jwtToken.RawData, ipAddress))
                        context.Fail("Invalid Token Details");

                };
            });

            return services;
        }
    }
}
