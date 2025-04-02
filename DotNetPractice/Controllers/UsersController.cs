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
        public Task<List<Models.User>> List()
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
        
        [HttpGet("query")]
        public async Task<IActionResult> Query([FromQuery(Name = "keyword")] String keyword)
        {
            return Ok(await userService.Query(keyword));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UserDto userDto)
        {
            IActionResult result = NotFound();
            var thread = new Thread(() =>
            {
                if (!string.IsNullOrEmpty(userDto.IdNumber) && !ValidationUtils.IsValidIdNumber(userDto.IdNumber))
                {
                    result = BadRequest("Invalid IDNumber");
                }

                userService.Update(id, userDto);
                result = Ok();
            });
            thread.Start();
            thread.Join();
            return result;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var thread = new Thread(() =>
            {
                userService.Delete(id);
            });
            thread.Start();
            thread.Join();
            return Ok();
        }

    }
}
