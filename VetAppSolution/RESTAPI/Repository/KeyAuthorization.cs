using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace RESTAPI.Repository
{
    public class KeyAuthorization
    {
        private readonly  RequestDelegate _next;
        private IKeyRepository keyRepository { get; set; }

        public readonly string APIKEY = "apikey";

        public KeyAuthorization(RequestDelegate next, IKeyRepository _repo)
        {
            _next = next;
            keyRepository = _repo;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.Keys.Contains(APIKEY))
            {
                context.Response.StatusCode = 400; //Bad Request                
                await context.Response.WriteAsync("API Key is missing");
                return;
            }
            else
            {
                if (!keyRepository.CheckValidApiKey(context.Request.Headers[APIKEY]))
                {
                    context.Response.StatusCode = 401; //UnAuthorized
                    await context.Response.WriteAsync("Invalid API Key");
                    return;
                }
            }

            await _next.Invoke(context);
        }
    }

    #region ExtensionMethod
    public static class ApiKeyValidatorsExtension
    {
        public static IApplicationBuilder ApplyApiKeyValidation(this IApplicationBuilder app)
        {
            app.UseMiddleware<KeyAuthorization>();
            return app;
        }
    }
    #endregion
}
