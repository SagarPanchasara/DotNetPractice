﻿using DotNetPractice.Data;
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
