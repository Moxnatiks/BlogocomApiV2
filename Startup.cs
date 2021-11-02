using BlogocomApiV2.GraphQL.Chats;
using BlogocomApiV2.GraphQL.Messages;
using BlogocomApiV2.GraphQL.UserChats;
using BlogocomApiV2.GraphQL.Users;
using BlogocomApiV2.Interfaces;
using BlogocomApiV2.Repository;
using BlogocomApiV2.Services;
using BlogocomApiV2.Settings;
using FFMpegCore;
using GraphQL.Server.Ui.Voyager;
using HotChocolate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Runtime.InteropServices;

namespace BlogocomApiV2
{
    public class Startup
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //DB
            services.AddPooledDbContextFactory<ApiDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("MyProjectApiConection")));
            services.AddDbContext<ApiDbContext>(opt => opt.UseNpgsql
            (Configuration.GetConnectionString("MyProjectApiConection")));

            //Relations (only after DB connection)
            //services.AddControllers()
            //   .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling =
            //    Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //Mysor
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();

            //Services
            services.AddScoped<UserService>();
            services.AddScoped<ChatService>();
            services.AddScoped<MessageService>();


            //Interfaces
            services.AddScoped<IUser, UserRepository>();
            services.AddScoped<IChat, ChatRepository>();
            services.AddScoped<IMessage, MessageRepository>();

            //Transient

            //GraphQL
            services
                .AddGraphQLServer()
                .AddQueryType(q => q.Name("Query"))
                    .AddTypeExtension<UserQueries>()
                    .AddTypeExtension<MessageQueries>()
                .AddMutationType(m => m.Name("Mutation"))
                    .AddTypeExtension<UserMutations>()
                    .AddTypeExtension<ChatMutations>()
                    .AddTypeExtension<MessageMutations>()
               .AddSubscriptionType(d => d.Name("Subscription"))
                    .AddTypeExtension<ChatSubscriptions>()
                    .AddTypeExtension<MessageSubscriptions>()
                .AddType<UserType>()
                .AddType<UserChatType>()
                .AddType<ChatType>()
                .AddType<GraphQL.Messages.MessageType>()

                //.AddFiltering()
                //.AddSorting()
                .AddInMemorySubscriptions()
                .AddAuthorization()
                .AddErrorFilter(er =>
                {
                    switch (er.Exception)
                    {
                        case ArgumentException argexc:
                            return ErrorBuilder.FromError(er)
                            .SetMessage(argexc.Message)
                            .SetCode("ArgumentException")
                            .RemoveException()
                            .ClearExtensions()
                            .ClearLocations()
                            .Build();
                            break;
                    }
                    return er;
                });


            //JWT Test (It is working) 
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = AuthSettings.AUDIENCE,
                    ValidIssuer = AuthSettings.ISSUER,
                    RequireSignedTokens = false,
                    IssuerSigningKey = AuthSettings.GetSymmetricSecurityKey(),
                };



                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
            })

            //FirebaseAuth
            .AddJwtBearer("Firebase", options =>
            {
                options.Authority = "https://securetoken.google.com/my-firebase-project";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "my-firebase-project",
                    ValidateAudience = true,
                    ValidAudience = "my-firebase-project",
                    ValidateLifetime = true
                };
            }); ;


            services
               .AddAuthorization(options =>
               {
                   options.DefaultPolicy = new AuthorizationPolicyBuilder()
                       .RequireAuthenticatedUser()
                       .AddAuthenticationSchemes("Local")
                       .Build();

                   options.AddPolicy("Firebase", new AuthorizationPolicyBuilder()
                       .RequireAuthenticatedUser()
                       .AddAuthenticationSchemes("Firebase")
                       .Build());
               });

            //File TEst
            services.AddSwaggerGen(
              options =>
              {
                  options.SwaggerDoc("v1", new OpenApiInfo()
                  {
                      Title = "Learn Smart Coding - Demo",
                      Version = "V1",
                      Description = "",
                      TermsOfService = new System.Uri("https://karthiktechblog.com/copyright"),
                      Contact = new OpenApiContact()
                      {
                          Name = "Learn Smart Coding",
                          Email = "karthiktechblog.com@gmail.com",
                          Url = new System.Uri("http://www.karthiktechblog.com")
                      },
                      License = new OpenApiLicense
                      {
                          Name = "Use under LICX",
                          Url = new System.Uri("https://karthiktechblog.com/copyright"),
                      }
                  });
              }
          );
            services.AddControllers();
            //
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //file test
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Karthiktechblog Restaurant API V1");
            });
            //
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseWebSockets();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //test file
                endpoints.MapControllers();
                //
                endpoints.MapGraphQL();
            });

            app.UseGraphQLVoyager(new VoyagerOptions(), "/graphql-voyager");
        }
    }
}
