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
            if (!string.IsNullOrEmpty(userDto.IdNumber) && !ValidationUtils.IsValidIdNumber(userDto.IdNumber))
            {
                return BadRequest("Invalid IDNumber");
            }
            return Ok(userService.Add(userDto));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(userService.Get(id));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UserDto userDto)
        {
            if (!string.IsNullOrEmpty(userDto.IdNumber) && !ValidationUtils.IsValidIdNumber(userDto.IdNumber))
            {
                return BadRequest("Invalid IDNumber");
            }
            userService.Update(id, userDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            userService.Delete(id);
            return Ok();
        }

    }
}
