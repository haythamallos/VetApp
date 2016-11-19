
using System;
using System.Threading.Tasks;
using Vetapp.Engine.BusinessFacadeLayer;
using Vetapp.Engine.DataAccessLayer.Data;
using Microsoft.AspNetCore.Http;
using RESTAPI.Reply;

namespace RESTAPI.Utils
{
    public class ControllerUtils
    {
 
        private readonly string _defaultConnection;

        public ControllerUtils(string defaultConnection)
        {
            _defaultConnection = defaultConnection;
        }

        public Apilog logAPIRequest(HttpContext context)
        {
            Apilog apilog = null;
            try
            {
                apilog = new Apilog();
                BusFacCore busFacCore = new BusFacCore(_defaultConnection);

                var host = $"{context.Request.Scheme}://{context.Request.Host}";
                apilog.Reqtxt = context.Request.Path;
                if (context.Request.QueryString != null)
                {
                    apilog.Reqtxt += context.Request.QueryString.ToString();
                }
                apilog.InProgress = true;
                apilog.Msgsource = context.Request.Method;
                apilog.CallStartTime = DateTime.UtcNow;
            }
            catch { }
            return apilog;
        }

        public void logAPIResponse(ReplyBase reply, int StatusCode, Apilog pApilog, string pStrPostData = null, string pSearchText = null, string pAuthUserID = null)
        {

            try
            {
                Task.Run(() =>
                {
                    try
                    {
                        if ((pApilog != null))
                        {
                            BusFacCore busFacCore = new BusFacCore(_defaultConnection);

                            pApilog.CallEndTime = DateTime.UtcNow;
                            pApilog.DurationInMs = (long)pApilog.CallEndTime.Subtract(pApilog.CallStartTime).TotalMilliseconds;

                            pApilog.IsSuccess = false;
                            pApilog.HttpStatusStr = StatusCode.ToString();
                            if (StatusCode == 200)
                            {
                                pApilog.IsSuccess = true;
                            }
                            else
                            {
                                pApilog.Trace = "<<" + reply.StatusErrorMessage + ">>" + "<<" + reply.ErrorMessage + ">>" + "<<" + reply.ErrorStacktrace + ">>";
                            }

                            pApilog.InProgress = false;
                       
                            long lID = busFacCore.ApilogCreateOrModify(pApilog);
                        }
                    }
                    catch (Exception) { }
                });
            }
            catch { }
        }
    }
}
