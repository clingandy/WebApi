using System;
using Infrastructure.Web.Extentions;
using Infrastructure.Web.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace Infrastructure.Web
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
            services.AddMemoryCache();  //添加本地缓存

            services.AddMvc(option => { option.Filters.Add(typeof(PermissionAttribute)); }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.Configure<IISOptions>(options =>
            //{
            //    options.ForwardClientCertificate = false;
            //});

            services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromSeconds(3600);
            });

            services.RegisDependency();     //Core自带注入，直接引用时使用

            //Autofac注入  返回类型改为:IServiceProvider
            //NuGet添加Autofac和Autofac.Extensions.DependencyInjection
            //var builder = new ContainerBuilder();
            //builder.Populate(services);
            //builder.RegisterAssemblyTypes(Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "CrmScrew.Core.dll")).AsImplementedInterfaces();
            //builder.RegisterAssemblyTypes(Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "Infrastructure.Core.dll")).AsImplementedInterfaces();
            //return new AutofacServiceProvider(builder.Build());

            //API文档
            //services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" }); });
            //services.ConfigureSwaggerGen(options =>
            //{
            //    //var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            //    //options.IncludeXmlComments(basePath + "\\Infrastructure.Web.xml");
            //    options.OperationFilter<AddAuthTokenHeaderParameter>();
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionHandler(env.IsDevelopment() ? "/Home/ErrorPage" : "/Home/ErrorWriteLog");

            //允许全部跨域
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            //app.UseSession();

            app.UseStaticFiles();

            //使用API文档
            //app.UseSwagger();
            //app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
