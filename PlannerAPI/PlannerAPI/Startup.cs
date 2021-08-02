using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlannerAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PlannerAPI.Services;
using System.Linq;
using System.Security.Claims;
using PlannerAPI.Models.Auth;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PlannerAPI.Persistence.Repos;

namespace PlannerAPI
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
            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: "AllowOrigins",
            //        builder =>
            //        {
            //            builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowCredentials().AllowAnyMethod();
            //        });
            //});

            services.AddScoped<UserService, UserService>();

            services.AddCors();

            services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = (context =>
                    {
                        UserService service = context.HttpContext.RequestServices.GetRequiredService<UserService>();
                        var claims = context.Principal.Claims.ToList();
                        int id = int.Parse(context.Principal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier.ToString()).Value);
                        Organizer test = service.GetOrganizerById(id);

                        if (test == null)
                            context.Fail("Unauthorized User");
                        return Task.CompletedTask;
                    })
                };

                options.RequireHttpsMetadata = false; // For Development Only // TURN OFF IN PRODUCTION

                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AppSettings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddDbContext<PlannerDbContext>((options) => 
                options.UseSqlServer(Configuration.GetConnectionString("PlannerDb")));
            services.AddScoped<IEventDao, EFEventRepo>();
            services.AddScoped<IActivityDao, EFActivityRepo>();
            services.AddScoped<IAttendeeDao, EFAttendeeRepo>();
            services.AddScoped<IOrganizerDao, EFOrganizerRepo>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPlannerService, PlannerService>();
            services.AddControllers().AddNewtonsoftJson((options) 
                => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
