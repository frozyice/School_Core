using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using School_Core.ViewModels.Student;
using School_Core.ViewModels.Lecture;
using School_Core.ViewModels.Teacher;
using School_Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using School_Core.Commands;
using School_Core.Contexts;
using School_Core.Querys;
using School_Core.Commands.Lecture;
using School_Core.Util;
using School_Core.ViewModels.Home;

namespace School_Core
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
            //Commands
            services.AddTransient<ICommandHandler<CloseLectureCommand>, CloseLectureCommand.Handler>();
            services.AddTransient<ICommandHandler<ArchiveLectureCommand>, ArchiveLectureCommand.Handler>();
            services.AddTransient<ICommandHandler<EnrollStudentCommand>, EnrollStudentCommand.Handler>();

            //Querys
            services.AddTransient<ILectureQuery, LectureQuery>();
            services.AddTransient<IStudentQuery, StudentQuery>();
            services.AddTransient<ITeacherQuery, TeachersQuery>();

            //ViewModelProviders
            services.AddTransient<StudentViewModel.IProvider, StudentViewModel.Provider>();
            services.AddTransient<StudentListViewModel.IProvider, StudentListViewModel.Provider>();

            services.AddTransient<TeacherViewModel.IProvider, TeacherViewModel.Provider>();
            services.AddTransient<TeacherListViewModel.IProvider, TeacherListViewModel.Provider>();
            services.AddTransient<TeacherDetailsViewModel.IProvider, TeacherDetailsViewModel.Provider>();

            services.AddTransient<LectureViewModel.IProvider, LectureViewModel.Provider>();
            services.AddTransient<LectureListViewModel.IProvider, LectureListViewModel.Provider>();
            services.AddTransient<LectureDetailsViewModel.IProvider, LectureDetailsViewModel.Provider>();
            services.AddTransient<StudentNotEnrolledViewModel.IProvider, StudentNotEnrolledViewModel.Provider>();
            services.AddTransient<EnrollStudentViewModel.IProvider, EnrollStudentViewModel.Provider>();
            services.AddTransient<EnrollStudentViewModel.IProvider, EnrollStudentViewModel.Provider>();

            services.AddTransient<CounterTableViewModel.IProvider, CounterTableViewModel.Provider>();

            services.AddTransient<HomeViewModel.IProvider, HomeViewModel.Provider>();

            //DbContext
            services.AddDbContext<SchoolCoreDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SchoolDbConnection")));
            //services.AddScoped(_ => new SchoolCoreDbContext(Configuration.GetConnectionString("SchoolDbConnection")));

            //Util
            services.AddTransient<Messages>();

            //MVC
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
