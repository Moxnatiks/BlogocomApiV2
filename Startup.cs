using BlogocomApiV2.Exceptions;
using BlogocomApiV2.GraphQL.Chats;
using BlogocomApiV2.GraphQL.Messages;
using BlogocomApiV2.GraphQL.UserChats;
using BlogocomApiV2.GraphQL.Users;
using BlogocomApiV2.Interfaces;
using BlogocomApiV2.Repository;
using BlogocomApiV2.Services;
using BlogocomApiV2.Settings;
using GraphQL.Server.Transports.Subscriptions.Abstractions;
using GraphQL.Server.Ui.Voyager;
using HotChocolate;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace BlogocomApiV2
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseAuthorization();

           /* app.Use(async (context, next) =>
            {
                // Use this if there are multiple authentication schemes
                var authResult = await context.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
                if (authResult.Succeeded && authResult.Principal.Identity.IsAuthenticated)
                {
                    await next();
                }
                else if (authResult.Failure != null)
                {
                    // Rethrow, let the exception page handle it.
                    ExceptionDispatchInfo.Capture(authResult.Failure).Throw();
                }
                else
                {
                    await context.ChallengeAsync();
                }
            });*/

            // My exceptions
            //app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseWebSockets();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });

            app.UseGraphQLVoyager(new VoyagerOptions(), "/graphql-voyager");
        }
    }
}
