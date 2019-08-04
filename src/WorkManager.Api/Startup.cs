using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using WorkManager.Core.Settings;
using WorkManager.Data;
using WorkManager.Data.Models;
using WorkManager.Services;

namespace WorkManager.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "WorkManager API", Version = "v1" });
            });

            var connectionString = this.Configuration.GetConnectionString("WorkManagerDbConnection");

            services.AddDbContextPool<WorkManagerDbContext>(options =>
                    options.UseSqlServer(connectionString, builder =>
                    {
                        builder.EnableRetryOnFailure(3);
                        builder.MigrationsAssembly(typeof(WorkManagerDbContext).Assembly.FullName);
                    }));

            services.AddIdentity<User, UserRole>()
                .AddEntityFrameworkStores<WorkManagerDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            });

            services.Configure<JwtSettings>(this.Configuration.GetSection(nameof(JwtSettings)));

            services.AddScoped<IUserService, UserService>();

            var jwtSettings = this.Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();

            services
                .AddAuthentication(configureOptions =>
                {
                    configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(configureOptions =>
                {
                    configureOptions.RequireHttpsMetadata = false;
                    configureOptions.SaveToken = true;
                    configureOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                        ValidIssuer = jwtSettings.Authority,
                        ValidAudience = jwtSettings.Audience,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
                });

            services.PostConfigure<ApiBehaviorOptions>(options =>
            {
                var builtInFactory = options.InvalidModelStateResponseFactory;

                options.InvalidModelStateResponseFactory = context =>
                {
                    Log.Warning($"Request submitted with invalid model state.");

                    foreach (var modelStateEntry in context.ModelState)
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine($"{modelStateEntry.Key} - ");

                        var errors = modelStateEntry.Value.Errors;
                        foreach (var error in errors)
                        {
                            builder.AppendLine(error.ErrorMessage);
                        }

                        Log.Warning(builder.ToString());
                    }

                    return builtInFactory(context);
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();

            app.UseCors("AllowAll");

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WorkManager API V1");
                });
            }

            app.UseMvc();
        }
    }
}
