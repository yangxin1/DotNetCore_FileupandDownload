using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace FileUpanDown
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //添加swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Sparkle",
                    Version = "这是显示在名称上面的版本号 ",
                    Description = "这是描述语句",
                    Contact = new Contact { Name = "图片上传测试页面：", Url = "/views/fileupdown.html"}
                });
                //添加说明文档
                var basepath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var xmlpath = Path.Combine(basepath, "FileUpanDown.xml");
                c.IncludeXmlComments(xmlpath);
            });
            //添加跨域访问
            services.AddCors(options =>
            {
                options.AddPolicy("Cors", builder => builder.AllowAnyOrigin() //添加跨域访问规则
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "这是显示在右上角的文字");
            });
            app.UseMvc();
        }
    }
}
