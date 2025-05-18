using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using LumenSys.WebAPI.Data;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Data.Repositories;
using LumenSys.WebAPI.Services.Interfaces;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LumenSys.WebAPI.Services.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LumenSys.WebAPI.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace LumenSys.WebAPI
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
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            // Configuração do Entity Framework com Npgsql
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            // Configuração do Swagger com autenticação Bearer
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LumenSys", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Enter 'Bearer' [space] your token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,       
                    Scheme = "bearer",                      
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    TokenSignatures tokenSignature = new TokenSignatures();
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenSignature.Issuer,
                        ValidAudience = tokenSignature.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSignature.Key)),
                    };
                });


            // Adiciona controllers e configura a serialização JSON
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true; // Opcional, para melhor legibilidade
            });

            // Configuração do CORS
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:3000", "http://localhost:5173")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Scoped Repositories and Interfaces repo
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IFuneralPlansService, FuneralPlansService>();
            services.AddScoped<IFuneralService, FuneralService>();
            services.AddScoped<IUserService, UserService>();
          
            //Scoped Repositories and Interfaces repo
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IFuneralPlansRepository, FuneralPlansRepository>();
            services.AddScoped<IFuneralRepository, FuneralRepository>();
            services.AddScoped<IUserRepository, UserRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "LumenSys Web API V1");
                    // Configurações adicionais do Swagger UI
                    c.DocExpansion(DocExpansion.None);
                    c.DisplayRequestDuration();
                    c.EnableDeepLinking();
                    c.EnableFilter();
                    c.ShowExtensions();
                    c.EnableValidator();
                    c.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put, SubmitMethod.Delete);
                    c.OAuthClientId("swagger-ui");
                    c.OAuthAppName("Swagger UI");
                });
            }
            else
            {
                app.UseExceptionHandler("/home/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("MyPolicy");
            app.UseAuthentication();  
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
