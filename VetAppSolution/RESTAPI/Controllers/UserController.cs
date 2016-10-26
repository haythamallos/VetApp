using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RESTAPI.Models;
using RESTAPI.Reply;
using Microsoft.Extensions.Options;

namespace RESTAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly AppSettings _settings;

        public UserController(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }
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

        [HttpPost]
        public IActionResult Create([FromBody] UserModel pMemberModel)
        {
            UserModel memberModel = null;
            UserControllerReply reply = new UserControllerReply(_settings);

            memberModel = reply.Create(pMemberModel);
            if (reply.HasError)
            {
                return BadRequest(reply.ErrorMessage);
            }
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(memberModel);
            return Ok(jsonString);
        }
    }
}
