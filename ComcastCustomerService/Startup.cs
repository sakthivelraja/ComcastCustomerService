using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;

namespace ComcastCustomerService
{
    public class Startup
    {
        public const string AppS3BucketKey = "AppS3Bucket";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            MvcXmlMvcBuilderExtensions.AddXmlSerializerFormatters(MvcServiceCollectionExtensions.AddMvc(services, (Action<MvcOptions>)(options => options.FormatterMappings.SetMediaTypeMappingForFormat("js", MediaTypeHeaderValue.Parse("application/json").ToString()))));
            Environment.SetEnvironmentVariable("AWS_PROFILE_NAME", Configuration.GetValue<string>("AppSettings:ProfileName"));
            Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID",Configuration.GetValue<string>("AppSettings:AccessKey"));
            Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", Configuration.GetValue<string>("AppSettings:SecretKey"));
            Environment.SetEnvironmentVariable("AWS_REGION", Configuration.GetValue<string>("AppSettings:Region"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (HostingEnvironmentExtensions.IsDevelopment(env))
                DeveloperExceptionPageExtensions.UseDeveloperExceptionPage(app);
            MvcApplicationBuilderExtensions.UseMvc(app);
        }
    }
}
