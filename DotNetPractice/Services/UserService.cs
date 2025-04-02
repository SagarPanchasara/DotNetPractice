using System.Collections.Concurrent;
using System.Runtime.Serialization;
using DotNetPractice.Data;
using DotNetPractice.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DotNetPractice.Services
{
    public class UserService(ApplicationDbContext context)
    {

        public async Task<List<Models.User>> List()
        {
            var users = await context.Users.ToListAsync();
            var partitioner = Partitioner.Create(0, users.Count);
            Parallel.ForEach(partitioner, (range, loopState) =>
            {
                for (var i = range.Item1; i < range.Item2; i++)
                {
                    users[i].FormatedCreatedOn = users[i].CreatedOn.ToString("dd/MM/yyyy");
                }
            });
            return users;
        }

        public UserDto Add(UserDto userDto)
        {
            if (!string.IsNullOrEmpty(userDto.IdNumber) && context.Users.Where(U => U.IdNumber == userDto.IdNumber).Any())
            {
                throw new HttpRequestException("IDNumber already exists", null, System.Net.HttpStatusCode.Conflict);
            }
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Models.User> entityEntry = context.Users.Add(new Models.User()
            {
                Name = userDto.Name,
                IdNumber = userDto.IdNumber
            });
            context.SaveChanges();
            userDto.Id = entityEntry.Entity.Id;
            userDto.CreatedOn = entityEntry.Entity.CreatedOn;
            return userDto;
        }

        public async Task<List<Models.User>> Query(String keyword)
        {
            var users = await this.List();
            ParallelQuery<Models.User> parallelQuery = from user in users.AsParallel().WithDegreeOfParallelism(2)
                where user.Name.Contains(keyword)
                select user;
            return parallelQuery.ToList();
        }

        private Models.User FindById(int id)
        {
            var user = context.Users.Where(U => U.Id == id).FirstOrDefault();
            if (null == user)
            {
                throw new HttpRequestException("Invalid Id", null, System.Net.HttpStatusCode.BadRequest);
            }
            return user;
        }

        public UserDto Get(int id)
        {
            var user = FindById(id);
            return new()
            {
                Id = user.Id,
                Name = user.Name,
                IdNumber = user.IdNumber,
                CreatedOn = user.CreatedOn,
            };
        }

        public void Update(int id, UserDto userDto)
        {
            if (!string.IsNullOrEmpty(userDto.IdNumber) && context.Users.Where(U => U.IdNumber == userDto.IdNumber && U.Id != id).Any())
            {
                throw new HttpRequestException("IDNumber already exists", null, System.Net.HttpStatusCode.Conflict);
            }
            var user = FindById(id);
            user.Name = userDto.Name;
            user.IdNumber = userDto.IdNumber;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var count = context.Users.Where(U => U.Id == id).ExecuteDelete();
            if (count == 0)
            {
                throw new HttpRequestException("Invalid Id", null, System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}
