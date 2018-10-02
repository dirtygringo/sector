using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NM.Sector.Services.Identity.Contract.Events;
using NM.Sector.Services.Identity.Handlers.Command;
using NM.Sector.Services.Identity.Handlers.Event;
using NM.Sector.Services.Identity.Security.Policy;
using NM.Sector.Services.Identity.Security.Token;
using NM.Sector.Services.Identity.Commands;
using NM.Sector.Services.Security.Claims;
using NM.ServiceBus.RabbitMq;
using NM.SharedKernel.Core;
using NM.SharedKernel.Core.Abstraction.Workers;
using NM.Storage.MongoDb;

namespace NM.Sector.Services.Identity
{
    public class Startup
    {
        #region Fields

        private readonly IConfiguration _configuration;

        #endregion

        #region Constructor

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region Methods

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services
                .AddOptions()
                .AddLogging()
                .AddSingleton(_configuration)
                .AddSingleton<IJsonWebTokenFactory, JsonWebTokenFactory>();

            var tokenSettings = _configuration.GetSection(nameof(TokenSettings)).Get<TokenSettings>();

            var key = Encoding.ASCII.GetBytes(tokenSettings.Secret);
            var signgingKey = new SymmetricSecurityKey(key);
            var apiAccessKey = tokenSettings.ApiAccessKey;

            services
                .Configure<TokenSettings>(options =>
                {
                    options.ApiAccessKey = apiAccessKey;
                    options.TokenPath = tokenSettings.TokenPath;
                    options.Secret = tokenSettings.Secret;
                    options.Issuer = tokenSettings.Issuer;
                    options.Audience = tokenSettings.Audience;
                    options.SigningCredentials = new SigningCredentials(signgingKey, SecurityAlgorithms.HmacSha256);
                })
                .AddAuthorization(options =>
                {
                    options.AddPolicy(IdentityPolicy.ApiUser, policy =>
                    {
                        policy.RequireClaim(SectorClaimTypes.Id);
                        policy.RequireClaim(SectorClaimTypes.Email);
                        policy.RequireClaim(SectorClaimTypes.ApiAccess, apiAccessKey);
                    });

                    options.AddPolicy(IdentityPolicy.Admin, policy =>
                    {
                        policy.RequireClaim(SectorClaimTypes.Id);
                        policy.RequireClaim(SectorClaimTypes.Email);
                        policy.RequireClaim(SectorClaimTypes.ApiAccess, apiAccessKey);
                        policy.RequireClaim(SectorClaimTypes.AdminAccess);
                    });
                })
                .AddAuthentication(auth =>
                {
                    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearer =>
                {
                    bearer.ClaimsIssuer = tokenSettings.Issuer;
                    bearer.Audience = tokenSettings.Audience;
                    bearer.RequireHttpsMetadata = false;
                    bearer.SaveToken = true;
                    bearer.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = tokenSettings.Issuer,
                        ValidateAudience = true,
                        ValidAudience = tokenSettings.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signgingKey,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                    bearer.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            if (
                                //context.Request.Path.Value.StartsWith($"/{ApiHubEndpoints.NotifyHub}") &&
                                context.Request.Query.TryGetValue("token", out var token))
                            {
                                context.Token = token;
                            }

                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            var te = context.Exception;
                            return Task.CompletedTask;
                        }
                    };
                });

            services
                .AddCore(dependency =>
                {
                    dependency.AddRabbitMq(_configuration);
                    dependency.AddMongoDb(_configuration);
                });

            services.AddTransient<IMessageHandler<CreateUser>, CreateUserHandler>();
            services.AddTransient<IMessageHandler<UserCreated>, UserCreatedHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(opt =>
                    opt.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials())
                .UseAuthentication()
                .UseHttpsRedirection()
                .UseMvc()
                .UseRabbitMq(config =>
                {
                    config.SubscribeToCommand<CreateUser>();
                    config.SubscribeToEvent<UserCreated>();
                })
                .UseMongoDb();
        }

        #endregion
    }
}
