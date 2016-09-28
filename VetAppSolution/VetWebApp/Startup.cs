using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace VetWebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            //if (env.IsDevelopment())
            //{
            //    // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
            //    builder.AddUserSecrets();
            //}

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add authentication services
            //services.AddAuthentication(
            //    options => options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);

            services.AddMvc();

            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // Add the Auth0 Settings object so it can be injected
            //services.Configure<Auth0Settings>(Configuration.GetSection("Auth0"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");
            app.UseStaticFiles();

            //// Add the cookie middleware
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = true
            //});

            //// Add the OIDC middleware
            //app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions("Auth0")
            //{
            //    // Set the authority to your Auth0 domain
            //    Authority = $"https://{auth0Settings.Value.Domain}",

            //    // Configure the Auth0 Client ID and Client Secret
            //    ClientId = auth0Settings.Value.ClientId,
            //    ClientSecret = auth0Settings.Value.ClientSecret,

            //    // Do not automatically authenticate and challenge
            //    AutomaticAuthenticate = false,
            //    AutomaticChallenge = false,

            //    // Set response type to code
            //    ResponseType = "code",

            //    // Set the callback path, so Auth0 will call back to http://localhost:5000/signin-auth0 
            //    // Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard 
            //    CallbackPath = new PathString("/signin-auth0"),

            //    // Configure the Claims Issuer to be Auth0
            //    ClaimsIssuer = "Auth0",
            //    //Events = new OpenIdConnectEvents
            //    //{
            //    //    OnRedirectToIdentityProvider = context =>
            //    //    {
            //    //        if (context.Properties.Items.ContainsKey("connection"))
            //    //            context.ProtocolMessage.SetParameter("connection", context.Properties.Items["connection"]);

            //    //        return Task.FromResult(0);
            //    //    }
            //    //}


            //});

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});



            app.UseMvcWithDefaultRoute();
            //app.UseMvc(
            //    routes =>
            //    {
            //        routes
            //        .MapRoute("Default", "{controller=Home}/{action=Index}/{id?}")
            //        .MapRoute("Member", "Member/{controller=Member}/{action=Index}/{id?}")
            //        .MapRoute("Account", "Account/{controller=Account}/{action=Index}/{id?}");
            //    });

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }

        // Handle sign-in errors differently than generic errors.
        private Task OnAuthenticationFailed(FailureContext context)
        {
            context.HandleResponse();
            context.Response.Redirect("/Home/Error?message=" + context.Failure.Message);
            return Task.FromResult(0);
        }
    }
}
