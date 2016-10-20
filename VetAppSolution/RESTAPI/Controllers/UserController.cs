using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RESTAPI.Models;

namespace RESTAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        public UserController(IUserRepository userItems)
        {
            UserItems = userItems;
        }
        public IUserRepository UserItems { get; set; }

        [HttpGet]
        public IEnumerable<UserItem> GetAll()
        {
            return UserItems.GetAll();
        }

        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetById(string id)
        {
            var item = UserItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            UserItems.Add(item);
            return CreatedAtRoute("GetUser", new {}, item);
        }
    }
}
