﻿using ELRDDataAccessLibrary.DataAccess;
using ELRDDataAccessLibrary.Models;
using ELRDServerAPI.Contracts.V1.Requests;
using ELRDServerAPI.Contracts.V1.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELRDServerAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ELRDContext _db;

        public UserService(ELRDContext db)
        {
            _db = db;
        }

        public UserResponse AddNewUser(CreateUserRequest u)
        {
            //Map to DB Object
            User x = new ELRDDataAccessLibrary.Models.User
            {
                Firstname = u.Firstname,
                Lastname = u.Lastname,
                Password = u.Password,
                Username = u.Password
            };

            //Save to DB and get ID
            _db.Users.Add(x);
            _db.SaveChanges();


            //Map to Response
            var response = new UserResponse
            {
                Id = x.Id,
                Firstname = x.Firstname,
                Lastname = x.Lastname,
                Password = x.Password,
                Username = x.Password
            };

            return response;
        }

        public User GetUserById(int id)
        {
            return _db.Users.FirstOrDefault(x => x.Id == id);
        }

        public List<User> GetUsers()
        {
            return _db.Users.ToList();
        }
    }
}