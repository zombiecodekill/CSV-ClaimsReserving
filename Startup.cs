using ClaimsReserving.Services;
using ClaimsReserving.Services.Validation;
using ClaimsReserving.Services.Writer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ClaimsReserving
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddTransient<ICsvLoader, CsvLoader>();
            services.AddTransient<ICsvOutput, CsvOutput>();
            services.AddTransient<ISummaryRecord, SummaryRecord>();
            services.AddTransient<IFileValidator, FileValidator>();
            services.AddTransient<IRecordsPerProduct, RecordsPerProduct>();
            services.AddSingleton<IDirectoryFinder, DirectoryFinder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Claims}/{action=Index}/{id?}");
            });
        }
    }
}
