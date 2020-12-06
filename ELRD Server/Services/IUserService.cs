using ELRDDataAccessLibrary.Models;
using ELRDServerAPI.Contracts.V1.Requests;
using ELRDServerAPI.Contracts.V1.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ELRDServerAPI.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();

        Task<User> GetUserByIDAsync(int id);

        Task<UserResponse> AddNewUserAsync(CreateUserRequest u);

        Task<bool> UpdateUserAsync(User userToUpdate);

        Task<bool> DeleteUserAsync(int id);
    }
}
