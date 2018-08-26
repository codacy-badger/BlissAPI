namespace API
{
    using API.Services;
    using Infrastructure.Context;
    using Infrastructure.Entities;
    using Infrastructure.Repository;
    using Infrastructure.Settings;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json.Serialization;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System;
    using System.IO;
    using System.Xml.XPath;

    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnv;

        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            _hostingEnv = env;

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                }));

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "Bliss", Version = "v1" });
                });

            services.AddMvc()
                .AddJsonOptions(
                    opts =>
                        {
                            opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<Settings>(
                options =>
                    {
                        options.ConnectionString = Configuration.GetSection("MongoDb:ConnectionString").Value;
                        options.Database = Configuration.GetSection("MongoDb:Database").Value;
                    });

            //services.AddHealthChecks();

            services.AddScoped<IMongoContext<QuestionEntity>, QuestionContext>();
            services.AddScoped<IMongoRespository, MongoRespository<QuestionEntity>>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            
            services.AddScoped<IQuestionService, QuestionService>();

            services.ConfigureSwaggerGen(options =>
                {
                    options.DescribeAllEnumsAsStrings();

                    XPathDocument comments = new XPathDocument($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{_hostingEnv.ApplicationName}.xml");
                    options.OperationFilter<XmlCommentsOperationFilter>(comments);
                    options.SchemaFilter<XmlCommentsSchemaFilter>(comments);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors("MyPolicy");

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(
                c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bliss API V1");
                        c.RoutePrefix = "swagger/ui";
                    });

            app.UseSwagger(action =>
                {
                    action.RouteTemplate = "swagger/{documentName}/swagger.json";
                });
        }
    }
}
