using Impexium.DataAccess;
using Impexium.Domain.Entities;
using Impexium.Domain.Repositories;
using Impexium.Domain.Services;
using Impexium.Repositories;
using Impexium.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace Impexium.Api
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

            string key = Configuration.GetValue<string>("Key");
            string issuer = Configuration.GetValue<string>("Issuer");
            string audience = Configuration.GetValue<string>("Audience");

            services.AddDbContext<ImpexiumContext>(opt =>
                                               opt.UseInMemoryDatabase("Impexium"));


            services.AddDefaultIdentity<IdentityUser>()
             .AddRoles<IdentityRole>()
             .AddEntityFrameworkStores<ImpexiumContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    SaveSigninToken = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
                option.SaveToken = true;
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder
                        .WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });
            services.AddTransient<IProductsRepository, ProductsRepository>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IAuthService, AuthService>();

            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddSwaggerGen();

            services.AddControllers()
                .AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<IdentityUser> userManager, IProductsService productsService)
        {
            await CreateDumpUserAsync(userManager);
            await CreateProductsAsync(productsService);

            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();


            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }

        private async Task CreateProductsAsync(IProductsService productsService)
        {


            await productsService.CreateProductAsync(new Product
            {
                Name = "Computer",
                Quantity = 10,
                Description = "Mac Computers"
            });
            await productsService.CreateProductAsync(new Product
            {
                Name = "Mouse",
                Description = "Wireless",
                Quantity = 20
            });
            await productsService.CreateProductAsync(new Product
            {
                Name = "KeyBoard",
                Description = "Wireless",
                Quantity = 40
            });
        }

        private async Task CreateDumpUserAsync(UserManager<IdentityUser> userManager)
        {
            var user = new IdentityUser
            {
                Email = "admin@admin.com",
                UserName = "admin@admin.com"
            };
            await userManager.CreateAsync(user);

            await userManager.AddPasswordAsync(user, "AdminAdmin123.");
        }


    }
}
