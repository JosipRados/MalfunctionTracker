﻿using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using System.Runtime.CompilerServices;

namespace MalfunctionTrackerUI
{
    public static class RegisterServices
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddMemoryCache();

            builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                {
                    policy.RequireClaim("jobTitle", "Admin");
                });
            });

            builder.Services.AddSingleton<IDBConnection, DBConnection>();
            builder.Services.AddSingleton<ICategoryData, MongoCategoryData>();
            builder.Services.AddSingleton<IStatusData, MongoStatusData>();
            builder.Services.AddSingleton<IMalfunctionData, MongoMalfunctionData>();
            builder.Services.AddSingleton<IUserData, MongoUserData>(); //Nemamo nikakve zapise u pojedinog korisnika zato singl.
            builder.Services.AddSingleton<IPriorityData, MongoPriorityData>();
        }
    }
}
