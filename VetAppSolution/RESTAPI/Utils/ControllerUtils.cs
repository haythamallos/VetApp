using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vetapp.Engine.BusinessFacadeLayer;
using Vetapp.Engine.DataAccessLayer.Data;

namespace RESTAPI.Utils
{
    public class ControllerUtils
    {
 
        private readonly string _defaultConnection;

        public ControllerUtils(string defaultConnection)
        {
            _defaultConnection = defaultConnection;
        }

        public Apilog logAPIRequest(Microsoft.AspNetCore.Http.HttpContext context)
        {
            Apilog apilog = null;
            try
            {
                apilog = new Apilog();
                BusFacCore busFacCore = new BusFacCore(_defaultConnection);

                var host = $"{context.Request.Scheme}://{context.Request.Host}";
                apilog.Reqtxt = context.Request.Path;
                apilog.InProgress = true;
                apilog.Msgsource = context.Request.Method;
                apilog.CallStartTime = DateTime.UtcNow;
            }
            catch { }
            return apilog;
        }

        public void logAPIResponse(Microsoft.AspNetCore.Http.HttpContext context, Apilog pApilog, string pStrPostData = null, string pSearchText = null, string pAuthUserID = null)
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

                            //pApilog.Resptxt = response.StatusCode.ToString();
                            pApilog.IsSuccess = false;
                            if (context.Response.StatusCode == 200)
                            {
                                pApilog.IsSuccess = true;
                            }

                            pApilog.InProgress = false;
                            if ((!string.IsNullOrEmpty(context.Response.StatusCode.ToString())) && (context.Response.StatusCode.ToString() == "OK"))
                            {
                                pApilog.HttpStatusStr = context.Response.StatusCode.ToString();
                                pApilog.IsSuccess = true;
                            }
                            else
                            {
                                pApilog.HttpStatusStr = context.Response.StatusCode.ToString();
                            }

                            if (!string.IsNullOrEmpty(pStrPostData))
                            {
                                pApilog.Trace = pStrPostData;
                            }

                            //if (pApiLogType != null)
                            //{
                            //    pApilog.Searchtext = pSearchText;
                            //    pApilog.Accesstoken = pAccessToken;
                            //    pApilog.ApilogTypeID = pApiLogType.ApilogTypeID;
                            //}

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
