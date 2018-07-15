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
using NM.Sector.Services.Identity.Infrastructure.Policy;
using NM.Sector.Services.Identity.Infrastructure.Token;
using NM.SharedKernel.Common.Claims;
using NM.SharedKernel.Implementation;

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
            services.AddDefaultMicroserviceImplementation();

            services.AddSingleton(_configuration);
            services.AddSingleton<IJsonWebTokenFactory, JsonWebTokenFactory>();

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
                        policy.RequireClaim(CustomClaimTypes.Id);
                        policy.RequireClaim(CustomClaimTypes.Email);
                        policy.RequireClaim(CustomClaimTypes.ApiAccess, apiAccessKey);
                    });

                    options.AddPolicy(IdentityPolicy.Admin, policy =>
                    {
                        policy.RequireClaim(CustomClaimTypes.Id);
                        policy.RequireClaim(CustomClaimTypes.Email);
                        policy.RequireClaim(CustomClaimTypes.ApiAccess, apiAccessKey);
                        policy.RequireClaim(CustomClaimTypes.AdminAccess);
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
                .UseMvc();
        }

        #endregion
    }
}
