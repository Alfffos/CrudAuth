using Agenda_api.Models.DTOs;
using CrudAuth.Models.Entities;

namespace CrudAuth.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        Task<User> ValidateUser(AutenticationRequestBody autenticationRequestBody);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserByName(string userName);
        Task<User> GetUserById(int id);
        Task DeleteUser(int id);
    }
}
