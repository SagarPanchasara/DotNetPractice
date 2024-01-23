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

    }
}
