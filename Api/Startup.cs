using Core.Interfaz;
using Core.Logic;
using Infraestructura.Data;
using Infraestructura.Interfaz;
using Infraestructura.Mapper;
using Infraestructura.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using System.Text;
using Utils.Converters;
using Utils.Filters;
using Utils.Middleware;
using Utils.Security;

namespace Api
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    // Agrega el converter
                    options.SerializerSettings.Converters.Add(new DateTimeUnspecifiedConverter());
                });
            services.AddHttpContextAccessor();
            services.Configure<ForwardedHeadersOptions>(options => { options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto; });
            services.AddMemoryCache();
            //AUTOMAPPER
            services.AddAutoMapper(cfg => { }, typeof(MappingProfile));
            //JWT
            services.AddAuthorization();
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer((Action<JwtBearerOptions>)(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = this.Configuration["Authentication:Issuer"],
                        ValidAudience = this.Configuration["Authentication:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes((string)((this.Configuration?["Authentication:SecretKey"]) ?? "0")))
                    };
                }));
            //SWAGER
            services.AddOpenApiDocument(document =>
            {
                document.Title = "Api Reportes REST";
                document.Description = "Reportes del sistema de la casa del tio rozch.";
                document.AddSecurity("JWT", [],
                    new OpenApiSecurityScheme
                    {
                        Type = OpenApiSecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        In = OpenApiSecurityApiKeyLocation.Header,
                        Description = "Copia y pega el Token en el campo 'Value:' así: Bearer {Token JWT}."
                    }
                );
                document.OperationProcessors.Add(new AuthorizeOperationProcessor("JWT"));
            });
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(culture: "es-MX", uiCulture: "es-MX");

            });
            //MIDDLEWARES
            services.AddTransient<PeticionMiddleware>();
            //INYECCION DE DEPENDENCIAS
            services.AddDbContext<ReportesContext>(o => o.UseNpgsql(Configuration.GetConnectionString("BdReportes")));
            services.AddTransient<ILoggerRepository, LoggerRepository>();
            services.AddTransient<ICacheRepository, CacheRepository>();
            services.AddTransient<IReporteRepository, ReportesRepository>();

            services.AddTransient<INotificacionesLogic, NotificacionesLogic>();
            services.AddTransient<IReportesLogic, ReportesLogic>();

            Encrypt.Configure(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins(origins: Configuration["CorsOrigins"]?.Split(',') ?? [])
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
            services.AddAntiforgery(options => { options.SuppressXFrameOptionsHeader = true; });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHttpContextAccessor accesor)
        {
            JWT.Inicializar(accesor);
            app.UseHsts();
            app.UseCors("CorsPolicy");
            app.UseXXssProtection(options=> options.EnabledWithBlockMode());
            app.UseRedirectValidation();
            app.UseXContentTypeOptions();
            app.UseXfo(xfo => xfo.SameOrigin());
            app.UseNoCacheHttpHeaders();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseForwardedHeaders();
            app.UseOpenApi();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Reportes REST");
                c.RoutePrefix = string.Empty;
            });
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<PeticionMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.Use(async (context, next) =>
            //{
            //    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            //    context.Response.Headers.Add("Referrer-Policy", "no-referrer");
            //    context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");
            //    context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'");
            //    await next();
            //});
        }
    }
}
