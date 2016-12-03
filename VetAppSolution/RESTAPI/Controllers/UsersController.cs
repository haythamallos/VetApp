using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RESTAPI.Models;
using RESTAPI.Reply;
using Microsoft.Extensions.Options;
using Vetapp.Client.ProxyCore;
using RESTAPI.Utils;
using Vetapp.Engine.DataAccessLayer.Data;
using System;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace RESTAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UsersController : Controller
    {
        private readonly AppSettings _settings;
        private ControllerUtils _cutils = null;

        public UsersController(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
            _cutils = new ControllerUtils(_settings.DefaultConnection);
        }
        [HttpGet]
        [ActionName("Get")]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        [ActionName("Get")]
        public IActionResult Get(long userid)
        {
            //Apilog apilog = _cutils.logAPIRequest(HttpContext);

            UserProxy item = new UserProxy() { UserID = userid };
            ////if (item == null)
            ////{
            ////    return NotFound();
            ////}
            //_cutils.logAPIResponse(HttpContext, apilog);
            return new ObjectResult(item);
        }

        // POST api/users
        [HttpPost]
        [ActionName("Create")]
        public IActionResult Create([FromBody] UserProxy userBodyProxy)
        {
            try
            {
                Apilog apilog = _cutils.logAPIRequest(HttpContext);
                UserControllerReply reply = new UserControllerReply(_settings);
                var data = reply.Create(userBodyProxy);
                if (!reply.HasError)
                {
                    _cutils.logAPIResponse(reply, 200, apilog);
                    return Ok(data);
                }
                else
                {
                    _cutils.logAPIResponse(reply, 400, apilog);
                    return BadRequest(reply.StatusErrorMessage);
                }
            }
            catch
            {
                return BadRequest("Fatal Error");
            }

        }

        [HttpPost]
        [ActionName("Authenticate")]
        public IActionResult Authenticate([FromBody] UserProxy userBodyProxy)
        {
            try
            {
                Apilog apilog = _cutils.logAPIRequest(HttpContext);
                UserControllerReply reply = new UserControllerReply(_settings);
                var data = reply.Authenticate(userBodyProxy);
                if (!reply.HasError)
                {
                    _cutils.logAPIResponse(reply, 200, apilog);
                    return Ok(data);
                }
                else
                {
                    _cutils.logAPIResponse(reply, 400, apilog);
                    return BadRequest(reply.StatusErrorMessage);
                }
            }
            catch
            {
                return BadRequest("Fatal Error");
            }

        }

        // PUT api/users/5
        [HttpPut("{id}")]
        [ActionName("Put")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        public void Delete(int id)
        {
        }

        [HttpGet]
        [ActionName("Find")]
        public IActionResult Find()
        {
            try
            {
                Apilog apilog = _cutils.logAPIRequest(HttpContext);
                UserControllerReply reply = new UserControllerReply(_settings);
                string username = HttpContext.Request.Query.FirstOrDefault(x => x.Key == "username").Value;
                var data = reply.Find(username);
                if (!reply.HasError)
                {
                    _cutils.logAPIResponse(reply, 200, apilog);
                    return Ok(data);
                }
                else
                {
                    _cutils.logAPIResponse(reply, 400, apilog);
                    return BadRequest(reply.StatusErrorMessage);
                }
            }
            catch
            {
                return BadRequest("Fatal Error");
            }

        }
    }
}

//UserModel memberModel = null;
//UserControllerReply reply = new UserControllerReply(_settings);

//memberModel = reply.Create(pMemberModel);
//if (reply.HasError)
//{
//    return BadRequest(reply.ErrorMessage);
//}
//string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(pUserProxy);
//return Ok(jsonString);

//[HttpGet]
//public IEnumerable<MemberModel> GetAll()
//{
//    return MemberItems.GetAll();
//}

//[HttpGet("{id}", Name = "GetUser")]
//public IActionResult GetById(string id)
//{
//    var item = MemberItems.Find(id);
//    if (item == null)
//    {
//        return NotFound();
//    }
//    return new ObjectResult(item);
//}

//[HttpPost]
//public IActionResult Create([FromBody] Member item)
//{
//    if (item == null)
//    {
//        return BadRequest();
//    }
//    //MemberItems.Add(item);
//    //return CreatedAtRoute("GetUser", new {}, item);
//    return Ok();
//}