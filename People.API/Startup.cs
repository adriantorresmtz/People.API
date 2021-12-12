using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Models;
using DataAccess.DbAccess;
using DataAccessLlibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;

namespace People.API
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
           // Set Connection for SQLServer for EF
            services.AddDbContext<PersonDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

            // MongoDb Settings 
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            // Set dependency for DataAccess 
            services.AddSingleton<IDataBaseAccess, SqlDataAccess>();

            services.AddSingleton<IDataAccess<PersonModel>, DataAccessMemory>();
            //services.AddTransient<IDataAccess<PersonModel>, DataAccessSQLDapper>(); // Use with Dapper
            //services.AddTransient<IDataAccess<PersonModel>, DataAccessSQLEF>(); // Use this with EF
            //services.AddSingleton<IDataAccess<PersonModel>, DataAccessMongoDB>(); // Use this for MongoDb

            //Load MongoDBSettings from appsettings.json
            services.Configure<MongoDBSettings>(Configuration.GetSection(nameof(MongoDBSettings)));
            //Creat DI for MongoDBSettings
            services.AddSingleton<IMongoDBSettings>(d => d.GetRequiredService<IOptions<MongoDBSettings>>().Value);
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "People.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "People.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
