using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using School_Core.Commands;
using School_Core.Commands.Lectures;
using School_Core.Contexts;
using School_Core.Domain.Models.Lectures;
using School_Core.Queries;
using School_Core.Util;

namespace School_Core.API
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
            
            //Command Handlers
            services.AddTransient<ICommandHandler<CloseLectureCommand>, CloseLectureCommand.Handler>();
            services.AddTransient<ICommandHandler<ArchiveLectureCommand>, ArchiveLectureCommand.Handler>();
            
            //Querys
            services.AddTransient<IQuery<Lecture>, LectureQuery>();
            
            
            //Util
            services.AddTransient<Messages>();
            
            services.AddDbContext<SchoolCoreDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SchoolDbConnection")));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}