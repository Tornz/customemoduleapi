using CustomeModule.Model.Model;
using CustomeModule.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using CustomeModule.MailHelper;
using Swashbuckle.AspNetCore.Swagger;
using System.Threading.Tasks;

namespace CustomeModule.API
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public IHostingEnvironment _environment { get; set; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }
        public object provider { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(options =>
                  {
                      options.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuer = true,
                          ValidateAudience = true,
                          ValidateLifetime = true,
                          ValidateIssuerSigningKey = true,

                          ValidIssuer = "CustomeModule.Security.Bearer",
                          ValidAudience = "CustomeModule.Security.Bearer",
                          IssuerSigningKey = JwtSecurityKey.Create("CustomeModule-secret-key"),
                          RequireExpirationTime=true
                          

                      };



                      options.Events = new JwtBearerEvents
                      {
                          OnAuthenticationFailed = context =>
                          {
                              // Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                              return Task.CompletedTask;
                          },
                          OnTokenValidated = context =>
                          {
                              //  Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                              return Task.CompletedTask;
                          }
                      };
                  });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Member",
                    policy => policy.RequireClaim("SecurityCustomeModule"));
            });

            services.AddDbContext<CustomerModuleContext>(r => r.UseMySQL(_configuration.GetConnectionString("CheckWriterConnectionString")));

            //// DI Repo registration
            services.RegisterRepositories();

            //// DI Services registration
            services.RegisterServices();

            services.AddMvc();
            services.AddSignalR();

            //// Email setup
            services.Configure<EmailSetting>(_configuration.GetSection("EmailSettings"));
            services.Configure<EmailTemplate>(_configuration.GetSection("EmailTemplate"));
            ////GET TIMEZONE CONFIGURATION IN APPSETTINGS
            services.Configure<Model.Dto.TimeZone>(_configuration.GetSection("TimeZone"));

            //// Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Enable cors origin
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            ////Signal R
            //app.UseSignalR(routes =>
            //{
            //    routes.MapHub<NotifyHub>("/notify");
            //});

            //// Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            //// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            //// specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "CustomeModule API V1");
                //c.RoutePrefix = string.Empty;
            });

            app.UseStaticFiles();
            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // app.UseMvc();

            //var tokenValidationParameters = new TokenValidationParameters
            //{
            //    //...your setting

            //    // set ClockSkew is zero
            //    ClockSkew = TimeSpan.Zero
            //};

            //app.UseJwtBearerAuthentication(new JwtBearerOptions { Audience=true });
           

            app.UseMvcWithDefaultRoute();
        }
    }
}
