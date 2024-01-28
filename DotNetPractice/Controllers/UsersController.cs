using DotNetPractice.Dtos;
using DotNetPractice.Services;
using DotNetPractice.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPractice.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class UsersController(UserService userService) : ControllerBase
    {
        [HttpGet]
        public List<Models.User> List()
        {
            return userService.List();
        }

        [HttpPost]
        public IActionResult Save(UserDto userDto)
        {
            return Ok(userService.Add(userDto));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(userService.Get(id));
        }

    }
}
