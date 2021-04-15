using System;
using back.Data;
using back.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace back
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
            var dbLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

            services.AddDbContext<ApplicationContext>(
                options =>
                {
                    options.UseMySql(
                        Configuration.GetConnectionString("DefaultConnection"), 
                        new MySqlServerVersion(new Version(8, 0, 11))
                        ).UseLoggerFactory(dbLoggerFactory);
                }
            );
            
            services.AddControllers();
            
            
            services.AddOData(opt => 
                opt.AddModel("odata", GetEdmModel()).Filter().Select().OrderBy().Expand().Count());
            
            
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new() {Title = "back", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "back v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private IEdmModel GetEdmModel()
        {
            var edmModelBuilder = new ODataConventionModelBuilder();
            edmModelBuilder.EntitySet<User>("Users");
            edmModelBuilder.EntitySet<Role>("Roles");
            return edmModelBuilder.GetEdmModel();
        }
    }
}