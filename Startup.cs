using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Zaipay.Configurations;
using Zaipay.Service;

namespace Zaipay
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

            services.AddControllers();
            services.AddDbContext<ZaipayDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("SqlConnection")), ServiceLifetime.Transient);

            services.AddTransient<IZaiPayPlatform, ZaiPayPlatformService>();
            services.AddTransient<ICanadaEftPaymentService, CanadaEftPaymentService>();
            services.AddTransient<IFinmoAuFlow, FinmoAuFlowService>();
            services.AddTransient<IFinmoNzFlowService, FinmoNzFlowService>();
            services.AddTransient<ICanadaInteracPaymentService, CanadaInteracPaymentService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ILoggedInUserService, LoggedInUserService>();
           
            services.AddTransient<IHangfire, HangfireService>();

            // Adding Authentication  
            // services.AddAuthentication(options =>
            // {
            //     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            // })

            // // Adding Jwt Bearer  
            // .AddJwtBearer(options =>
            // {
            //     options.SaveToken = true;
            //     options.RequireHttpsMetadata = false;
            //     options.TokenValidationParameters = new TokenValidationParameters()
            //     {
            //         ValidateIssuer = true,
            //         ValidateAudience = false,
            //         ValidAudience = Configuration["JWT:ValidAudience"],
            //         ValidIssuer = Configuration["JWT:ValidIssuer"],
            //         IssuerSigningKey =
            //         new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
            //     };

            //     options.Events = new JwtBearerEvents()
            //     {
            //         OnAuthenticationFailed = c =>
            //         {
            //             if (c.Exception.GetType() == typeof(SecurityTokenExpiredException))
            //             {
            //                 c.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
            //                 c.Response.StatusCode = 403;
            //                 c.Response.ContentType = "application/json";
            //                 var result = JsonConvert.SerializeObject("401 Not authenticated. Token expired");

            //                 return c.Response.WriteAsync(result);
            //             }
            //             // c.NoResult();
            //             c.Response.StatusCode = 500;
            //             c.Response.ContentType = "text/plain";
            //             return c.Response.WriteAsync(c.Exception.ToString());
            //         },
            //         OnChallenge = context =>
            //         {
            //             //context.HandleResponse();
            //             context.Response.StatusCode = 401;
            //             context.Response.ContentType = "application/json";
            //             var result = JsonConvert.SerializeObject("401 Not authenticated");
            //             return context.Response.WriteAsync(result);
            //         },
            //         OnForbidden = context =>
            //         {
            //             context.Response.StatusCode = 403;
            //             context.Response.ContentType = "application/json";
            //             var result = JsonConvert.SerializeObject("403 Not authorized");
            //             return context.Response.WriteAsync(result);
            //         }
            //     };
            // });
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Zaipay Service", Version = "v1" });
            });

            //services.AddHangfire(configuration => configuration
            // .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            // .UseSimpleAssemblyNameTypeSerializer()
            // .UseRecommendedSerializerSettings()
            // .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
            // {
            //     CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            //     SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            //     QueuePollInterval = TimeSpan.Zero,
            //     UseRecommendedIsolationLevel = true,
            //     DisableGlobalLocks = true
            // }));

            //Add the processing server as IHostedService
            //services.AddHangfireServer();

            //services.AddDbContext<HangfireDbContext>(options =>
            //{
            //    options.UseSqlServer(Configuration.GetConnectionString("HangfireConnection"));
            //});

            //services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("SqlConnection")));

            services.Configure<ZaiConfig>(Configuration.GetSection("ZaiConfig"));
            services.Configure<InternalConfig>(Configuration.GetSection("InternalConfig"));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
            }
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zaipay v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapHangfireDashboard();
            });


            //app.UseHangfireServer();

            //BackgroundJob.Schedule<IHangfire>(x => x.UpdateToken(),TimeSpan.FromMinutes(60));
            //app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            //{
            //    Authorization = new[] { new DashboardNoAuthorizationFilter() }
            //});

        }
    }
}
