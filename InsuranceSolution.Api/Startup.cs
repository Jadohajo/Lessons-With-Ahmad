using InsuranceSolution.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceSolution.Api
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
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDb;Database=InsuranceSolutionDb;Integrated Security=true", b => 
                {
                    b.MigrationsAssembly("InsuranceSolution.Api");
                }); 
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "InsuranceSolution.Api", Version = "v1" });
            });

            // Define CORS policy for everything 
            services.AddCors(options =>
            {
                options.AddPolicy("AllWebsites", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin(); 
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InsuranceSolution.Api v1"));
            }
            app.UseCors("AllWebsites");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); // Login 
            app.UseAuthorization();  // Roles based access 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
