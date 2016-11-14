using System.IO;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Threading.Tasks;
using System;
using System.Security.Claims;

namespace MainSite
{
    public class Startup
    {
        public static void Main(string[] args)
        {
        
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add authentication services
            services.AddAuthentication(
                options => options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);

            // Add framework services.
            services.AddMvc();

            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // Add the Auth0 Settings object so it can be injected
            services.Configure<Auth0Settings>(Configuration.GetSection("Auth0"));

            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.CookieName = ".VVCS.Session";
            });

            //using Dependency Injection
            services.AddSingleton<IConfigurationRoot>(Configuration);
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<Auth0Settings> auth0Settings)
        {
            //enable session before MVC
            app.UseSession();

            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Add the cookie middleware
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            var options = new OpenIdConnectOptions("Auth0")
            {
                // Set the authority to your Auth0 domain
                Authority = $"https://{auth0Settings.Value.Domain}",

                // Configure the Auth0 Client ID and Client Secret
                ClientId = auth0Settings.Value.ClientId,
                ClientSecret = auth0Settings.Value.ClientSecret,

                // Do not automatically authenticate and challenge
                AutomaticAuthenticate = false,
                AutomaticChallenge = false,

                // Set response type to code
                ResponseType = "code",

                // Set the callback path
                // Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard
                CallbackPath = new PathString("/signin-auth0"),

                // Configure the Claims Issuer to be Auth0
                ClaimsIssuer = "Auth0",

                Events = new OpenIdConnectEvents
                {
                    OnRedirectToIdentityProvider = context =>
                    {
                        if (context.Properties.Items.ContainsKey("connection"))
                            context.ProtocolMessage.SetParameter("connection", context.Properties.Items["connection"]);

                        return Task.FromResult(0);
                    },
                    OnTicketReceived = context =>
                    {
                        // Get the ClaimsIdentity
                        var identity = context.Principal.Identity as ClaimsIdentity;
                        if (identity != null)
                        {
                            // Add the Name ClaimType. This is required if we want User.Identity.Name to actually return something!
                            //if (!context.Principal.HasClaim(c => c.Type == ClaimTypes.Name) &&
                            //    identity.HasClaim(c => c.Type == "name"))
                            //    identity.AddClaim(new Claim(ClaimTypes.Name, identity.FindFirst("name").Value));
                        }

                        return Task.FromResult(0);
                    }
                }
            };

            options.Scope.Clear();
            options.Scope.Add("openid");
            options.Scope.Add("user_id");
            options.Scope.Add("name");
            options.Scope.Add("picture");
            options.Scope.Add("email");
            options.Scope.Add("family_name");
            options.Scope.Add("given_name");
            options.Scope.Add("gender");
            app.UseOpenIdConnectAuthentication(options);

            //// Add the OIDC middleware
            //var options = new OpenIdConnectOptions("Auth0")
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
            //    ClaimsIssuer = "Auth0"
            //};
            //options.Scope.Clear();
            //options.Scope.Add("openid");
            //app.UseOpenIdConnectAuthentication(options);

            app.UseMvcWithDefaultRoute();
        }
    }
}
