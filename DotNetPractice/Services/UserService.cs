using DotNetPractice.Data;
using DotNetPractice.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DotNetPractice.Services
{
    public class UserService(ApplicationDbContext context)
    {

        public List<Models.User> List()
        {
            var users = context.Users.ToList();
            return users;
        }

    }
}
