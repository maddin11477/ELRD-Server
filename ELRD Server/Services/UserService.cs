using ELRDDataAccessLibrary.DataAccess;
using ELRDDataAccessLibrary.Models;
using ELRDServerAPI.Contracts.V1.Requests;
using ELRDServerAPI.Contracts.V1.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELRDServerAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ELRDContext _db;
        private readonly ILogger<UserService> _logger;

        public UserService(ILogger<UserService> log, ELRDContext db)
        {
            _db = db;
            _logger = log;
        }

        public async Task<UserResponse> AddNewUserAsync(CreateUserRequest u)
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
            await _db.Users.AddAsync(x);
            await _db.SaveChangesAsync();


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

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await GetUserByIDAsync(id);

            if (user == null)
                return false;

            _db.Users.Remove(user);
            var deleted = await _db.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<User> GetUserByIDAsync(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<bool> UpdateUserAsync(User userToUpdate)
        {
            //var exists = GetUserById(userToUpdate.Id) != null;
            var exists =  _db.Users.Any(c => c.Id == userToUpdate.Id);

            if (!exists)
                return false;

             _db.Users.Update(userToUpdate);
            var updated = await _db.SaveChangesAsync();

            return updated > 0;

        }
    }
}
