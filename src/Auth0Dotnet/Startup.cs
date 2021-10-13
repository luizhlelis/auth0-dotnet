using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.OpenApi.Models;

namespace Auth0Dotnet
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
            var openApiInfo = new OpenApiInfo();
            Configuration.Bind("OpenApiInfo", openApiInfo);

            services.AddControllers();
            services.AddApiVersioning(options => {
                options.ReportApiVersions = true;

                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", openApiInfo);

                // Set the comments path for the Swagger JSON and UI
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });

            // Cookie configuration for HTTP to support cookies with SameSite=None
            services.ConfigureSameSiteNoneCookies();

            // Cookie configuration for HTTPS
            // services.Configure<CookiePolicyOptions>(options =>
            // {
            //    options.MinimumSameSitePolicy = SameSiteMode.None
            // });

            // Add authentication services
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect("Auth0", options => {
                // Set the authority to your Auth0 domain
                options.Authority = $"https://{Configuration["AuthorizationServer:Domain"]}";

                // Configure the Auth0 Client ID and Client Secret
                options.ClientId = Configuration["AuthorizationServer:ClientId"];
                        options.ClientSecret = Configuration["AuthorizationServer:ClientSecret"];

                // Set response type to code
                options.ResponseType = OpenIdConnectResponseType.Code;

                // Configure the scope
                options.Scope.Clear();
                options.Scope.Add(Configuration["AuthorizationServer:Scope"]);

                // Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard
                options.CallbackPath = new PathString("/response-oidc");

                // Configure the Claims Issuer to be Auth0
                options.ClaimsIssuer = "Auth0";
            });

            // Add framework services.
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth0Dotnet v1");
                    c.RoutePrefix = string.Empty;
                }
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
