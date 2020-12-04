using ELRDDataAccessLibrary.Models;
using ELRDServerAPI.Contracts.V1.Requests;
using ELRDServerAPI.Contracts.V1.Responses;
using System.Collections.Generic;

namespace ELRDServerAPI.Services
{
    public interface IUserService
    {
        List<User> GetUsers();

        User GetUserById(int id);

        UserResponse AddNewUser(CreateUserRequest u);
    }
}
